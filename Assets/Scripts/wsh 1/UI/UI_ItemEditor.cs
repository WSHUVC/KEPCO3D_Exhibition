using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class UI_ItemEditor : MonoBehaviour
{
    TextMeshProUGUI[] texts;
    public Color textColor;
    public float currentFontSize;
    public float fontSizeMax;
    public bool set;
    private void OnEnable()
    {
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        if (texts.Length > 0)
        {
            textColor = texts[0].color;
            fontSizeMax = texts[0].fontSizeMax;
            currentFontSize = texts[0].fontSize;
        }
    }

    private void Update()
    {
        if (set)
        {
            set = false;
            foreach(var t in texts)
            {
                t.color = textColor;
                t.fontSize = currentFontSize;
                t.fontSizeMax = fontSizeMax;
            }
        }
    }
}
