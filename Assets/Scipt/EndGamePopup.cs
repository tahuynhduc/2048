using UnityEngine;

public class EndGamePopup : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup.alpha = 0;
    }

    private void Update()
    {
        canvasGroup.alpha += Time.deltaTime;
    }

    public void ReStartGame()
    {
        EventManager.OnStartGame();
    }
}