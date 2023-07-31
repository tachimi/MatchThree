using DG.Tweening;
using UnityEngine;

public class ItemFallAnimation : MonoBehaviour
{
    private float _animationDuration;

    public Sequence ShowFallAnimation(Item[,] items, Grid grid)
    {
        var sequence = DOTween.Sequence();

        for (var x = 0; x < items.GetLength(0); x++)
        {
            for (var y = 0; y < items.GetLength(1); y++)
            {
                if (items[x, y] == null) continue;

                var itemPos = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));

                var tween = items[x, y].transform
                    .DOMove(itemPos, _animationDuration);
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