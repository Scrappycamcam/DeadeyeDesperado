using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeActivator : MonoBehaviour
{
	[SerializeField]private BridgeController bridgeToActivate;

    [SerializeField] private BridgeController bridgeToDeactivate;

    private bool Activated = false;

    private void Start()
    {
        CrystalLight();
        if (bridgeToDeactivate)
        {
            bridgeToDeactivate.StartCoroutine("Activate");
        }
    }

    public void Activate()
	{
        if (bridgeToActivate)
        {
            if (!Activated)
            {
                Debug.Log("Activated");
                bridgeToActivate.StartCoroutine("Activate");
                Activated = true;
            }
            else
            {
                bridgeToActivate.StopCoroutine("Activate");
                bridgeToActivate.Start();
                Activated = false;
            }
            GetComponentInChildren<AudioSource>().Play();
            CrystalLight();
        }
        if (bridgeToDeactivate)
        {
            if (!Activated)
            {
                bridgeToDeactivate.StopCoroutine("Activate");
                bridgeToDeactivate.Start();
                if (!bridgeToActivate)
                {
                    Activated = true;
                }
            }
            else
            {
                Debug.Log("DeActivated");
                bridgeToDeactivate.StartCoroutine("Activate");
                if (!bridgeToActivate)
                {
                    Activated = false;
                }
            }
        }
    }

    private void CrystalLight()
    {
        transform.GetChild(0).GetComponentInChildren<Light>().enabled = Activated;
    }

}
