using UnityEngine;

public class MoveLeft :MoveHandle
{
    public MoveLeft(int i, int j) : base(i, j)
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
                if (MatrixNumbers[i, j].IsNull()) continue;
                if (currentNumber?.numberValue == MatrixNumbers[i, j].numberValue)
                {
                    var nextNumber = MatrixNumbers[i, j];
                    ICurrentNumber = currentNumber;
                    ICurrentNumber.UpdateNumber(Color.white, currentNumber.numberValue);
                    INextNumber = nextNumber;
                    INextNumber.UpdateNumber(Color.white, -nextNumber.numberValue);                    
                    // Debug.LogError($"i:{i}-----j:{j}");
                    break;
                }
                if (!MatrixNumbers[i, j].IsNull())
                {
                    currentNumber = MatrixNumbers[i, j];
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
                ICurrentNumber.UpdateNumber(Color.white, nextNumber.numberValue);
                INextNumber = nextNumber;
                INextNumber.UpdateNumber(Color.white, -nextNumber.numberValue);
            }
        }
    }
    protected override Number GetNextNumber(int i, int j)
    {
        var colNext = j + 1;
        var maxCol = MatrixNumbers.GetLength(1) - 1;
        return IsOutOfArray(colNext)? MatrixNumbers[i, maxCol] : MatrixNumbers[i, colNext];
    }
    protected override bool IsOutOfArray(int j)
    {
        return j>=colCount-1;
    }
}