using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class crystal : MonoBehaviour
{
	public enum color{red, blue, overworld};
	[Tooltip("The color of the puzzle this crystal is for, options are red, blue, or overworld for no color")]
	public color Color;
	[Tooltip("Was this crystal hit or not?")]
	public bool hit = false; //Was this crystal hit or not?
	[Tooltip("The index this crystal comes in the sequence.")]
	public int seqNum;
	GameObject g;
	[HideInInspector]
	public bool once;

    public bulletType myColor;

    private Light myLight;
    //private SpriteRenderer myRune;
    private float lightMedium = .5f;

    void Awake() 
	{
		switch(Color)
		{
			case color.red:
				gameObject.tag = "red";
				break;
			case color.blue:
				gameObject.tag = "blue";
				break;
			case color.overworld:
				gameObject.tag = "overworld";
				break;
		}
        myLight = GetComponentInChildren<Light>();
        //myRune = GetComponentInChildren<SpriteRenderer>();
	}
    // Start is called before the first frame update
    void Start()
    {		
		checkSeq();
    }
	void checkSeq()
	{
		if((seqNum > 6 || seqNum < 1) && Color != color.overworld) 
		{
			Debug.LogError("seqNum MUST BE BETWEEN 1 AND THE NUMBER OF CRYSTALS IN THE SEQUENCE");
        }
        else
        {
            if (Player.Instance.GetComponent<PlayerInforamtionSave>().loadSaves)
            {
                var saveName = SceneManager.GetActiveScene().name + gameObject.name;
                if (SaveLoad.SaveExists(saveName))
                {
                    hit = SaveLoad.Load<bool>(saveName);
                }
            }
        }
        if (hit)
        {
            Destroy(this.GetComponent<MeshCollider>());
            Destroy(this.GetComponent<MeshRenderer>());
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
	}
	public bool checkHit()
    {
        Debug.Log("TryBeHit");
        if (!once && isActive)
        {
            hit = true;
            once = true;
            SaveLoad.Save<bool>(hit, SceneManager.GetActiveScene().name + gameObject.name);
            GetComponent<AudioSource>().Play();
            if (Color == color.overworld)//not part of a puzzle
            {
                Destroy(this.GetComponent<MeshCollider>());
                Destroy(this.GetComponent<MeshRenderer>());
                Destroy(gameObject.transform.GetChild(0).gameObject);

                return true;
            }
			else if(Color == color.red)//part of the puzzle
			{
                //indicate its been hit.
                Debug.Log("Been Hit");
                g = GameObject.FindWithTag("crystal_controller");
                if (g.GetComponent<Crystal_Controller>().redPuzzle())
                {
                    Destroy(this.GetComponent<MeshCollider>());
                    Destroy(this.GetComponent<MeshRenderer>());
                    Destroy(gameObject.transform.GetChild(0).gameObject);
                    return true;
                    //gameObject.SetActive(false);
                }
                else
                {
                    return false;
                }
            }
			else if(Color == color.blue)
            {
                Debug.Log("Been Hit");
                g = GameObject.FindWithTag("crystal_controller");
                g.GetComponent<Crystal_Controller>().bluePuzzle();

                Destroy(this.GetComponent<MeshCollider>());
                Destroy(this.GetComponent<MeshRenderer>());
                Destroy(gameObject.transform.GetChild(0).gameObject);

                return true;
            }
		}
        return false;
	}

    private bool isActive = true;
}