using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class PlayerInforamtionSave : MonoBehaviour
{
    //Get the name of the level
    //Must be in Scene to work 
    public string LevelName;
    public GameObject Blocker;
    private CollectionItemSet GatherBlockers;
    private UniqueID uniqueID;
    public List<string> Blockes { get; set; } = new List<string>();

    public bool loadSaves = true;

    private void Awake()
    {
        //LOAD the blockers that have been saved
        Load();
        uniqueID = GetComponent<UniqueID>();
        SaveGameEvents.SaveInitiated += Save;
        Load();
        //find the blocker in the scene
        Blocker = GameObject.FindGameObjectWithTag("Blocker");
        
        /*GatherBlockers.BlockersTurnedOff.Add(Blocker.ToString());
        GatherBlockers = FindObjectOfType<CollectionItemSet>();
        if (GatherBlockers.BlockersTurnedOff.Contains(uniqueID.ID))
        {

        }
        */
    }
    private void Start()
    {


        if (loadSaves)
        {
            //gets the name of the scene
            LevelName = SceneManager.GetActiveScene().name;
            //Puts the Save function so it can save.
            if (Blocker != null)
            {
                //Check if the blocker name and the level is in the saved list
                if (Blockes.Contains((Blocker.name + LevelName)))
                {
                    //Turbn off the blocker if it does.
                    Blocker.SetActive(false);
                    Debug.Log("THE BLCOKERS BE GONE YAY WE CAN GET THROUGH");
                    return;
                }
                else
                {
                    return;
                }
            }
        }


    }

    public void SavedBlocker()
    {
        // GatherBlockers.CollectedItems.Add(uniqueID.ID);
        //GatherBlockers.BlockersTurnedOff.Add(Blocker.ToString());
        //Blockes.Add(uniqueID.ID);
        //check the blocker isn't already in list if not add it to the list when completed. 
        if(Blockes.Contains((Blocker.name + LevelName)) && loadSaves){

            Debug.Log("Already There");
            Blocker.SetActive(false);
            return;
        }
        else
        {
            Blockes.Add(Blocker.name + LevelName);

        }
        Debug.Log("SAVED THE BLOCKERRR!!!!!!!!!!!!!!");
        //BlockerSet.BlockersTurnedOff.Add(uniqueID.ID);
        //save the blockers in the list
        SaveLoad.Save(Blockes, "BlockersOff");
    }
    void Save()
    {
        //adds the save name as a string to the saving system
        //save the name of the last level name/
        SaveLoad.Save(LevelName, "NameofLevel");

    }
    void Load()
    {
        if (SaveLoad.SaveExists("BlockersOff"))
        {
            //load the blocers list of blockers that had been tunred and add it to the list
            Blockes = SaveLoad.Load<List<string>>("BlockersOff");

        }
        else
        {
            return;
        }
    }

}
