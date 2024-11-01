public abstract class MoveHandle
{
    protected Number[,] MatrixNumbers => SingletonManager.BoardManager.matrixManager.MatrixNumbers;
    
    protected int rowCount;
    protected int colCount;
    protected MoveHandle(int i,int j)
    {
        rowCount = i;
        colCount = j;
    }

    protected abstract void CheckMatchCount();
    public abstract void Move();
    protected abstract Number GetNextNumber(int i,int j);
    protected abstract bool IsOutOfArray(int j);
}