using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MovementController _movementController;
    [SerializeField] private MatchesController _matchesController;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private BoardController _boardController;
    [SerializeField] private EmptyTilesController _emptyTilesController;
    [SerializeField] private Grid _grid;

    private Sequence _sequence;
    private List<Item> _matches;
    private Item[,] _items;

    private bool _isAnimationOnPlay;

    private void Awake()
    {
        _movementController.IsDerectionFound += FindMatches;
        _boardController.IsBoardSpawned += _animationController.ShowSpawnAnimationOnStart;
    }

    private void Start()
    {
        _items = _boardController.GetItems();
    }


    private void FindMatches(Vector3Int firstItemIndex, Vector3Int secondItemIndex)
    {
        if (IsIndexOutOfBounds(secondItemIndex))
        {
            return;
        }

        ChangeMovementControllerStatus();

        _matches = new();

        var firstItem = _items[firstItemIndex.x, firstItemIndex.y];
        var secondItem = _items[secondItemIndex.x, secondItemIndex.y];

        UpdateItems(firstItemIndex, secondItemIndex);

        _matches = _matchesController.FindMatches(_items);

        if (_matches.Count == 0)
        {
            UpdateItems(firstItemIndex, secondItemIndex);

            _sequence = _animationController.DoDoubleSwapAnimation(firstItem, secondItem)
                .OnComplete(ChangeMovementControllerStatus);
            return;
        }

        _sequence = _animationController.DoSwapAnimation(firstItem, secondItem);
        _sequence.Append(_animationController.HideItems(_matches));
        _sequence.OnComplete(DestroyMatches);
        _sequence.OnComplete(HandleSituation);
        _sequence.Play();
    }

    private void HandleSituation()
    {
        var isNeedFallAnimation = _emptyTilesController.FillEmptyTiles(_items);
        _sequence = _animationController.DoFallAnimation(_items, _grid, isNeedFallAnimation);
        _boardController.SpawnNewItems();
        _sequence.Append(_animationController.DoSpawnAnimation(_items));
        _sequence.OnComplete(CheckForMatches);
        _sequence.Play();
    }

    private void CheckForMatches()
    {
        _matches = _matchesController.FindMatches(_items);
        if (_matches.Count == 0)
        {
            ChangeMovementControllerStatus();
            return;
        }

        _sequence = _animationController.HideItems(_matches);
        _sequence.OnComplete(DestroyMatches);
        _sequence.OnComplete(HandleSituation);
    }

    private bool IsIndexOutOfBounds(Vector3Int index)
    {
        return index.x < 0 || index.y < 0 || index.x >= _items.GetLength(0) || index.y >= _items.GetLength(1);
    }

    private void UpdateItems(Vector3Int firstItemIndex, Vector3Int secondItemIndex)
    {
        (_items[firstItemIndex.x, firstItemIndex.y], _items[secondItemIndex.x, secondItemIndex.y]) =
            (_items[secondItemIndex.x, secondItemIndex.y], _items[firstItemIndex.x, firstItemIndex.y]);
    }

    private void ChangeMovementControllerStatus()
    {
        _movementController.enabled = !_movementController.enabled;
    }

    private void DestroyMatches()
    {
        foreach (var item in _matches)
        {
            Destroy(item.gameObject);
        }

        _matches.Clear();
    }
}