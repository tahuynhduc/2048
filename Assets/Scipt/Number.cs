using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Number : MonoBehaviour,IUpdateNumber
{
    public int numberValue;
    [SerializeField] private Image color;
    [SerializeField] private Text valueText;
    public void UpdateNumber(Color newColor,int numberValue)
    {
        this.numberValue += numberValue;
        color.color = newColor;
        valueText.text = this.numberValue.ToString();
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
}
public interface IUpdateNumber
{
    public void UpdateNumber(Color newColor, int numberValue);
}


