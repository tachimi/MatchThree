using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private LayerMask _targetForRaycast;

    public Action<int, int, bool, GameObject[,]> IsDerectionFound;

    public int _x;
    public int _y;

    private Vector2 _clickPosition;
    private Vector2 _endPosition;

    private RaycastHit2D _hit;

    private GameObject[,] _items;

    private List<GameObject> _matches;

    private Sequence _sequence;

    private void Start()
    {
        _items = _boardManager.GetItems();
    }

    void Update()
    {
        if (_sequence.IsActive() && _sequence != null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _hit = Physics2D.Raycast(_clickPosition, Vector2.zero, Mathf.Infinity, _targetForRaycast);

            if (_hit.collider == null) return;

            _x = _hit.collider.GetComponent<Index>().X;
            _y = _hit.collider.GetComponent<Index>().Y;
        }

        if (!Input.GetMouseButtonUp(0)) return;

        if (_hit.collider == null) return;

        _endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        FindDirection(_clickPosition, _endPosition);
    }

    private void FindDirection(Vector2 clickPos, Vector2 endPos)
    {
        var direction = clickPos - endPos;

        if (Mathf.Abs(direction.x) == Mathf.Abs(direction.y)) return;

        var x = _x;
        var y = _y;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Движение по горизонтали
            if (direction.x > 0)
            {
                var newX = --_x;

                if (newX < 0)
                {
                    return;
                }

                IsDerectionFound.Invoke(x, newX, true, _items);
            }
            else
            {
                var newX = ++_x;

                if (newX >= _items.GetLength(0))
                {
                    return;
                }

                IsDerectionFound.Invoke(x, newX, true, _items);
            }
        }
        else
        {
            // Движение по вертикали
            if (direction.y > 0)
            {
                var newY = --_y;

                if (newY < 0)
                {
                    return;
                }

                IsDerectionFound.Invoke(y, newY, false, _items);
            }
            else
            {
                var newY = ++_y;

                if (newY >= _items.GetLength(1))
                {
                    return;
                }

                IsDerectionFound.Invoke(y, newY, false, _items);
            }
        }
    }
}