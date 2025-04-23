using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeScrollView : MonoBehaviour
{
    [SerializeField] private RectTransform _panelScrollview;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private int _hozCount = 10;
    [SerializeField] private int _verCount = 3;
    // Only change Spacing or Contraint in Grid

    private void Start()
    {
        //StartCoroutine(ResizeNextFrame());
        ResizeHoz();
        ResizeVer();
    }

    IEnumerator ResizeNextFrame()
    {
        yield return null;
        ResizeHoz();
        ResizeVer();
    }

    private void ResizeHoz()
    {
        float width = _panelScrollview.rect.width;
        float totalPadding = _grid.padding.left + _grid.padding.right;
        float totalSpacing = _grid.spacing.x * (_hozCount - 1);

        float cellWidth = (width - totalPadding - totalSpacing) / _hozCount;
        _grid.cellSize = new Vector2(cellWidth, _grid.cellSize.y);
    }

    private void ResizeVer()
    {
        float height = _panelScrollview.rect.height;
        float totalPadding = _grid.padding.top + _grid.padding.bottom;
        float totalSpacing = _grid.spacing.y * (_verCount - 1);

        float cellHeight = (height - totalPadding - totalSpacing) / _verCount;
        _grid.cellSize = new Vector2(_grid.cellSize.x, cellHeight);
    }
}
