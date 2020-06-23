using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveBars : MonoBehaviour
{
    public Material dissolveMat;


    public void DissolveMe()
    {
        StartCoroutine(Dissolve());
    }

    private IEnumerator Dissolve()
    {

        Debug.Log("Get Dissolved");
        gameObject.GetComponent<Renderer>().material = dissolveMat;
        gameObject.GetComponent<Renderer>().material.SetFloat("_TimeProperty", -1);
        for(float time = -1; time < 1; time += Time.deltaTime)
        {
            gameObject.GetComponent<Renderer>().material.SetFloat("_TimeProperty", time);
            yield return null;
        }
        Destroy(this.gameObject);
    }

}
