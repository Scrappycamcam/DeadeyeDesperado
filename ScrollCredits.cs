using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCredits : MonoBehaviour
{
    private Animator CreditsAnimator;

    private void Awake()
    {
        CreditsAnimator = GetComponent<Animator>();
    }

    public void StartCredits()
    {
        CreditsAnimator.Play("Credits");
    }
}
