using System.Collections.Generic;
using UnityEngine;

public class MatchesController : MonoBehaviour
{
    public List<Item> FindMatches(Item[,] items)
    {
        var matchesBoard = new int[items.GetLength(0), items.GetLength(1)];
        List<Item> matches = new();

        FindHorizontalMatches(items, ref matchesBoard);
        FindVerticalMatches(items, ref matchesBoard);

        for (var x = 0; x < matchesBoard.GetLength(0); x++)
        {
            for (var y = 0; y < matchesBoard.GetLength(1); y++)
            {
                if (matchesBoard[x, y] != 1) continue;

                matches.Add(items[x, y]);
                items[x, y] = null;
            }
        }

        return matches;
    }

    private void FindHorizontalMatches(Item[,] items, ref int[,] matchesBoard)
    {
        for (var x = 0; x < items.GetLength(0) - 2; x++)
        {
            for (var y = 0; y < items.GetLength(1); y++)
            {
                var firstElement = items[x, y].SpriteRenderer.sprite;
                var secondElement = items[x + 1, y].SpriteRenderer.sprite;
                var thirdElement = items[x + 2, y].SpriteRenderer.sprite;

                if (firstElement != secondElement || secondElement != thirdElement) continue;

                matchesBoard[x, y] = 1;
                matchesBoard[x + 1, y] = 1;
                matchesBoard[x + 2, y] = 1;
            }
        }
    }

    private void FindVerticalMatches(Item[,] items, ref int[,] matchesBoard)
    {
        for (var x = 0; x < items.GetLength(0); x++)
        {
            for (var y = 0; y < items.GetLength(1) - 2; y++)
            {
                var firstElement = items[x, y].SpriteRenderer.sprite;
                var secondElement = items[x, y + 1].SpriteRenderer.sprite;
                var thirdElement = items[x, y + 2].SpriteRenderer.sprite;

                if (firstElement != secondElement || secondElement != thirdElement) continue;

                matchesBoard[x, y] = 1;
                matchesBoard[x, y + 1] = 1;
                matchesBoard[x, y + 2] = 1;
            }
        }
    }
}