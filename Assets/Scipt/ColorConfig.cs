using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Data", menuName = "Color Data")]
public class ColorConfig : ScriptableObject
{
    [SerializeField] private List<ColorData> colorData;
    private Dictionary<int, Color> _dictColor;

    private Dictionary<int,Color> DictColor
    {
        get
        {
            if (_dictColor == null) InitData();
            return _dictColor;
        }
        set => _dictColor = value;
    }

    public Color GetColor(int number)
    {
        if (DictColor.ContainsKey(number)) return DictColor[number];
        return DictColor.Values.Last();
    }
    private void InitData()
    {
        _dictColor = new Dictionary<int, Color>();
        foreach (var item in colorData)
        {
            _dictColor.Add(item.key,item.color);
        }
    }
}