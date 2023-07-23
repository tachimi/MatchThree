using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemDestroyAnimation : MonoBehaviour
{
    private float _animationDuration;

    public Sequence HideItems(List<GameObject> items)
    {
        var sequence = DOTween.Sequence();
        foreach (var item in items)
        {
            var tween = item.transform.DOScale(new Vector3(0, 0, 0), _animationDuration);

            sequence.Join(tween);
        }

        return sequence.Play();
    }

    public void SetDuration(float duration)
    {
        _animationDuration = duration;
    }
}