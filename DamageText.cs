using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    Animator animator;
    Text damageText;
    public Transform sourcePosition;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        damageText = animator.GetComponent<Text>();
        RuntimeAnimatorController dir;
        switch (Random.Range(0,3)) //get a random direction for the damage text to fall in
        {
            case 0:
                dir = Resources.Load<RuntimeAnimatorController>("TextBounceRight");
                break;
            case 1:
                dir = Resources.Load<RuntimeAnimatorController>("TextBounceLeft");
                break;
            case 2:
                dir = Resources.Load<RuntimeAnimatorController>("TextBounceDown");
                break;
            default:
                dir = Resources.Load<RuntimeAnimatorController>("TextBounceRight");
                break;
        }

        animator.runtimeAnimatorController = dir;
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfos[0].clip.length);
    }

    public void SetText(string text, Color color)
    {
        animator = GetComponentInChildren<Animator>();
        damageText = animator.GetComponent<Text>();
        damageText.text = "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
    }

    private void Update()
    {
        if (sourcePosition)
        {
            transform.position = Camera.main.WorldToScreenPoint(sourcePosition.position + Vector3.up);
        }
    }
}
