using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    public Vector3 direction;
    public bool consistentFlip = false;
    public float speed;
    public float distanceToTravel;
	private float originalSpeed;
    public Vector3 offset;
    

    private Vector3 startPos;
	
	private float maxSpeed, maxY;

    public ParticleSystem[] sparks;

    private void Start()
    {
		originalSpeed = speed;
		maxSpeed = 25f;
		maxY = 1.7f;
        startPos = transform.localPosition;
        transform.localPosition += offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 travel = direction * speed *Time.deltaTime;
        transform.localPosition += travel;
        if (Vector3.Distance(startPos, transform.localPosition) >= distanceToTravel)
        {
            if (!consistentFlip)
            {
                transform.localPosition = startPos;
            }
            else if(consistentFlip)
            {
                direction = -direction;
                startPos = transform.localPosition;
            }
        }
		if(speed<originalSpeed)
		{
			if(speed>=0)
			{
				Debug.Log(speed);
				if(speed == 0)
				{
					transform.GetChild(1).transform.localPosition = new Vector3(transform.GetChild(1).transform.localPosition.x, 
					maxY, transform.GetChild(1).transform.localPosition.z);
				}
				else
				{
					transform.GetChild(1).transform.localPosition = new Vector3(transform.GetChild(1).transform.localPosition.x, 
					(maxY/speed)+1.069f, transform.GetChild(1).transform.localPosition.z);
				}
			}
		}
        foreach (ParticleSystem p in sparks)
        {
            p.emissionRate = speed;
        }
    }
}
