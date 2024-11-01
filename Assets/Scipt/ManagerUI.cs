using System;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Text scoreText;
    private GameManager GameManager => SingletonManager.GameManager;
    private void OnEnable()
    {
        EventManager.IsEndGame += OnShowEndGame;
    }

    private void Update()
    {
        scoreText.text = $"score: {GameManager.score}";
    }

    private void OnDisable()
    {
        EventManager.IsEndGame -= OnShowEndGame;
    }

    private void OnShowEndGame()
    {
        endGamePanel.SetActive(true);
    }

    public void OnClickSave()
    {
        GameManager.Save();
    }
}