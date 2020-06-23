using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int oreCount;
    private int crystalCount;
    private int totalCollectables;

    private void Awake()
    {
        if (SaveLoad.SaveExists("Ores"))
        {
            oreCount = SaveLoad.Load<int>("Ores");
        }
        if (SaveLoad.SaveExists("OCrystals"))
        {
            crystalCount = SaveLoad.Load<int>("OCrystals");
        }
        if (SaveLoad.SaveExists("TotalCollectables"))
        {
            totalCollectables = SaveLoad.Load<int>("TotalCollectables");
        }


    }
    public void AddOre()
    {
        ++oreCount;
        Debug.Log("Ores" + oreCount);
        SaveLoad.Save(oreCount, "Ores");

        ++totalCollectables;
        SaveLoad.Save(totalCollectables, "TotalCollectables");
    }

    public void AddCrystal()
    {
        ++crystalCount;
        Debug.Log("OCrystals: " + crystalCount);
        SaveLoad.Save(crystalCount, "OCrystals");

        ++totalCollectables;
        SaveLoad.Save(totalCollectables, "TotalCollectables");

    }

    public int totalCollectablesAcquired()
    {
        return totalCollectables;
    }

    public int GetOreCount()
    {
        return oreCount;
    }

    public int GetCrystalCount()
    {
        return crystalCount;
    }

    public void SpendOres(int cost)
    {
        oreCount -= cost;
        SaveLoad.Save(oreCount, "Ores");
    }

    public void SpendCrystals(int cost)
    {
        crystalCount -= cost;
        SaveLoad.Save(crystalCount, "OCrystals");
    }
}