using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Number : MonoBehaviour,IUpdateNumber
{
    public int numberValue;
    [SerializeField] private Image color;
    [SerializeField] private Text valueText;
    private GameManager GameManager => SingletonManager.GameManager;
    public void UpdateNumber(int newNumber)
    {
        numberValue += newNumber;
        color.color = GameManager.GetColor(numberValue);
        valueText.text = numberValue.ToString();
        if (IsNull())valueText.text = null;
    }
    
    public bool CanMove(int currentNumber)
    {
        return numberValue == currentNumber;
    }

    public bool IsNull()
    {
        return numberValue == 0;
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
public interface IUpdateNumber
{
    public void UpdateNumber(int newNumber);
}


