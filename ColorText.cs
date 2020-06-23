using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class ColorText : MonoBehaviour
{
    [System.Serializable]
    public class ColoredString
    {
        [SerializeField]public string content;
        [SerializeField]public Color stringColor;
    }

    public Text textBox;

    [SerializeField] public ColoredString[] BoxContents;

    private string text = "";

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        text = "";
        foreach (ColoredString c in BoxContents)
        {
            text += "<color=#" + ColorUtility.ToHtmlStringRGBA(c.stringColor) + ">" + c.content + "</color>";
        }
        textBox.text = text;
    }
}
