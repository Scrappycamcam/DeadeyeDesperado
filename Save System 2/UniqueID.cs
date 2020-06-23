using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UniqueID : MonoBehaviour
{
    public string ID {get; private set; }
    private void Awake()
    {
        ID = transform.position.sqrMagnitude + "-" + name + transform.GetSiblingIndex();
        Debug.Log("ID FOR " + name + "is " + ID);
    }
}
