using UnityEngine;

public class MoveRight:MoveHandle
{
    public MoveRight(int i, int j) : base(i, j)
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
            for (var j = colCount-1; j >=0; j--)
            {
                if (MatrixNumbers[i, j].IsNull()) continue;
                if (currentNumber?.numberValue == MatrixNumbers[i, j].numberValue)
                {
                    var nextNumber = MatrixNumbers[i, j];
                    ICurrentNumber = currentNumber;
                    GameManager.score += currentNumber.numberValue;
                    ICurrentNumber.UpdateNumber(currentNumber.numberValue);
                    INextNumber = nextNumber;
                    INextNumber.UpdateNumber(-nextNumber.numberValue);
                    //Debug.LogError($"i:{i}-----j:{j}");
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
            for (var j = colCount-1; j >=0; j--)
            {
                var currentNumber = MatrixNumbers[i, j];
                var nextNumber = GetNextNumber(i, j);
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
        var colNext = j - 1;
        return IsOutOfArray(colNext) ? MatrixNumbers[i, 0] : MatrixNumbers[i, colNext];
    }

    protected override bool IsOutOfArray(int j)
    {
        return j <= 0;
    }
}