using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Crystal_Controller : MonoBehaviour
{
	GameObject[] redFind;
	GameObject[] blueFind;
	struct Crystal {
		public GameObject g;
		public int i;
	}
	List<Crystal> red;
	List<Crystal> blue;

    public GameObject blocker;
    public GameObject[] doorCrystals;
    public GameObject unblocker;


    private CollectionItemSet BlockerSet;
    private UniqueID uniqueID;
    private GameObject FPSPlayer;
    
    
    // Start is called before the first frame update

    void Start()
    {
        FPSPlayer = Player.Instance.gameObject;
        doorCrystals = new GameObject[6];
        red = new List<Crystal>();
		blue = new List<Crystal>();
		redFind = GameObject.FindGameObjectsWithTag("red");
		blueFind = GameObject.FindGameObjectsWithTag("blue");
		switch (redFind.Length)
		{
			case 0:
				Debug.Log("No red Crystals were found");
				break;
			case 6:
				for(int i=0; i<redFind.Length; i++) 
				{
					addToList(redFind[i], redFind[i].GetComponent<crystal>().seqNum);
				}
				checkIfSame();
				break;
		}
	    if(blueFind.Length < 0) 
		{
			//Do nothing
			Debug.Log("No blue crystals found.");
		}
		else if(blueFind.Length > 0) 
		{
			for(int i=0; i<blueFind.Length; i++) 
			{
				addToList(blueFind[i], blueFind[i].GetComponent<crystal>().seqNum);
			}
			checkIfSame();
        }
        for (int i = 0; i < 6; i++)
        {
            var g = blocker.transform.GetChild(0).GetChild(i).gameObject;
            if (g)
            {
                doorCrystals[i] = g;
            }
        }
        bluePuzzle();
    }
	
	void addToList(GameObject newCrystal, int seqNum) 
	{
		if(newCrystal.tag == "red") 
		{
			red.Add(new Crystal() { g = newCrystal, i = seqNum});
		}
		else if(newCrystal.tag == "blue")
		{
			blue.Add(new Crystal() { g = newCrystal, i = seqNum});
		}		
	}
	
	void checkIfSame() 
	{
		for(int i=0; i<red.Count; i++) 
		{
			int j = i+1;
			while(j < red.Count)
			{
				if(red[j].i == red[i].i) //check if two crystals have the same index in a sequence.
				{
					Debug.LogError("Two or more crystals in the red puzzle have the same index in the sequence");
				}
				j++;
			}
		}
		for(int i=0; i<blue.Count; i++) 
		{
			int j = i+1;
			while(j < blue.Count)
			{
				if(blue[j].i == blue[i].i) //check if two crystals have the same index in a sequence.
				{
					Debug.LogError("Two or more crystals in the blue puzzle have the same index in the sequence");
				}
				j++;
			}
		}
	}
	
	public bool redPuzzle() 
	{
        Debug.Log("Red Puzzle Check");
		int i, j, k, l, x;
		int numHit = 0;
		GameObject p, q;
		for (i=0; i<red.Count; i++)
		{
			k = red[i].i; //crystal's integer at i, should be equal to i
			p = red[i].g; //corresponding game object
            //Debug.Log(i + " " + p);
			if(p.GetComponent<crystal>().hit) //if the crystal has been hit
			{
				for(j=0; j<red.Count; j++) //check through all the crystals
				{
					l = red[j].i; 
					q = red[j].g;
                    //Debug.Log(l + " " + k);
					if(l<k && q.GetComponent<crystal>().hit == false) //if a crystal later in the sequence is hit but crystals before it in the sequence haven't been hit reset the puzzle
					{
                        ResetRed();
						numHit =  0;
                        return false;
                    }
				}
			}
		}
		for(x=0; x<red.Count; x++)
		{
			if(red[x].g.GetComponent<crystal>().hit == true)
			{
				numHit++;
                Debug.Log(numHit);
            }
		}
        Debug.Log(red.Count);
		if(numHit == red.Count && numHit != 0) //you beat the puzzle, open next area.
		{
            Debug.Log("Puzzle Complete");
            //for(m=0; m<red.Count; m++)
            //{
            //	Destroy(red[m].g);
            //}
			red.Clear();
            BlockerSet.BlockersTurnedOff.Add(uniqueID.ID);
            blocker.SetActive(false);
		}
        return true;
	}

    public void ResetRed()
    {
        for (int z = 0; z < red.Count; z++) //resetting the puzzle
        {
            red[z].g.SetActive(true);
            red[z].g.GetComponent<crystal>().hit = false;
            red[z].g.GetComponent<crystal>().once = false;
            red[z].g.GetComponent<crystal>().StartCoroutine("LightBright");
            //Player.Instance.GetComponentInChildren<Gun>().bulletHit(false, 1000); //reset player combo
        }
        Player.Instance.GetComponentInChildren<Gun>().ResetCombo();
    }
	public void bluePuzzle () 
	{
		int j = 0;
		for(int i=0; i<blue.Count; i++)
		{
			if(blue[i].g.GetComponent<crystal>().hit == true) {
				j++;
			}
		}
        FPSPlayer.GetComponentInChildren<Gun>().setCrystalTracker(j);
        for(int i = 0; i < j; i++)
        {
            doorCrystals[i].GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(1f,1f,1f,1f));
        }
		if(j == blue.Count) //you beat the puzzle, open next area.
		{
            //for(int i=0; i<blue.Count; i++)
            //{
            //	Destroy(blue[i].g);
            //}
            Debug.Log("TurnOFYOURBLOCKER!!!!!!!!!");
            FPSPlayer.GetComponent<PlayerInforamtionSave>().SavedBlocker();
            blue.Clear();

            if (blocker.gameObject.name == "WellDoor" || blocker.gameObject.name == "EntranceDoor")
            {
                StartCoroutine(DoorOpen(true));
            }
            else
            {
                blocker.SetActive(false);
            }
            
            if (unblocker)
            {
                unblocker.SetActive(true);
            }
        }
	}

    public float doorOpenSpeed;
    public float doorOpenDistance;
    public Vector3 doorOpenDirection;

    private IEnumerator DoorOpen(bool isWell)
    {
        float d = 0;
        while(d <= doorOpenDistance)
        {
            if (isWell)
            {
                //blocker.gameObject.transform.Rotate(blocker.transform.GetChild(1).up, doorOpenSpeed*Time.deltaTime);
            }
            blocker.gameObject.transform.Translate(doorOpenDirection*doorOpenSpeed*Time.deltaTime);
            d += doorOpenSpeed * Time.deltaTime;
            yield return null;
        }
    }
    
}
