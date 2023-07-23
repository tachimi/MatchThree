using System.Collections.Generic;
using UnityEngine;

public class MatchesController : MonoBehaviour
{
    public List<GameObject> CheckMatches(GameObject firstItem, GameObject secondItem, GameObject[,] items)
    {
        var firstSprite = firstItem.GetComponent<SpriteRenderer>().sprite;
        var secondSprite = secondItem.GetComponent<SpriteRenderer>().sprite;

        List<GameObject> matches = new();

        var firstItemHorizontalMatches = FindHorizontalMatches(items, firstItem, firstSprite);
        var firstItemVerticalMatches = FindVerticalMatches(items, firstItem, firstSprite);

        if (firstItemHorizontalMatches.Count >= 2)
        {
            matches.Add(firstItem);
            matches.AddRange(firstItemHorizontalMatches);
        }

        if (firstItemVerticalMatches.Count >= 2)
        {
            matches.Add(firstItem);
            matches.AddRange(firstItemVerticalMatches);
        }


        var secondItemHorizontalMatches = FindHorizontalMatches(items, secondItem, secondSprite);
        var secondItemVerticalMatches = FindVerticalMatches(items, secondItem, secondSprite);

        if (secondItemHorizontalMatches.Count >= 2)
        {
            matches.Add(secondItem);
            matches.AddRange(secondItemHorizontalMatches);
        }

        if (secondItemVerticalMatches.Count >= 2)
        {
            matches.Add(secondItem);
            matches.AddRange(secondItemVerticalMatches);
        }

        return matches;
    }

    private List<GameObject> FindHorizontalMatches(GameObject[,] items, GameObject item, Sprite itemSprite)
    {
        List<GameObject> matches = new();

        var startIndex = item.GetComponent<Index>().Y;
        var fixedLine = item.GetComponent<Index>().X;

        for (var y = startIndex + 1; y < items.GetLength(1); y++)
        {
            if (itemSprite == items[fixedLine, y].GetComponent<SpriteRenderer>().sprite)
            {
                matches.Add(items[fixedLine, y]);
            }
            else
            {
                break;
            }
        }

        for (var y = startIndex - 1; y >= 0; y--)
        {
            if (itemSprite == items[fixedLine, y].GetComponent<SpriteRenderer>().sprite)
            {
                matches.Add(items[fixedLine, y]);
            }
            else
            {
                break;
            }
        }

        return matches;
    }

    private List<GameObject> FindVerticalMatches(GameObject[,] items, GameObject item, Sprite itemSprite)
    {
        List<GameObject> matches = new();

        var startIndex = item.GetComponent<Index>().X;
        var fixedLine = item.GetComponent<Index>().Y;

        for (var x = startIndex + 1; x < items.GetLength(0); x++)
        {
            if (itemSprite == items[x, fixedLine].GetComponent<SpriteRenderer>().sprite)
            {
                matches.Add(items[x, fixedLine]);
            }
            else
            {
                break;
            }
        }

        for (var x = startIndex - 1; x >= 0; x--)
        {
            if (itemSprite == items[x, fixedLine].GetComponent<SpriteRenderer>().sprite)
            {
                matches.Add(items[x, fixedLine]);
            }
            else
            {
                break;
            }
        }

        return matches;
    }


}