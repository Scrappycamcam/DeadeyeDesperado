using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoading : MonoBehaviour
{

    public int area;

    public bool Active;

    public bool loadArea;
    public bool loadPrevious;
    public bool loadNext;

    public GameObject[] Areas;

    private void Awake()
    {
        SetAreaObjects();
    }

    private bool loading = false;

    public void SetArea(int newArea)
    {
        if (!loading)
        {
            loading = true;
            area = newArea;
            Debug.Log(newArea);
            SetAreaObjects();
        }
    }

    private void SetAreaObjects()
    {
        for (int i = 0; i < Areas.Length; i++)
        {
            if ((i == area - 1 && loadPrevious) || (i == area && loadArea) || (i == area + 1 && loadNext))
            {
                Areas[i].SetActive(true);
            }
            else
            {
                Areas[i].SetActive(false);
            }
        }
        loading = false;
    }

}
