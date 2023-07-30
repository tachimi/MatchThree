using DG.Tweening;
using UnityEngine;

public class ItemFallAnimation : MonoBehaviour
{
    private float _animationDuration;

    public Sequence ShowFallAnimation(Item[,] items)
    {
        var sequence = DOTween.Sequence();

        for (var x = 0; x < items.GetLength(0); x++)
        {
            for (var y = 0; y < items.GetLength(1); y++)
            {
                if (items[x, y] == null) continue;

                var itemXPosition = items[x, y].transform.position.x;
                var itemYPosition = items[x, y].transform.position.y;

                if (itemXPosition == x && itemYPosition == y) continue;

                var tween = items[x, y].transform.DOMove(new Vector3(x, y, 0), _animationDuration);
                sequence.Join(tween);
            }
        }

        return sequence;
    }

    public void SetDuration(float duration)
    {
        _animationDuration = duration;
    }
}