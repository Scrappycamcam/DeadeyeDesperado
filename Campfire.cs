using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{

    public GameObject onFire;
    private GameObject notOnFire;

    private void Awake()
    {
        notOnFire = transform.Find("Not Burning").gameObject;

        onFire.SetActive(false);
    }


    public void LightMe()
    {
        StartCoroutine(BurnUp());
    }

    private IEnumerator BurnUp()
    {
        onFire.SetActive(true);
        notOnFire.SetActive(false);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
