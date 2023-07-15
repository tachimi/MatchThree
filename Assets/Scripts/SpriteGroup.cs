using UnityEngine;

[CreateAssetMenu(fileName = "SpriteGroup")]
public class SpriteGroup : ScriptableObject
{
    [SerializeField] private Sprite[] _sprites;

    public Sprite[] GetSprites()
    {
        return _sprites;
    }
}
