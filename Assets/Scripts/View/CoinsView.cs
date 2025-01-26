using TMPro;
using UnityEngine;
using Zenject;

public sealed class CoinsView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    [Inject]
    public void Init(CoinsLocal coinsLocal)
    {
        _coinsLocal = coinsLocal;

        _coinsLocal.CoinsChanged += UpdateText;
        UpdateText(coinsLocal.Coins);
    }

    private void UpdateText(int score)
    {
        _text.text = NumberToSpritesConverter.Convert(score);
    }

    private CoinsLocal _coinsLocal;
}
