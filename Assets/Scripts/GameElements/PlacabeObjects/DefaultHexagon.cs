using DG.Tweening;
using UnityEngine;

public class DefaultHexagon : PlacebleObjectBase
{
    public override int GetPoints()
        => 10;

    protected override void DestroyObjectImpl()
    {
        var randomDelay = Random.Range(0f, 0.25f);
        transform.DOScale(Vector3.zero, duration: 0.25f)
            .SetDelay(randomDelay)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => base.DestroyObjectImpl());
    }
}