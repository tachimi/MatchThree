using UnityEngine;

public class RandomSpriteGenerator : MonoBehaviour
{
    [SerializeField] private int _numberOfSpritesGroup;
    [SerializeField] private SpriteGroup[] _spriteGroup;

    private Sprite[] _sprites;

    private void Awake()
    {
        _sprites = _spriteGroup[_numberOfSpritesGroup].GetSprites();
    }

    public void GetRandomSprite(ref GameObject item)
    {
        item.GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length)];
    }
}