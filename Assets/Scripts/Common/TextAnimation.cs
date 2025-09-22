using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class TextAnimation
{
    public TextAnimation(
        UnityObjectLifeController objectController,
        PrefabLoader prefabLoader)
    {
        _objectController = objectController;
        _colorProvider = new ColorProvider();
        _textPrefab = prefabLoader.AddedScoreText;
        _comboPrefab = prefabLoader.ComboText;
    }


    public void CreateAnimatedComboText(int combo, Vector3 position)
    {
        if (combo < 2)
            return;
        combo = combo > 5 ? 5 : combo;

        position = new Vector3(position.x, position.y, position.z - 10f);

        var textObject = CreateComboText(position);
        var text = textObject.GetComponent<TextMeshPro>();
        var word = _comboToEnTextDictionary[combo].SelectRandom();
        text.text = $"x{combo} {word}";
        text.color = _colorProvider.GetNextColor();

        textObject.transform.localScale = new Vector3(0, 0, 1);

        var center = new Vector3 (0f, 0f, position.z);

        Sequence animation = DOTween.Sequence();

        animation.Append(textObject.transform.DOMove(center, 0.6f)
            .SetEase(Ease.OutCubic));
        animation.Join(textObject.transform.DOScale(1, 0.6f)
            .SetEase(Ease.OutCubic));

        animation.AppendInterval(0.5f);

        animation.Append(text.DOFade(0f, 0.4f));
        animation.Join(textObject.transform.DOScale(1.3f, 0.4f));

        animation.OnComplete(() => _objectController.Destroy(textObject));
    }

    public void CreateAnimatedAddedScoreText(int addedScore, Vector3 position)
    {
        position = new Vector3(position.x, position.y, position.z - 5f);

        var textObject = CreateAddedScoreText(position);
        var text = textObject.GetComponent<TextMeshPro>();
        text.text = addedScore.ToString();

        textObject.transform
            .DOMoveY(position.y + 0.75f, 0.25f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
                text.DOFade(0, 0.25f).SetDelay(0.25f)
                    .OnComplete(() => _objectController.Destroy(textObject)));

        var calculatedScale = 0.5f + (addedScore / 1000);
        textObject.transform.localScale = new Vector3(calculatedScale, calculatedScale, 1);
    }

    private GameObject CreateAddedScoreText(Vector3 position)
        => _objectController.Instantiate(_textPrefab, position, Quaternion.identity);

    private GameObject CreateComboText(Vector3 position)
        => _objectController.Instantiate(_comboPrefab, position, Quaternion.identity);

    private readonly UnityObjectLifeController _objectController;
    private readonly ColorProvider _colorProvider;
    private readonly GameObject _textPrefab;
    private readonly GameObject _comboPrefab;

    private readonly Dictionary<int, List<string>> _comboToEnTextDictionary = new()
    {
        { 2, new List<string> { "Good!", "Nice!", "Combo!" } },
        { 3, new List<string> { "Excellent!", "Awesome!", "Amazing!" } },
        { 4, new List<string> { "Incredible!", "Unbelievable!", "Fantastic!" } },
        { 5, new List<string> { "Legendary!", "Godlike!", "Supreme!" } },
    };

    private readonly Dictionary<int, List<string>> _comboToRuTextDictionary = new()
    {
        { 2, new List<string> { "Хорошо!", "Так держать!", "Комбо!" } },
        { 3, new List<string> { "Потрясающе!", "Великолепно!", "Браво!" } },
        { 4, new List<string> { "Невероятно!", "Бесподобно!", "Фантастически!" } },
        { 5, new List<string> { "Легендарно!", "Божественно!", "Величайше!" } },
    };
}