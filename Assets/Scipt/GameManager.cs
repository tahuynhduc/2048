using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : TemporaryMonoSingleton<GameManager>
{
    [SerializeField] private GameObject prefab;
    private BoardManager BoardManager => BoardManager.Instance;

    public bool isSpawnNumber;

    private void Update()
    {
        if (isSpawnNumber) SpawnNumber();
    }

    private void Start()
    {
        var col = BoardManager.matrixManager.MatrixNumbers.GetLength(0);
        var row = BoardManager.matrixManager.MatrixNumbers.GetLength(1);
        for (var i = 0; i < col; i++)
        {
            IUpdateNumber number;
            for (var j = 0; j < row; j++)
            {
                var obj = Instantiate(prefab, prefab.transform.position, Quaternion.identity,
                    BoardManager.transform);
                obj.SetActive(true);
                BoardManager.matrixManager.MatrixNumbers[i, j] = obj.GetComponent<Number>();
                number = BoardManager.matrixManager.MatrixNumbers[i, j];
                number.UpdateNumber(Color.white, 0);
            }
            if(i>2) continue;
            var randomIndex = this.RandomRange(0, row);
            number = BoardManager.matrixManager.MatrixNumbers[i, randomIndex];
            number.UpdateNumber(Color.yellow, 2);
        }
    }
    
    private void SpawnNumber()
    {
        if (IsEndGame())
        {
            // EventManager.OnEndGame();
            return;
        }
        var col = BoardManager.matrixManager.MatrixNumbers.GetLength(0);
        var row = BoardManager.matrixManager.MatrixNumbers.GetLength(1);
        var randomCol = this.RandomRange(0, col);
        var randomRow = this.RandomRange(0, row);
        var currentNumber = BoardManager.matrixManager.MatrixNumbers[randomCol, randomRow];
        if (currentNumber.numberValue != 0)
        {
            SpawnNumber();
            return;
        }
        isSpawnNumber = false;
        BoardManager.OnMove(TouchDirection.None);
        IUpdateNumber INumber = BoardManager.matrixManager.MatrixNumbers[randomCol, randomRow];
        INumber.UpdateNumber(Color.yellow, 2);
    }
    private bool IsEndGame()
    {
        var col = BoardManager.matrixManager.MatrixNumbers.GetLength(0);
        var row = BoardManager.matrixManager.MatrixNumbers.GetLength(1);
        for (var i = 0; i < col; i++)
        {
            for (var j = 0; j < row; j++)
            {
                var currentNumber = BoardManager.matrixManager.MatrixNumbers[i, j];
                if (currentNumber.IsNull())
                {
                    return false;
                }
            }
        }

        return true;
    }
}

public class EventManager
{
    public static Action IsEndGame = delegate { };
    public static void OnEndGame()
    {
        IsEndGame.Invoke();
    }
}