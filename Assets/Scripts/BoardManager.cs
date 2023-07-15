using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private ItemSpawnAnimation _itemSpawnAnimation;

    [SerializeField] private int _xSize, _ySize;
    [SerializeField] private int _xOffset, _yOffset;

    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private GameObject _itemPrefab;

    [SerializeField] private int _numberOfSpritesGroup;

    [SerializeField] private SpriteGroup[] _spriteGroup;

    private GameObject[,] _items;
    private Sprite[] _sprites;


    private void Awake()
    {
        _sprites = _spriteGroup[_numberOfSpritesGroup].GetSprites();
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        bool result;
        _items = new GameObject[_xSize, _ySize];

        var startX = transform.position.x;
        var startY = transform.position.y;
        
        SpawnTiles(startX,startY);
        SpawnItems(startX,startY);

        do
        {
            result = CheckMatches();
        } while (result);

        _itemSpawnAnimation.IsBoardSpawned.Invoke(_items);
    }

    private void SpawnTiles(float startX, float startY)
    {
        for (var x = 0; x < _xSize; x++)
        {
            for (var y = 0; y < _ySize; y++)
            {
                Instantiate(_tilePrefab, new Vector3(startX + x * _xOffset, startY + y * _yOffset, 0),
                    Quaternion.identity);
            }
        }
    }

    private void SpawnItems(float startX, float startY)
    {
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                var item = Instantiate(_itemPrefab,
                    new Vector3(startX + x * _xOffset, startY + y * _yOffset, 0),
                    Quaternion.identity);
                GenerateRandomSprite(ref item);
                _items[x, y] = item;
            }
        }
    }

    private void GenerateRandomSprite(ref GameObject item)
    {
        var randomSprite = _sprites[Random.Range(0, _sprites.Length)];
        item.GetComponent<SpriteRenderer>().sprite = randomSprite;
    }

    private bool CheckMatches()
    {
        var isMatchesFound = false;

        for (var x = 0; x < _xSize; x++)
        {
            for (var y = 0; y < _ySize - 2; y++)
            {
                var firstElement = _items[x, y].GetComponent<SpriteRenderer>().sprite;
                var secondElement = _items[x, y + 1].GetComponent<SpriteRenderer>().sprite;
                var thirdElement = _items[x, y + 2].GetComponent<SpriteRenderer>().sprite;

                if (firstElement != secondElement || secondElement != thirdElement) continue;

                GenerateRandomSprite(ref _items[x, y]);
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

                GenerateRandomSprite(ref _items[x, y]);
                isMatchesFound = true;
            }
        }

        return isMatchesFound;
    }
}