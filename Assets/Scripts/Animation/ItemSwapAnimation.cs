using DG.Tweening;
using UnityEngine;

public class ItemSwapAnimation : MonoBehaviour
{
    private float _animationDuration;

    public Sequence ShowSuccessfulAnimation(Transform firstObj, Transform secondObj)
    {
        var sequence = DOTween.Sequence();

        var firstObjTween = firstObj.DOMove(secondObj.position, _animationDuration);
        var secondObjTween = secondObj.DOMove(firstObj.position, _animationDuration);

        sequence.Join(firstObjTween);
        sequence.Join(secondObjTween);

        return sequence.Play();
    }

    public Sequence showAnimationWithoutMatching(Transform firstObj, Transform secondObj)
    {
        var sequence = DOTween.Sequence();

        var firstObjTween = firstObj.DOMove(secondObj.position, _animationDuration).SetLoops(2, LoopType.Yoyo);
        var secondObjTween = secondObj.DOMove(firstObj.position, _animationDuration).SetLoops(2, LoopType.Yoyo);

        sequence.Join(firstObjTween);
        sequence.Join(secondObjTween);

        return sequence.Play();
    }

    public void SetDuration(float duration)
    {
        _animationDuration = duration;
    }
}