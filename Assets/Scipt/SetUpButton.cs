using UnityEngine;
using UnityEngine.UI;

public class SetUpButton : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    private BoardManager BoardManager => SingletonManager.BoardManager;
    private GameManager GameManager => SingletonManager.GameManager;
    public void SetUpGame()
    {
        BoardManager.colRowCount = int.Parse(inputField.text);
        EventManager.OnSetupGameplay();
        BoardManager.SetUpGameplay();
        GameManager.SpawnNumbers(false);
    }
}