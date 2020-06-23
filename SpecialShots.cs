using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public struct DesperadoShot
{
    public bulletType bPart;
    public float passiveDuration;
    public float activeDuration;

}


public class SpecialShots : MonoBehaviour
{
    public int SaveSlots;

    [SerializeField]
    public List<DesperadoShot> EquippedShots;

    public void SaveShot()
    {

        Debug.Log("SavedShots");
        Debug.Log(EquippedShots);
        switch (SaveSlots)
        {
            case 1:
                SaveSystemShot1.SaveShots1(this);
                break;
            case 2:
                SaveSystemShot2.SaveShots2(this);
                break;
            case 3:
                SaveSystemShot3.SaveShots3(this);
                break;
            default:
                Debug.LogError("Save Not Working");
                break;
        }
        
    }
    
    void LoadShot()
    {
        switch (SaveSlots)
        {
            case 1:
                LoadSaveOne();

                break;
            case 2:
                LoadSaveTwo();



                break;
            case 3:
                LoadSaveThree();
                break;
            default:
                Debug.LogError("Save Not Working");
                break;
        }
        

        Debug.Log(EquippedShots);
    }
    
    private void LoadSaveOne()
    {
       if( File.Exists(Application.persistentDataPath + "/shot1.save"))
       {
            ShotData1 shotdata = SaveSystemShot1.LoadShots();
            EquippedShots = shotdata.Shots1;
        }
        else
        {
            return;
        }
       


    }

    private void LoadSaveTwo()
    {
        if (File.Exists(Application.persistentDataPath + "/shot2.save"))
        {
            ShotData2 shotdata = SaveSystemShot2.LoadShots();
            EquippedShots = shotdata.Shots2;
        }
        else
        {
            return;
        }
      

    }

    private void LoadSaveThree()
    {
        if(File.Exists(Application.persistentDataPath + "/shot3.save"))
        {
            ShotData3 shotdata = SaveSystemShot3.LoadShots();
            EquippedShots = shotdata.Shots3;

        }

    }

    private void Awake()
    {
        SaveSlots = PlayerPrefs.GetInt("SaveSlots", 1);
        switch (SaveSlots)
        {
            case 1:
                LoadSaveOne();
                break;
            case 2:
                LoadSaveTwo();
                break;
            case 3:
                LoadSaveThree();
                break;
            default:
                Debug.LogError("Save Not Working");
                break;
           
        }
      
        
            EquippedShots = new List<DesperadoShot>(6);
        
        
        DesperadoShot fill;
        fill.passiveDuration = 0f;
        fill.activeDuration = 0f;
        for (int i = 0; i < 6; i++)
        {
            //Debug.Log(i);
            fill.bPart = (bulletType)i;
            EquippedShots.Insert(i, fill);
        }
    }

    public void equipShot(DesperadoShot dShot)
    {
        if (EquippedShots[(int)dShot.bPart].bPart == dShot.bPart)
        {
            EquippedShots[(int)dShot.bPart] = dShot;
        }
    }

    public DesperadoShot checkShot(bulletType part)
    {
        for (int i = 0; i < 6; i++)
        {
            if (EquippedShots[i].bPart == part)
            {
                return EquippedShots[i];
            }
        }
        //SaveShot();
        return new DesperadoShot();
    }

}
