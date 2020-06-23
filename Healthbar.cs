using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    [SerializeField] private Image[] HealthBars = null;

    private bool found = false;

    public void UpdateBar(float percent)
    {
        if (percent > 1)
        {
            percent = 1;
        }
        if(percent < 0)
        {
            percent = 0;
        }
        float amount = 1f / HealthBars.Length;
        //Debug.Log(amount);
        float count = amount;
        foreach (Image i in HealthBars)
        {
            if (percent >= count)
            {
                i.fillAmount = 1f;
            }
            else if (percent < count && !found)
            {
                var diff = ((amount - (count - percent)) / amount);
                //Debug.Log(diff);
                i.fillAmount = diff;
                found = true;
            }
            else if (found)
            {
                i.fillAmount = 0f;
            }
            count += amount;
        }
        found = false;
    }

    public void UpdateColors(Color c)
    {
        foreach (Image i in HealthBars)
        {
            i.color = c;
        }
    }
}
