using UnityEngine;
using UnityEngine.UI;

public class GridLayoutCustom : MonoBehaviour
{
    private GridLayoutGroup _gridLayout;
    private BoardManager BoardManager => SingletonManager.BoardManager;
    private int RowCount => BoardManager.rowCount;

    private void Start()
    {
        _gridLayout = GetComponent<GridLayoutGroup>();
        var wight = (Screen.width - 400) / RowCount;
        var space = _gridLayout.spacing.x;
        _gridLayout.constraintCount = RowCount;
        _gridLayout.cellSize = new Vector2(wight-space, wight-space);
    }
}