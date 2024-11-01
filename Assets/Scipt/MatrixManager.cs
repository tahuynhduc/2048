using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MatrixManager
{
    public Number[,] MatrixNumbers;
    private MoveHandle _moveHandle;
    public MatrixManager(int colCount,int rowCount)
    {
        MatrixNumbers = new Number[colCount, rowCount];
    }

    public void Log()
    {
        
    }
    public bool GetMoveDirection(TouchDirection direction)
    {
        _moveHandle = FactoryUtility.GetMoveDirection(direction);
        return _moveHandle != null;
    }
    public void Move()
    {
        _moveHandle?.Move();
    }

    public bool CheckMove(int i,int j)
    {
        return !CanMoveLeft(i, j) && !CanMoveRight(i, j) && !CanMoveUp(i, j) && !CanMoveDown(i, j);
    }
    private bool CanMoveLeft(int i,int j)
    {
        if (j - 1 < 0) return false;
        var leftNumber = MatrixNumbers[i, j - 1];
        var currentNumber =  MatrixNumbers[i, j];
        return leftNumber.IsNull() || currentNumber.CanMove(leftNumber.numberValue);
    }
    private bool CanMoveRight(int i,int j)
    {
        if (j + 1 > MatrixNumbers.GetLength(1)-1) return false;
        var rightNumber = MatrixNumbers[i, j + 1];
        var currentNumber =  MatrixNumbers[i, j];
        return rightNumber.IsNull() || currentNumber.CanMove(rightNumber.numberValue);
    }
    private bool CanMoveUp(int i,int j)
    {
        if (i - 1 < 0) return false;
        var upNumber = MatrixNumbers[i-1, j];
        var currentNumber =  MatrixNumbers[i, j];
        return upNumber.IsNull() || currentNumber.CanMove(upNumber.numberValue);
    }
    private bool CanMoveDown(int i,int j)
    {
        if (i + 1 > MatrixNumbers.GetLength(1)-1) return false;
        var downNumber = MatrixNumbers[i+1, j];
        var currentNumber =  MatrixNumbers[i, j];
        return downNumber.IsNull() || currentNumber.CanMove(downNumber.numberValue);
    }
}
[Serializable]
public class DataGame
{
    public List<Index> positionMatrix = new();
    public int score;
    private GameManager GameManager => SingletonManager.GameManager;
    public void UpdateMatrix(Index index)
    {
        positionMatrix.Add(index);
    }
    public void Save(Number[,] numbers)
    {
        var key = $"{numbers.GetLength(0)}x{numbers.GetLength(1)}";
        score = GameManager.score;
        for (var i = 0; i < numbers.GetLength(0); i++)
        {
            for (var j = 0; j < numbers.GetLength(1); j++)
            {
                
                var newData = new Index(i,j,numbers[i, j].numberValue);
                UpdateMatrix(newData);
            }
        }
        var json = JsonUtility.ToJson(this);
        Debug.Log(json);
        PlayerPrefs.SetString(key,json);
        PlayerPrefs.Save();
    }

    public DataGame GetDataGame(int boardType)
    {
        var key = $"{boardType}x{boardType}";
        var jsonData = PlayerPrefs.GetString(key,null);
        if (string.IsNullOrEmpty(jsonData)) return null;
        return JsonUtility.FromJson<DataGame>(jsonData);
    }
}
[Serializable]
public class Index
{
    public int i;
    public int j;
    public int value;

    public Index(int i,int j,int value)
    {
        this.i = i;
        this.j = j;
        this.value = value;
    }
}