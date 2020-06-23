using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    private static TimeScaler worldTimeScale;

    public static TimeScaler Instance { get{ return worldTimeScale; } }

    private float totalTimeToSlow;

    private float curTime;

    [SerializeField] float slowestTimeScale = .3f;

    private void Awake()
    {
        if(worldTimeScale != this)
        {
            worldTimeScale = this;
        }
        totalTimeToSlow = 0f;
    }

    private void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= totalTimeToSlow)
        {
            Reset();
        }
        if (totalTimeToSlow > 0)
        {
            Time.timeScale = slowestTimeScale + (curTime / totalTimeToSlow)/(1-slowestTimeScale);
        }
    }

    private void Reset()
    {
        Time.timeScale = 1f;
        curTime = 0f;
        totalTimeToSlow = 0f;
    }

    public void slowTime(float time)
    {
        //if (totalTimeToSlow <= 0)
        {
            totalTimeToSlow = time;
            curTime = 0f;
            //Time.timeScale = slowestTimeScale + (1 - slowestTimeScale)/ 2f;
        }
        //else
        {
        //    totalTimeToSlow += time;
        }
    }
}
