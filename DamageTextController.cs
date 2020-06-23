using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    private static DamageText damageText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("NotPauseCanvas");
        if(!damageText)
            damageText = Resources.Load<DamageText>("DamageTextParent");
    }

    public static void CreateDamageText(string text, Transform location, Color textColor)
    {
        DamageText instance = Instantiate(damageText);
        instance.transform.position = Camera.main.WorldToScreenPoint(location.position + Vector3.up);
        instance.transform.SetParent(canvas.transform, false);
        instance.SetText(text, textColor);
        instance.sourcePosition = location;
    }
}
