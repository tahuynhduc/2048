using System;

[Serializable]
public class MatrixManager
{
    public Number[,] MatrixNumbers;
    private BoardManager BoardManager => SingletonManager.BoardManager;
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
}