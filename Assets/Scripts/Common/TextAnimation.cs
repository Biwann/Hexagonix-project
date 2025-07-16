using DG.Tweening;
using TMPro;
using UnityEngine;

public sealed class TextAnimation
{
    public TextAnimation(
        UnityObjectLifeController objectController,
        PrefabLoader prefabLoader)
    {
        _objectController = objectController;
        _textPrefab = prefabLoader.AddedScoreText;
    }

    public void CreateAnimatedAddedScoreText(int addedScore, Vector3 position)
    {
        position = new Vector3(position.x, position.y, position.z - 5f);

        var textObject = CreateAnimatedFloatUpText(addedScore.ToString(), position);

        var calculatedScale = 0.5f + (addedScore / 1000);
        textObject.transform.localScale = new Vector3(calculatedScale, calculatedScale, 1);
    }

    public GameObject CreateAnimatedFloatUpText(string message, Vector3 position)
    {
        var textObject = CreateAddedScoreText(position);
        var text = textObject.GetComponent<TextMeshPro>();
        text.text = message;

        textObject.transform
            .DOMoveY(position.y + 0.75f, 0.25f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
                text.DOFade(0, 0.25f).SetDelay(0.25f)
                    .OnComplete(() => _objectController.Destroy(textObject)));

        return textObject;
    }

    private GameObject CreateAddedScoreText(Vector3 position)
        => _objectController.Instantiate(_textPrefab, position, Quaternion.identity);

    private readonly UnityObjectLifeController _objectController;
    private readonly GameObject _textPrefab;
}