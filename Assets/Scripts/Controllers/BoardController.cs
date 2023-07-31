using System;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public Action<Item[,]> IsBoardSpawned;
    
    public int _xSize, _ySize;

    [SerializeField] private RandomSpriteGenerator _randomSpriteGenerator;

    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Item _itemPrefab;

    [SerializeField] private Grid _grid;


    private Item[,] _items;


    private void Awake()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        bool result;
        _items = new Item[_xSize, _ySize];

        SpawnTiles();
        SpawnItems();


        do
        {
            result = FindMatches();
        } while (result);

        IsBoardSpawned?.Invoke(_items);
    }

    private void SpawnTiles()
    {
        for (var x = 0; x < _xSize; x++)
        {
            for (var y = 0; y < _ySize; y++)
            {
                var tile = Instantiate(_tilePrefab);
                tile.transform.position = _grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
            }
        }
    }

    private void SpawnItems()
    {
        for (var x = 0; x < _xSize; x++)
        {
            for (var y = 0; y < _ySize; y++)
            {
                var item = Instantiate(_itemPrefab);
                _randomSpriteGenerator.GetRandomSprite(ref item);
                item.transform.position = _grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
                _items[x, y] = item;
            }
        }
    }


    private bool FindMatches()
    {
        var isMatchesFound = false;

        for (var x = 0; x < _xSize; x++)
        {
            for (var y = 0; y < _ySize - 2; y++)
            {
                var firstElement = _items[x, y].SpriteRenderer.sprite;
                var secondElement = _items[x, y + 1].SpriteRenderer.sprite;
                var thirdElement = _items[x, y + 2].SpriteRenderer.sprite;

                if (firstElement != secondElement || secondElement != thirdElement) continue;

                _randomSpriteGenerator.GetRandomSprite(ref _items[x, y]);
                isMatchesFound = true;
            }
        }

        for (var x = 0; x < _xSize - 2; x++)
        {
            for (var y = 0; y < _ySize; y++)
            {
                var firstElement = _items[x, y].GetComponent<SpriteRenderer>().sprite;
                var secondElement = _items[x + 1, y].GetComponent<SpriteRenderer>().sprite;
                var thirdElement = _items[x + 2, y].GetComponent<SpriteRenderer>().sprite;

                if (firstElement != secondElement || secondElement != thirdElement) continue;

                _randomSpriteGenerator.GetRandomSprite(ref _items[x, y]);
                isMatchesFound = true;
            }
        }

        return isMatchesFound;
    }

    public Item[,] GetItems()
    {
        return _items;
    }

    public void SpawnNewItems()
    {
        for (var x = 0; x < _xSize; x++)
        {
            for (var y = 0; y < _ySize; y++)
            {
                if (_items[x, y] != null) continue;

                var item = Instantiate(_itemPrefab);

                _randomSpriteGenerator.GetRandomSprite(ref item);

                item.transform.localScale = new Vector3(0, 0, 0);
                item.transform.position = _grid.GetCellCenterWorld(new Vector3Int(x, y, 0));

                _items[x, y] = item;
            }
        }
    }
}