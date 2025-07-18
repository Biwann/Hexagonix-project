using DG.Tweening;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public sealed class HexagonWithBomb : DefaultHexagon
{
    [SerializeField] private GameObject _explosiveObject;

    public void Init(
        CellFolder cellFolder,
        TextAnimation textAnimation,
        ScoresOnLevel scoresOnLevel,
        GameEvents gameEvents,
        ScoresUpgradeCharacteristicProvider scoresProvider)
    {
        _cellFolder = cellFolder;
        _textAnimation = textAnimation;
        _scoresOnLevel = scoresOnLevel;
        _gameEvents = gameEvents;

        base.Init(scoresProvider);
    }

    protected override void DestroyObjectImpl()
    {
        transform.DOScale(Vector3.zero, duration: 0.25f)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() =>
            {
                DestroyNeighbors();
                ExplodeEffect();
                base.DestroyObjectImpl();
            });
    }

    private List<Cell> GetNeighbors()
    {
        var currentPosition = GetPlacedPosition();
        Debug.Log("current position : " + currentPosition);
        var neighbors = new List<Cell>();

        foreach (var (xShift, yShift) in _neighborsShifts)
        {
            var needHorizontalShift = currentPosition.Y % 2 != 0 && yShift != 0;
            var nXPosition = currentPosition.X + xShift + (needHorizontalShift ? 1 : 0);
            var nYPosition = currentPosition.Y + yShift;

            var cell = _cellFolder.GetCellByPosition(nXPosition, nYPosition);
            if (cell != null)
            {
                neighbors.Add(cell);
            }
        }

        Debug.Log(string.Join('\n', neighbors.Select(c => c.Position)));
        return neighbors;
    }

    private void ExplodeEffect()
    {
        _explosiveObject.transform.SetParent(null);
        _explosiveObject.transform.localScale = Vector3.zero;

        _explosiveObject.transform.position = new Vector3(
            _explosiveObject.transform.position.x,
            _explosiveObject.transform.position.y, 
            _explosiveObject.transform.position.z - 5f);

        _explosiveObject.SetActive(true);

        _explosiveObject.transform.DOScale(Vector3.one, 0.25f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                _explosiveObject.transform.DOScale(Vector3.zero, 0.05f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => DestroyImmediate(_explosiveObject));
            });

        ShakeCamera();
    }

    private void DestroyNeighbors()
    {
        var scores = 0;
        GetNeighbors().ForEach(c => scores += c.ClearCellAndGetPoints() * 2);

        if (scores > 0)
        {
            _textAnimation.CreateAnimatedAddedScoreText(scores, gameObject.transform.position);
            _scoresOnLevel.AddScore(scores);
        }

        _gameEvents.InvokeGameFieldChanged();
    }

    private void ShakeCamera()
    {
        GameCamera.Instance?.DOShakePosition(0.5f, 0.5f, 10, 90f, false)
            .OnStart(() => _cameraShakeAmount++)
            .OnComplete(() =>
            {
                _cameraShakeAmount--;
                if (_cameraShakeAmount == 0)
                {
                    GameCamera.Instance?.gameObject.transform.DOMove(GameCamera.StartPosition, 0.25f);
                }
            });
    }

    private static (int, int)[] _neighborsShifts = new (int, int)[] { (-1, 0), (1, 0), (-1, 1), (0, 1), (-1, -1), (0, -1) };
    private static int _cameraShakeAmount;
    private CellFolder _cellFolder;
    private TextAnimation _textAnimation;
    private ScoresOnLevel _scoresOnLevel;
    private GameEvents _gameEvents;
}