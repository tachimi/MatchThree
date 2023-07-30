using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
}