using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{

    private GameObject fire;
    private GameObject light;

    private bool isLit = false;

    // Start is called before the first frame update
    void Awake()
    {
        fire = transform.Find("Fire Proper").gameObject;
        fire.SetActive(false);
        light = transform.Find("Point Light").gameObject;
    }
    
    public void LightMe()
    {
        if (isLit)
        {
            StopAllCoroutines();
        }
        LightLantern();
    }

    private void LightLantern()
    {
        if (!isLit)
        {
            isLit = true;
            light.SetActive(false);
            fire.SetActive(true);
        }
    }

}
