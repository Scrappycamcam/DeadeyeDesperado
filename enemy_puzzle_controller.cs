using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_puzzle_controller : MonoBehaviour
{
	public GameObject[] blockers;

    public int waves = 1;
    public Spawner[] enemySpawners;

    private bool encounterTriggered = false;

    public bool deactivateBlockers = false;

    //ublic int size;
    int count;

	void Awake()
	{
		count = 0;
        enemySpawners = GetComponentsInChildren<Spawner>();
	}
	public void incCount() 
	{
		count++;
		Debug.Log("IncCount " + count);
	}
	
	public void decCount() 
	{
		count--;
		Debug.Log("DecCount " + count);
		checkEmpty();
	}
    
	void checkEmpty()
	{
		if(count <= 0)
		{
            count = 0;
            waves--;
            if (waves <= 0)
            {
                Debug.Log("no more enemies");
                EndEncounter();
            }
            else
            {
                foreach(Spawner s in enemySpawners)
                {
                    s.numSpawned = 0;
                }
            }
		}
	}

    public void BeginEncounter()
    {
        if (!encounterTriggered)
        {
            encounterTriggered = true;
            foreach (GameObject g in blockers)
            {
                g.SetActive(!deactivateBlockers);
                foreach(Spawner s in enemySpawners)
                {
                    s.enabled = true;
                }
            }
        }
    }
    
    public void EndEncounter()
    {
        foreach (GameObject g in blockers)
        {
            g.SetActive(deactivateBlockers);
        }
    }
	
}
