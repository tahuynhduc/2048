using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : TemporaryMonoSingleton<GameManager>
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private ColorConfig colorConfig;
    [SerializeField] private Transform transParent;
    private const int DefaultValue = 2;
    private BoardManager BoardManager => SingletonManager.BoardManager;
    private int ColRow => BoardManager.colRowCount;
    private int Row => BoardManager.colRowCount;
    public int score;
    private void OnEnable()
    {
        EventManager.StartGame += ReStartGame;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        SpawnNumbers();
    }

    private void OnDisable()
    {
        EventManager.StartGame -= ReStartGame;
    }

    private void ReStartGame()
    {
        score = 0;
        for (var i = 0; i < ColRow; i++)
        {
            for (var j = 0; j < Row; j++)
            {
                var number =  BoardManager.matrixManager.MatrixNumbers[i, j];
                IUpdateNumber INumber = BoardManager.matrixManager.MatrixNumbers[i, j];
                INumber.UpdateNumber(-number.numberValue);
            }
            if(i>1) continue;
            SpawnNumber();
        }
    }
    public void SpawnNumbers(bool isGetData = true)
    {
        IUpdateNumber number;
        if (isGetData)
        {
            DataGame dataGame = new DataGame();
            var data =  dataGame.GetDataGame(BoardManager.colRowCount);
            if (data == null)
            {
                SpawnNumbers(false);
                return;
            }
            score = data.score;
            foreach (var index in data.positionMatrix)
            {
                var obj = Instantiate(prefab, prefab.transform.position, Quaternion.identity, transParent);
                obj.SetActive(true);
                BoardManager.matrixManager.MatrixNumbers[index.i,index.j] = obj.GetComponent<Number>();
                number = obj.GetComponent<Number>();
                number.UpdateNumber(index.value);
            }
            return;
        }
        for (var i = 0; i < ColRow; i++)
        {
            for (var j = 0; j < Row; j++)
            {
                var obj = Instantiate(prefab, prefab.transform.position, Quaternion.identity, transParent);
                obj.SetActive(true);
                BoardManager.matrixManager.MatrixNumbers[i, j] = obj.GetComponent<Number>();
                number = BoardManager.matrixManager.MatrixNumbers[i, j];
                number.UpdateNumber(0);
            }
            if(i>1) continue;
            var randomIndex = this.RandomRange(0, Row);
            number = BoardManager.matrixManager.MatrixNumbers[i, randomIndex];
            number.UpdateNumber(DefaultValue);
        }
    }

    public void Save()
    {
        DataGame dataGame = new DataGame();
        dataGame.Save(BoardManager.matrixManager.MatrixNumbers);
    }
    public Color GetColor(int number)
    {
        return  colorConfig.GetColor(number);
    }
    public void SpawnNumber()
    {
        var isSpawn = false;
        foreach (var number in BoardManager.matrixManager.MatrixNumbers)
        {
            if(!number.IsNull()) continue;
            isSpawn = true;

        }
        if(!isSpawn) return;
        var randomCol = this.RandomRange(0, ColRow);
        var randomRow = this.RandomRange(0, Row);
        var currentNumber = BoardManager.matrixManager.MatrixNumbers[randomCol, randomRow];
        if (!currentNumber.IsNull())
        {
            SpawnNumber();
            return;
        }
        IUpdateNumber INumber = currentNumber;
        INumber.UpdateNumber(DefaultValue);
    }
    public bool IsEndGame()
    {
        var gameOverState = false;
        for (var i = 0; i < ColRow; i++)
        {
            for (var j = 0; j < Row; j++)
            {
                
                //khong the di chuyen
                gameOverState = true;
                if(!BoardManager.matrixManager.CheckMove(i, j))
                {
                    return false;
                }
            }
        }
        
        return gameOverState;
    }
}