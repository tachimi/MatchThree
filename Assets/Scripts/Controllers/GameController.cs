using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MovementController _movementController;
    [SerializeField] private MatchesController _matchesController;
    [SerializeField] private AnimationController _animationController;

    private Sequence _sequence;
    private bool _isAnimationOnPlay;
    private List<GameObject> _matches;

    private void Awake()
    {
        _movementController.IsDerectionFound += FoundMatches;
    }

    private void Update()
    {
        if (!_isAnimationOnPlay)
        {
            return;
        }

        if (_sequence.IsActive()) return;

        ChangeAnimationStatus();
    }

    private void FoundMatches(int originalIndex, int newIndex, bool isHorizontal, GameObject[,] items)
    {
        _matches = new List<GameObject>();
        var isMatchesFound = false;

        var x = _movementController._x;
        var y = _movementController._y;


        if (isHorizontal)
        {
            UpdateIndex(items[originalIndex, y], items[newIndex, y], items);

            _matches = _matchesController.CheckMatches(items[originalIndex, y], items[newIndex, y], items);

            if (_matches.Count > 0)
            {
                isMatchesFound = true;
            }
        }
        else
        {
            UpdateIndex(items[x, originalIndex], items[x, newIndex], items);

            _matches = _matchesController.CheckMatches(items[x, originalIndex], items[x, newIndex], items);

            if (_matches.Count > 0)
            {
                isMatchesFound = true;
            }
        }

        ChangeAnimationStatus();

        switch (isMatchesFound)
        {
            case true when isHorizontal:
                _sequence = _animationController.DoSwapAnimation(x, y, originalIndex, newIndex, true, true, items,
                    _matches).OnComplete((HideItems));
                return;
            case true when !isHorizontal:
                _sequence = _animationController.DoSwapAnimation(x, y, originalIndex, newIndex, false, true, items,
                    _matches).OnComplete((HideItems));
                return;
            case false when isHorizontal:
                UpdateIndex(items[originalIndex, y], items[newIndex, y], items);

                _sequence = _animationController.DoSwapAnimation(x, y, originalIndex, newIndex, true, false, items,
                    _matches);
                return;
            case false when !isHorizontal:
                UpdateIndex(items[x, originalIndex], items[x, newIndex], items);

                _sequence = _animationController.DoSwapAnimation(x, y, originalIndex, newIndex, false, false, items,
                    _matches);
                break;
        }
    }

    private void HideItems()
    {
        _sequence = _animationController.HideItems(_matches);
    }

    private void UpdateIndex(GameObject firstItem, GameObject secondItem, GameObject[,] items)
    {
        var firstItemIndex = firstItem.GetComponent<Index>();
        var secondItemIndex = secondItem.GetComponent<Index>();

        items[firstItemIndex.X, firstItemIndex.Y] = secondItem;
        items[secondItemIndex.X, secondItemIndex.Y] = firstItem;

        (firstItemIndex.X, secondItemIndex.X) = (secondItemIndex.X, firstItemIndex.X);
        (firstItemIndex.Y, secondItemIndex.Y) = (secondItemIndex.Y, firstItemIndex.Y);
    }

    private void ChangeAnimationStatus()
    {
        _isAnimationOnPlay = !_isAnimationOnPlay;
        _movementController.enabled = !_movementController.enabled;
    }
}