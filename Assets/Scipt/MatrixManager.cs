using System;

[Serializable]
public class MatrixManager
{
    public Number[,] MatrixNumbers;
    private MoveHandle _moveHandle;
    public MatrixManager(int colCount,int rowCount)
    {
        MatrixNumbers = new Number[colCount, rowCount];
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