using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private ItemSpawnAnimation _itemSpawnAnimation;
    [SerializeField] private ItemSwapAnimation _itemSwapAnimation;
    [SerializeField] private ItemDestroyAnimation _itemDestroyAnimation;

    [SerializeField] private float _itemSpawnAnimationDuration = 1f;
    [SerializeField] private float _itemSwapAnimationDuration = 1f;
    [SerializeField] private float _itemFadeAnimationDuration = 1f;

    public Action<GameObject[,]> IsBoardSpawned;
    public Func<List<GameObject>, Sequence> IsMatchesFound;


    private void Awake()
    {
        _itemSpawnAnimation.SetDuration(_itemSpawnAnimationDuration);
        _itemSwapAnimation.SetDuration(_itemSwapAnimationDuration);
        _itemDestroyAnimation.SetDuration(_itemFadeAnimationDuration);

        IsBoardSpawned += _itemSpawnAnimation.ShowSpawnAnimation;

        IsMatchesFound += _itemDestroyAnimation.HideItems;
    }

    public Sequence DoSwapAnimation(int _x, int _y, int originalIndex, int newIndex, bool isHorizontal,
        bool isMatchesFound, GameObject[,] _items, List<GameObject> matches)
    {
        var sequence = DOTween.Sequence();

        if (isHorizontal)
        {
            if (isMatchesFound)
            {
                sequence = _itemSwapAnimation.ShowSuccessfulAnimation(_items[originalIndex, _y].transform,
                    _items[newIndex, _y].transform);
            }
            else
            {
                sequence = _itemSwapAnimation.showAnimationWithoutMatching(_items[originalIndex, _y].transform,
                    _items[newIndex, _y].transform);
            }
        }
        else
        {
            if (isMatchesFound)
            {
                sequence = _itemSwapAnimation.ShowSuccessfulAnimation(_items[_x, originalIndex].transform,
                    _items[_x, newIndex].transform);
            }
            else
            {
                sequence = _itemSwapAnimation.showAnimationWithoutMatching(_items[_x, originalIndex].transform,
                    _items[_x, newIndex].transform);
            }
        }

        return sequence;
    }

    public Sequence HideItems(List<GameObject> matches)
    {
        return IsMatchesFound.Invoke(matches);
    }
}