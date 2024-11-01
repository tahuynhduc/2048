using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardManager : TemporaryMonoSingleton<BoardManager>
{
    private GameManager GameManager => SingletonManager.GameManager;

    public MatrixManager matrixManager;
    public int colRowCount;

    protected override void Init()
    {
        matrixManager = new MatrixManager(colRowCount, colRowCount);
    }

    private void Update()
    {
        matrixManager.Move();
    }

    public void SetUpGameplay()
    {
        if (matrixManager != null)
        {
            foreach (var number in matrixManager.MatrixNumbers)
            {
                number.DestroyObj();
            }
        }

        matrixManager = new MatrixManager(colRowCount, colRowCount);
    }

    public void OnMove(TouchDirection moveDirection)
    {
        var moveState = matrixManager.GetMoveDirection(moveDirection);
        if (moveState) StartCoroutine(WaitToSpawn());
    }

    private IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(0.2f);
        OnMove(TouchDirection.None);
        if (GameManager.IsEndGame())
        {
            EventManager.OnEndGame();
            yield break;
        }

        GameManager.SpawnNumber();
    }
}