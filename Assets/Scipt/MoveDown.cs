public class MoveDown:MoveHandle
{
    public MoveDown(int i, int j) : base(i, j)
    {
        CheckMatchCount();
    }
    protected sealed override void CheckMatchCount()
    {
        IUpdateNumber ICurrentNumber;
        IUpdateNumber INextNumber;
        for (var i = rowCount -1; i >=0 ; i--)
        {
            Number currentNumber = null;
            for (var j = colCount-1; j >=0 ; j--)
            {
                if (MatrixNumbers[j, i].IsNull()) continue;
                if (currentNumber?.numberValue == MatrixNumbers[j, i].numberValue)
                {
                    var nextNumber = MatrixNumbers[j, i];
                    ICurrentNumber = currentNumber;
                    GameManager.score += currentNumber.numberValue;
                    ICurrentNumber.UpdateNumber(currentNumber.numberValue);
                    INextNumber = nextNumber;
                    INextNumber.UpdateNumber(-nextNumber.numberValue);
                    //Debug.LogError($"i:{i}-----j:{j}");
                    break;
                }
                if (!MatrixNumbers[j, i].IsNull())
                {
                    currentNumber = MatrixNumbers[j, i];
                }
            }
        }
    }
    public override void Move()
    {
        IUpdateNumber ICurrentNumber;
        IUpdateNumber INextNumber;
        for (var i = rowCount -1; i >=0 ; i--)
        {
            for (var j = colCount-1; j >=0 ; j--)
            {
                var currentNumber = MatrixNumbers[i, j];
                var nextNumber = GetNextNumber(i,j);
                if (!currentNumber.IsNull()) continue;
                ICurrentNumber = currentNumber;
                ICurrentNumber.UpdateNumber(nextNumber.numberValue);
                INextNumber = nextNumber;
                INextNumber.UpdateNumber(-nextNumber.numberValue);
            }
        }
    }

    protected override Number GetNextNumber(int i, int j)
    {
        var nextRow = i - 1;
        return IsOutOfArray(nextRow) ? MatrixNumbers[0, j] : MatrixNumbers[nextRow, j];
    }
    protected override bool IsOutOfArray(int i)
    {
        return i <= 0;
    }
}