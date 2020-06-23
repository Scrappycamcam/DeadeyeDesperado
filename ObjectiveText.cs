using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ObjectiveText : MonoBehaviour
{
    public int SaveSlots;

    public Text ObjectTextCheckpoint;


    public int CheckSave1;
    public int CheckSave2;
    public int CheckSave3;

    public bool TurnOff;
    public bool StopCoRotuine;

    public int CurrentCheckPointNumber;

    public string RedObjective0;
    public string RedObjective1;
    public string RedObjective2;
    public string RedObjective3;


    public string BlueObjective0;
    public string BlueObjective1;
    public string BlueObjective2;

    public string GreenObjective0;
    public string GreenObjective1;
    public string GreenObjective2;
    public string GreenObjective3;


    public string YellowObjective0;
    public string YellowObjective1;
    public string YellowObjective2;
    // Start is called before the first frame update
    void Start()
    {
        SaveSlots = PlayerPrefs.GetInt("SaveSlots", 1);
        StartCheckpoint();
        ChangeText();
    }
    public void StartCheckpoint()
    {
        switch (SaveSlots)
        {
            case 1:
                CurrentCheckPointNumber = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1;
                break;
            case 2:
                CurrentCheckPointNumber = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2;
                break;
            case 3:
                CurrentCheckPointNumber = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3;

                break;
        }
    }
    /*
      if (CheckSave1 > CurrentCheckPointNumber)
                            {
                                TurnOff = true;
                                StopCoRotuine = true;
                                NextCheckpoint();
                            }
      
     */
    // Update is called once per frame
    void Update()
    {
       // Debug.Log("SaveNumber" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1);
        //ChangeText();
        CheckSave1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1;
        CheckSave2 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2;
        CheckSave3 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3;
        if (TurnOff == false)
        {
            switch (SaveSlots)
            {
                case 1:
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "RedBullet_BlockOut":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                         GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 3:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                         GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "BlueBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "GreenBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 3:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                         GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "YellowBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                    //Tanner is The Best
                case 2:
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "RedBullet_BlockOut":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "BlueBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "GreenBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 3:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                         GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "YellowBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                    break;
                case 3:
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "RedBullet_BlockOut":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }

                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "BlueBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "GreenBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                case 3:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                         GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "YellowBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3)
                            {
                                case 0:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                case 1:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                case 2:
                                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3 >
                                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentSaveNumber)
                                    {
                                        TurnOff = true;
                                        StopCoRotuine = true;
                                        NextCheckpoint();

                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
        void NextCheckpoint()
        {
            if (StopCoRotuine)
            {
                StartCoroutine(CheckpointChecked());
            }
        }
        IEnumerator CheckpointChecked()
        {
            //Debug.Log("StartCheckpointChecked");
            yield return new WaitForSeconds(1.5f);
            ChangeText();
            StopCoRotuine = false;
            TurnOff = false;
            switch (SaveSlots)
            {
                case 1:
                    CurrentCheckPointNumber = CheckSave1;
                    break;
                case 2:
                    CurrentCheckPointNumber = CheckSave2;
                    break;
                case 3:
                    CurrentCheckPointNumber = CheckSave3;

                    break;
                default:
                    break;
            }
            //Debug.Log("StopCheckpointChecked");
        }
        void ChangeText()
        {
            switch (SaveSlots)
            {
                case 1:
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "RedBullet_BlockOut":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1)
                            {
                                case 0:
                                   
                                    ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective0;
                                    break;
                                case 1:
                                    
                                    ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective1;
                                    break;
                                case 2:
                                    
                                    ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective2;
                                    break;
                                case 3:

                                ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective3;
                                break;
                            default:
                                    break;
                            }
                            break;
                        case "BlueBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1)
                            {
                                case 0:
                                    
                                    ObjectTextCheckpoint.GetComponent<Text>().text = BlueObjective0;
                                    break;
                                case 1:
                                    
                                    ObjectTextCheckpoint.GetComponent<Text>().text = BlueObjective1;
                                    break;
                                case 2:
                                   
                                    ObjectTextCheckpoint.GetComponent<Text>().text = BlueObjective2;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "GreenBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1)
                            {
                                case 0:
                                    
                                    ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective0;
                                    break;
                                case 1:
                                   
                                    ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective1;
                                    break;
                                case 2:
                                   
                                    ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective2;
                                    break;
                                case 3:
                               // Debug.Log("CAse3GreenBullet text");
                                ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective3;
                                break;
                            default:
                                    break;
                            }
                            break;
                        case "YellowBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber1)
                            {
                                case 0:
                                  
                                    ObjectTextCheckpoint.GetComponent<Text>().text = YellowObjective0;
                                    break;
                                case 1:
                                   
                                    ObjectTextCheckpoint.GetComponent<Text>().text = YellowObjective1;
                                    break;
                                case 2:
                                    
                                    ObjectTextCheckpoint.GetComponent<Text>().text = YellowObjective2;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                case 2:
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "RedBullet_BlockOut":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2)
                            {
                            case 0:

                                ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective0;
                                break;
                            case 1:

                                ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective1;
                                break;
                            case 2:

                                ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective2;
                                break;
                            case 3:

                                ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective3;
                                break;
                            default:
                                break;
                        }
                            break;
                        case "BlueBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2)
                            {
                                case 0:
                                   
                                    ObjectTextCheckpoint.GetComponent<Text>().text = BlueObjective0;
                                    break;
                                case 1:
                                   
                                    ObjectTextCheckpoint.GetComponent<Text>().text = BlueObjective1;
                                    break;
                                case 2:
                                
                                    ObjectTextCheckpoint.GetComponent<Text>().text = BlueObjective2;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "GreenBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2)
                            {
                            case 0:

                                ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective0;
                                break;
                            case 1:

                                ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective1;
                                break;
                            case 2:

                                ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective2;
                                break;
                            case 3:

                                ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective3;
                                break;
                            default:
                                break;
                        }
                            break;
                        case "YellowBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber2)
                            {
                                case 0:
                                    
                                    ObjectTextCheckpoint.GetComponent<Text>().text = YellowObjective0;
                                    break;
                                case 1:
                                   
                                    ObjectTextCheckpoint.GetComponent<Text>().text = YellowObjective1;
                                    break;
                                case 2:
                                    
                                    ObjectTextCheckpoint.GetComponent<Text>().text = YellowObjective2;
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                    break;
                case 3:
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "RedBullet_BlockOut":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3)
                            {
                            case 0:

                                ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective0;
                                break;
                            case 1:

                                ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective1;
                                break;
                            case 2:

                                ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective2;
                                break;
                            case 3:

                                ObjectTextCheckpoint.GetComponent<Text>().text = RedObjective3;
                                break;
                            default:
                                break;
                        }
                            break;
                        case "BlueBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3)
                            {
                                case 0:
                                   
                                        ObjectTextCheckpoint.GetComponent<Text>().text = BlueObjective0;

                                    
                                    break;
                                case 1:
                                   
                                        ObjectTextCheckpoint.GetComponent<Text>().text = BlueObjective1;

                                    
                                    break;
                                case 2:
                                    
                                        ObjectTextCheckpoint.GetComponent<Text>().text = BlueObjective2;

                                    
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "GreenBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3)
                            {
                            case 0:

                                ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective0;
                                break;
                            case 1:

                                ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective1;
                                break;
                            case 2:

                                ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective2;
                                break;
                            case 3:

                                ObjectTextCheckpoint.GetComponent<Text>().text = GreenObjective3;
                                break;
                            default:
                                break;
                        }
                            break;
                        case "YellowBullet_Blockout":
                            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CheckpointSaveNumber3)
                            {
                                case 0:
                                    
                                        ObjectTextCheckpoint.GetComponent<Text>().text = YellowObjective0;

                                    
                                    break;
                                case 1:
                                    
                                        ObjectTextCheckpoint.GetComponent<Text>().text = YellowObjective1;

                                    
                                    break;
                                case 2:
                                    
                                        ObjectTextCheckpoint.GetComponent<Text>().text = YellowObjective2;

                                    
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    

}

