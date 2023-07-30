using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private ItemSpawnAnimation _itemSpawnAnimation;
    [SerializeField] private ItemSwapAnimation _itemSwapAnimation;
    [SerializeField] private ItemDestroyAnimation _itemDestroyAnimation;
    [SerializeField] private ItemFallAnimation _itemFallAnimation;

    [SerializeField] private float _itemSpawnAnimationDuration = 1f;
    [SerializeField] private float _itemSwapAnimationDuration = 1f;
    [SerializeField] private float _itemDestroyAnimationDuration = 1f;
    [SerializeField] private float _itemFallAnimationDuration = 1f;

    private void Awake()
    {
        _itemSpawnAnimation.SetDuration(_itemSpawnAnimationDuration);
        _itemSwapAnimation.SetDuration(_itemSwapAnimationDuration);
        _itemDestroyAnimation.SetDuration(_itemDestroyAnimationDuration);
        _itemFallAnimation.SetDuration(_itemFallAnimationDuration);
    }

    public Sequence DoSwapAnimation(Item firstItem, Item secondItem)
    {
        return _itemSwapAnimation.ShowSwapAnimation(firstItem.transform, secondItem.transform);
    }

    public Sequence DoDoubleSwapAnimation(Item firstItem, Item secondItem)
    {
        return _itemSwapAnimation.ShowDoubleSwapAnimation(firstItem.transform, secondItem.transform);
    }

    public Sequence DoFallAnimation(Item[,] items)
    {
        return _itemFallAnimation.ShowFallAnimation(items);
    }

    public Sequence HideItems(List<Item> matches)
    {
        return _itemDestroyAnimation.HideItems(matches);
    }

    public Sequence DoSpawnAnimation(Item[,] items)
    {
        return _itemSpawnAnimation.ShowSpawnAnimation(items);
    }

    public void ShowSpawnAnimationOnStart(Item[,] items)
    {
        _itemSpawnAnimation.ShowSpawnAnimationOnStart(items);
    }
}