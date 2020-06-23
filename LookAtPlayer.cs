using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform pTransform;

    // Start is called before the first frame update
    void Start()
    {
        pTransform = Player.Instance.GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(pTransform);
    }
}
