using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    private Bridge[] children;

    void Awake()
    {
        children = GetComponentsInChildren<Bridge>();
    }

    public void Start()
    {
        foreach (Bridge g in children)
        {
            g.GetComponent<Rigidbody>().useGravity = true;
            g.GetComponent<Rigidbody>().isKinematic = false;
            g.GetComponent<Collider>().enabled = true;
        }
    }

    public IEnumerator Activate()
    {
        //Debug.Log("Activated controller");
        foreach (Bridge g in children)
        {
            g.GetComponent<Rigidbody>().useGravity = false;
            g.GetComponent<Collider>().enabled = false;
        }

        var time = Time.time;

        while (children[0].transform.position != children[0].startPosition)
        {
            foreach (Bridge g in children)
            {
                g.transform.position = Vector3.Lerp(g.transform.position, g.startPosition, Time.deltaTime*5);
                g.transform.rotation = Quaternion.Lerp(g.transform.rotation, g.startRotation, Time.deltaTime*5);
                //Debug.Log(g.transform.position);
                if(Vector3.Distance(children[0].transform.position, children[0].startPosition) < 1)
                {
                    g.transform.position = g.startPosition;
                    g.transform.rotation = g.startRotation;
                }
            }
            if(time + 2f < Time.time)
            {
                break;
            }
            yield return null;
        }
        //Debug.Log("Past Move");
        foreach (Bridge g in children)
        {
                g.transform.position = g.startPosition;
                g.transform.rotation = g.startRotation;
        }

        foreach (Bridge g in children)
        {
            g.GetComponent<Rigidbody>().isKinematic = true;
            g.GetComponent<Collider>().enabled = true;
        }
    }
}
