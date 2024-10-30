using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;
    private void OnEnable()
    {
        EventManager.IsEndGame += OnShowEndGame;
    }

    private void OnDisable()
    {
        EventManager.IsEndGame -= OnShowEndGame;
    }

    private void OnShowEndGame()
    {
        endGamePanel.SetActive(true);
    }
}