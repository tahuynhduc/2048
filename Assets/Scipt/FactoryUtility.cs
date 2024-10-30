public static class FactoryUtility
{
    public static MoveHandle GetMoveDirection(TouchDirection direction)
    {
        var rowCount = BoardManager.Instance.rowCount;
        var colCount = BoardManager.Instance.colCount;
        switch (direction)
        {
            case TouchDirection.Up:
                return new MoveUp(rowCount, colCount);
            case TouchDirection.Down :
                return new MoveDown(rowCount, colCount);
            case TouchDirection.Left:
                return new MoveLeft(rowCount, colCount);
            case TouchDirection.Right:
                return new MoveRight(rowCount, colCount);
        }

        return null;
    }
}