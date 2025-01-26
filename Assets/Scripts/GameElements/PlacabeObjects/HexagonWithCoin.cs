using DG.Tweening;
using UnityEngine;

public sealed class HexagonWithCoin : DefaultHexagon
{
    [SerializeField] private GameObject _coin;

    public void Init(CoinsLocal coinsLocal)
    {
        _coinsLocal = coinsLocal;
    }

    protected override void DestroyObjectImpl()
    {
        _coinsLocal.AddCoins(1);
        _coin.transform.SetParent(null);
        _coin.transform.DOScale(Vector3.zero, 0.9f).SetEase(Ease.InOutQuart);
        _coin.transform.DOMoveY(_coin.transform.position.y + 1f, 1f).SetEase(Ease.InOutQuart)
            .OnComplete(() =>
            {
                DestroyImmediate(_coin);
            });

        base.DestroyObjectImpl();
    }

    private CoinsLocal _coinsLocal;
}