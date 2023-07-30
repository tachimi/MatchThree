using UnityEngine;

public class EmptyTilesController : MonoBehaviour
{
    public void FillEmptyTiles(Item[,] items)
    {
        for (var y = 0; y < items.GetLength(1); y++)
        {
            for (var x = 0; x < items.GetLength(0); x++)
            {
                if (items[x, y] != null) continue;

                var newY = FindTopItem(x, y, items);
                
                if (newY < 0) continue;
                
                items[x, y] = items[x, newY];
                items[x, newY] = null;
            }
        }
    }

    private int FindTopItem(int x, int currentY, Item[,] items)
    {
        for (var y = currentY + 1; y < items.GetLength(1); y++)
        {
            if (items[x, y] != null)
            {
                return y;
            }
        }

        return -1;
    }
}