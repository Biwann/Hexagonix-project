using DG.Tweening;
using UnityEngine;

public class DefaultHexagon : PlacebleObjectBase
{
    public void Init(
        ScoresUpgradeCharacteristicProvider scoresProvider)
    {
        _scoresProvider = scoresProvider;
    }

    public override int GetPoints()
        => _scoresProvider.ScoresInHexagon;

    protected override void DestroyObjectImpl()
    {
        var randomDelay = Random.Range(0f, 0.25f);
        transform.DOScale(Vector3.zero, duration: 0.25f)
            .SetDelay(randomDelay)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() =>
            {
                GameAudioController.Instance.PlayPopHexagon();
                base.DestroyObjectImpl();
            });
    }

    private ScoresUpgradeCharacteristicProvider _scoresProvider;
}