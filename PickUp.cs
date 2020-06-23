using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public GameObject pickUpObject;

    public void PickUpMe() {
        this.gameObject.SetActive(false);
        pickUpObject.SetActive(true);
    }

}
