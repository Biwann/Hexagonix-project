using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public sealed class HexagonWithCoin : DefaultHexagon
{
    [SerializeField] private GameObject _coin;

    public void Init(
        CoinsLocal coinsLocal,
        CoinsUpgradeCharacteristicProvider characteristic)
    {
        _coinsLocal = coinsLocal;
        _coinsToAdd = characteristic.CoinsPerOneHexagonWithCoin;
    }

    protected override void DestroyObjectImpl()
    {
        _coin.transform.SetParent(null);
        var coins = new GameObject[_coinsToAdd];
        coins[0] = _coin;
        for (int i = 1; i < _coinsToAdd; i++)
        {
            var newCoin = Instantiate(_coin, _coin.transform.position, _coin.transform.rotation);
            newCoin.transform.localScale = _coin.transform.localScale;
            coins[i] = newCoin;
        }

        var angleStep = 360f / _coinsToAdd;
        var currentAngle = 90f;

        foreach (var coin in coins)
        {
            var radians = currentAngle * Mathf.Deg2Rad;
            currentAngle += angleStep;
            var direction = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0).normalized;

            coin.transform.DOMove(coin.transform.position + direction, 0.3f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _coinsLocal.AddCoins(1);

                    coin.transform.DOScale(Vector3.zero, 0.9f).SetEase(Ease.InOutQuart);
                    coin.transform.DOMoveY(_coin.transform.position.y + 1f, 1f).SetEase(Ease.InOutQuart)
                        .OnComplete(() =>
                        {
                            DestroyImmediate(_coin);
                        });
                });
        }
        
        base.DestroyObjectImpl();
    }

    private CoinsLocal _coinsLocal;
    private int _coinsToAdd;
}