using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private LayerMask _targetForRaycast;
    [SerializeField] private Grid _grid;

    public Action<Vector3Int, Vector3Int> IsDerectionFound;

    private int _x;
    private int _y;

    private Vector2 _clickPosition;
    private Vector2 _endPosition;

    private RaycastHit2D _hit;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _hit = Physics2D.Raycast(_clickPosition, Vector2.zero, Mathf.Infinity, _targetForRaycast);

            if (_hit.collider == null) return;
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

        var firstItemIndex = _grid.WorldToCell(_hit.transform.position);
        var secondItemIndex = _grid.WorldToCell(_hit.transform.position);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Движение по горизонтали
            if (direction.x > 0)
            {
                secondItemIndex.x--;
                IsDerectionFound.Invoke(firstItemIndex, secondItemIndex);
            }
            else
            {
                secondItemIndex.x++;
                IsDerectionFound.Invoke(firstItemIndex, secondItemIndex);
            }
        }
        else
        {
            // Движение по вертикали
            if (direction.y > 0)
            {
                secondItemIndex.y--;
                IsDerectionFound.Invoke(firstItemIndex, secondItemIndex);
            }
            else
            {
                secondItemIndex.y++;
                IsDerectionFound.Invoke(firstItemIndex, secondItemIndex);
            }
        }
    }
}