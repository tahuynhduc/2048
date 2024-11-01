using UnityEngine;

public class MoveUp:MoveHandle
{
    public MoveUp(int i, int j) : base(i, j)
    {
        CheckMatchCount();
    }
    protected sealed override void CheckMatchCount()
    {
        IUpdateNumber ICurrentNumber;
        IUpdateNumber INextNumber;
        for (var i = 0; i < rowCount; i++)
        {
            Number currentNumber = null;
            for (var j = 0; j < colCount; j++)
            {
                if (MatrixNumbers[j, i].IsNull()) continue;
                if (currentNumber?.numberValue == MatrixNumbers[j, i].numberValue)
                {
                    var nextNumber = MatrixNumbers[j, i];
                    ICurrentNumber = currentNumber;
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
        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < colCount; j++)
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
        var nextRow = i + 1;
        var maxRow = MatrixNumbers.GetLength(0) - 1;
        return IsOutOfArray(nextRow)? MatrixNumbers[maxRow,j ] : MatrixNumbers[nextRow, j];
    }
    protected override bool IsOutOfArray(int i)
    {
        return i >= colCount-1;
    }
}