using DG.Tweening;
using UnityEngine;

public class ItemSpawnAnimation : MonoBehaviour
{
    private float _animationDuration;

    public void ShowSpawnAnimation(GameObject[,] items)
    {
        var sequence = DOTween.Sequence();

        foreach (var item in items)
        {
            var tween = item.transform.DOScale(new Vector3(0.55f, 0.55f, 0.55f), _animationDuration);
            sequence.Join(tween);
        }

        sequence.Play();
    }

    public void SetDuration(float duration)
    {
        _animationDuration = duration;
    }
}