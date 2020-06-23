using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadouts : MonoBehaviour
{
    public static Loadouts LO;
    public List<int[]> loadoutList = new List<int[]>();

    public int loadoutNum;

    [Header("Load Outs")]
    public int[] loadout1 = new int[6];
    public int[] loadout2 = new int[6];
    public int[] loadout3 = new int[6];
    public int[] loadout4 = new int[6];
    public int[] loadout5 = new int[6];
    public int[] loadout6 = new int[6];

    [Header("Load Out Unlocks")]
    //Enables & Disables use of each loadout
    public bool LO1Unlocked;
    public bool LO2Unlocked;
    public bool LO3Unlocked;
    public bool LO4Unlocked;
    public bool LO5Unlocked;
    public bool LO6Unlocked;

    

    //Constructs each loadout
   /* public Loadouts(int[] loadout1, int[] loadout2, int[] loadout3,
                    int[] loadout4, int[] loadout5, int[] loadout6)
    {
        this.loadout1 = loadout1;
        this.loadout2 = loadout2;
        this.loadout3 = loadout3;
        this.loadout4 = loadout4;
        this.loadout5 = loadout5;
        this.loadout6 = loadout6;
    }*/

    private void Awake()
    {
        if (LO == null)
        {
            LO = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        LO1Unlocked = false;
        LO2Unlocked = false;
        LO3Unlocked = false;
        LO4Unlocked = false;
        LO5Unlocked = false;
        LO6Unlocked = false;

        loadoutList.Add(loadout1);

        LoadLO();

        FillLoadoutList();
    }

    //Used to save loadout info into the save system
    public void SaveLO()
    {
        SaveLoad.Save<int[]>(loadout1, "Loadout 1");
        SaveLoad.Save<int[]>(loadout2, "Loadout 2");
        SaveLoad.Save<int[]>(loadout3, "Loadout 3");
        SaveLoad.Save<int[]>(loadout4, "Loadout 4");
        SaveLoad.Save<int[]>(loadout5, "Loadout 5");
        SaveLoad.Save<int[]>(loadout6, "Loadout 6");

        SaveLoad.Save<bool>(LO1Unlocked, "L1Unlock");
        SaveLoad.Save<bool>(LO2Unlocked, "L2Unlock");
        SaveLoad.Save<bool>(LO3Unlocked, "L3Unlock");
        SaveLoad.Save<bool>(LO4Unlocked, "L4Unlock");
        SaveLoad.Save<bool>(LO5Unlocked, "L5Unlock");
        SaveLoad.Save<bool>(LO6Unlocked, "L6Unlock");

    }
    private void Update()
    {
        SaveLO();
    }
    //Used to load loadout info from the save system
    public void LoadLO()
    {
        if(SaveLoad.SaveExists("Loadout 1"))
        {
            loadout1 = SaveLoad.Load<int[]>("Loadout 1");
        }
        if (SaveLoad.SaveExists("Loadout 2"))
        {
            loadout2 = SaveLoad.Load<int[]>("Loadout 2");
        }
        if (SaveLoad.SaveExists("Loadout 3"))
        {
            loadout3 = SaveLoad.Load<int[]>("Loadout 3");
        }
        if (SaveLoad.SaveExists("Loadout 4"))
        {
            loadout4 = SaveLoad.Load<int[]>("Loadout 4");
        }
        if (SaveLoad.SaveExists("Loadout 5"))
        {
            loadout5 = SaveLoad.Load<int[]>("Loadout 5");
        }
        if (SaveLoad.SaveExists("Loadout 6"))
        {
            loadout6 = SaveLoad.Load<int[]>("Loadout 6");
        }
        if (SaveLoad.SaveExists("L6Unlock"))
        {
            LO6Unlocked = SaveLoad.Load<bool>("L6Unlock");
        }
        if (SaveLoad.SaveExists("L5Unlock"))
        {
            LO5Unlocked = SaveLoad.Load<bool>("L5Unlock");
        }
        if (SaveLoad.SaveExists("L4Unlock"))
        {
            LO4Unlocked = SaveLoad.Load<bool>("L4Unlock");
        }
        if (SaveLoad.SaveExists("L3Unlock"))
        {
            LO3Unlocked = SaveLoad.Load<bool>("L3Unlock");
        }
        if (SaveLoad.SaveExists("L2Unlock"))
        {
            LO2Unlocked = SaveLoad.Load<bool>("L2Unlock");
        }
        if (SaveLoad.SaveExists("L1Unlock"))
        {
            LO1Unlocked = SaveLoad.Load<bool>("L1Unlock");
        }
    }

    //Unlocks loadouts for the player to use
    public void UnlockLO(int newType)
    {


        switch(((!LO1Unlocked ? 0 : 1) + (!LO2Unlocked ? 0 : 1) + (!LO3Unlocked ? 0 : 1) + (!LO4Unlocked ? 0 : 1) + (!LO5Unlocked ? 0 : 1) + (!LO6Unlocked ? 0 : 1)))
        {
            case 0:
                for(int i = 0; i < 6; i++)
                {
                    LO1Unlocked = true;
                    loadout1[i] = newType;
                    Debug.Log("NewType: " + newType);
                }
                break;
            case 1:
                for (int i = 0; i < 6; i++)
                {
                    LO2Unlocked = true;
                    loadout2[i] = newType;
                }
                break;
            case 2:
                for (int i = 0; i < 6; i++)
                {
                    LO3Unlocked = true;
                    loadout3[i] = newType;
                }
                break;
            case 3:
                for (int i = 0; i < 6; i++)
                {
                    LO4Unlocked = true;
                    loadout4[i] = newType;
                }
                break;
            case 4:
                for (int i = 0; i < 6; i++)
                {
                    LO5Unlocked = true;
                    loadout5[i] = newType;
                }
                break;
            case 5:
                for (int i = 0; i < 6; i++)
                {
                    LO6Unlocked = true;
                    loadout6[i] = newType;
                }
                break;
        }
    }

    //Determines what loadout the player uses when they reload
    public int[] LoadoutChoice(int loNum)
    {
        return loadoutList[loNum + 1];
    }

    private void FillLoadoutList()
    {
        loadoutList.Add(LO.loadout1);
        loadoutList.Add(LO.loadout2);
        loadoutList.Add(LO.loadout3);
        loadoutList.Add(LO.loadout4);
        loadoutList.Add(LO.loadout5);
        loadoutList.Add(LO.loadout6);
    }
}
