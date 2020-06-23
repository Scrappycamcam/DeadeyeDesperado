using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CheckpointSave : MonoBehaviour
{
    public bool RedChallengeCompelete1;
    public bool GreenChallengeCompelete1;
 
    public GameObject Player;
    public int SaveSlots;
    public int SavedNumber;
    private void Awake()
    {
        SavedNumber = PlayerPrefs.GetInt("CaseNumberOfSave", 1);
        SaveSlots = PlayerPrefs.GetInt("SaveSlots", 1);
        LoadPlayer();
        CheckPointCheck();
    }
    // Start is called before the first frame update
    void Start()
    {

        ChallengeFinish();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPointCheck();
    }
    private void LateUpdate()
    {
        SavePlayer();

        //NumberChecked();
    }
    public void CheckPointCheck()
    {
        
            
        RedChallengeCompelete1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().RedChallengeCompelete1;
        GreenChallengeCompelete1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GreenChallengeCompelete1;

               
        NumberChecked();
    }
    public void NumberChecked()
    {
        
                if(RedChallengeCompelete1 == true)
                {
                    SavedNumber = 1;
                }
        else
        {
            SavedNumber = 0;
        }
                if (GreenChallengeCompelete1 == true)
                {
                    SavedNumber = 2;
                }
                
    }

    public void SavePlayer()
    {
        PlayerPrefs.SetInt("CaseNumberOfSave", SavedNumber);
      
         PlayerPrefs.SetInt("RedCheckpointSave1", RedChallengeCompelete1 ? 1 : 0);
         PlayerPrefs.SetInt("GreenCheckpointSave1", GreenChallengeCompelete1 ? 1 : 0);


        
    }
    private void LoadPlayer()
    {
        SavedNumber = PlayerPrefs.GetInt("CaseNumberOfSave", 1);
        

        
        RedChallengeCompelete1 = PlayerPrefs.GetInt("RedCheckpointSave1") == 1;
        GreenChallengeCompelete1 = PlayerPrefs.GetInt("GreenCheckpointSave1") == 1;

        
    }
    public void ChallengeFinish()
    {
        if (SceneManager.GetActiveScene().name == "RedBullet_BlockOut")
        {


            if (SavedNumber == 1)
            {
              //Player.transform.position = new Vector3(8.61f, 1.81f, 26.56f);
            }

        }
        else if (SceneManager.GetActiveScene().name == "GreenBullet_Blockout")
        {

            if (SavedNumber == 2)
            {
            // Player.transform.position = new Vector3(55.04f, 3.115f, 157.83f);
            }

        }
    }


}
