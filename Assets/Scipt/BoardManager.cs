using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardManager : TemporaryMonoSingleton<BoardManager>
{
    private GameManager GameManager => SingletonManager.GameManager;
    
    public MatrixManager matrixManager;
    public int colCount;
    public int rowCount;
    protected override void Init()
    {
        matrixManager = new MatrixManager(colCount,rowCount);
    }
    private void Update()
    {
        matrixManager.Move();
    }
    public void OnMove(TouchDirection moveDirection)
    {
       var moveState= matrixManager.GetMoveDirection(moveDirection);
       if(moveState) StartCoroutine(WaitToSpawn());
    }

    private IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.isSpawnNumber = true;
    }
}