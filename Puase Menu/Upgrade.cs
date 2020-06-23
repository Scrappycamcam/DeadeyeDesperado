using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Uses the Upgrade Scriptable Object to perform functions for the Upgrade Menu display
[System.Serializable]
public class Upgrade
{
    public UpgradeSO upgrade;
    //public UpgradeDisplay upgradeDisplay;

    [SerializeField] private Player player;
    [SerializeField] private Gun gun;

    public int currentLevel;
    public bool reachedMax;

    private int upgradeType;
    private int upgradeCost;
    private int oreCount;
    private int crystalCount;

    //The type of upgrade (based on upgrade type from the corresponding UpgradeSO
    public enum UpgradeType { Player, Gun, NULL };

    //Constructs the Upgrade class based on the current Upgrade Scriptable Object
    public Upgrade(UpgradeSO upgrade, int currentLevel)
    {
        this.upgrade = upgrade;
        this.currentLevel = currentLevel;
        

    }
    public void AwakeUpgrade()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gun = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Gun>();

        if (SaveLoad.SaveExists(upgrade.name))
        {
            currentLevel = SaveLoad.Load<int>(upgrade.name);
            UpdatePlayerStats();
            UpdateGunStats();
        }
        //Debug.Log("FOUNDTHESAVELOAD!!!!!!!");
    }
    //Sets the values on the Upgrade Menu based on the variables of the Upgrade Scriptable Object being used
    public void SetValues(GameObject UpgradeDisplayObject)
    {
        //if (UpgradeDisplayObject)
        //{
            UpgradeDisplay UD = UpgradeDisplayObject.GetComponent<UpgradeDisplay>();
            UD.upgradeName.text = upgrade.Name + ":";

            if (UD.upgradeLevels)
            {
                UD.upgradeLevels.text = currentLevel + "/" + (upgrade.statList.Count - 1);
            }

            if (UD.currentUpgradeStat)
            {
                UD.currentUpgradeStat.text = upgrade.statList[currentLevel].ToString();
            }

            if (UD.nextUpgradeStat)
            {
                if (currentLevel < (upgrade.statList.Count - 1))
                {
                    UD.nextUpgradeStat.text = upgrade.statList[currentLevel + 1].ToString();
                    reachedMax = false;
                }
                else
                {
                    UD.nextUpgradeStat.text = "Max Reached";
                    reachedMax = true;
                }
            }

            if (UD.upgradeCost)
            {
            if (reachedMax)
            {
                UD.upgradeCost.text = "N/A";
            }
            else
            {
                UD.upgradeCost.text = upgrade.statCost[currentLevel + 1].ToString();
                upgradeCost = upgrade.statCost[currentLevel + 1];
            }
            }

            if (UD.currentUpgradeBG)
            {
                UD.currentUpgradeBG.sprite = UD.upgradeBackgrounds[currentLevel];
            }

            /*if (UD.oreCount)
            {
                oreCount = UD.inventory.GetOreCount();
                UD.oreCount.text = oreCount.ToString();
            }

            if (UD.crystalCount)
            {
                crystalCount = UD.inventory.GetCrystalCount();
                UD.crystalCount.text = crystalCount.ToString();
            }*/  

        UD.ToggleBuyButton();
        SaveLoad.Save(currentLevel, upgrade.Name);
        //}
    }

    //Checks to see if the player can afford to purchase the next upgrade
    public bool CheckInventory(GameObject UpgradeDisplayObject)
    {
        UpgradeDisplay UD = UpgradeDisplayObject.GetComponent<UpgradeDisplay>();

        /*crystalCount = UD.inventory.GetCrystalCount();
        oreCount = UD.inventory.GetOreCount();*/

        if (UD.oreCount)
        {
            oreCount = UD.inventory.GetOreCount();
            UD.oreCount.text = oreCount.ToString();
        }

        if (UD.crystalCount)
        {
            crystalCount = UD.inventory.GetCrystalCount();
            UD.crystalCount.text = crystalCount.ToString();
        }

        //TO DO: CHANGE "upgrade.statList[currentLevel]" TO PLAYER'S RESPECTIVE CURRENCY FOR UPGRADE (MAYBE A FUNCTION?)
        if (GetResourceAmount() >= upgrade.statCost[currentLevel + 1]) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Allows the player to purchase the upgrade on the Upgrade Menu
    public void GetUpgrade(GameObject UpgradeDisplayObject)
    {
        UpgradeDisplay UD = UpgradeDisplayObject.GetComponent<UpgradeDisplay>();

        upgradeType = GetUpgradeType(upgrade.upgradeType);
        if (upgradeType == (int)UpgradeType.Player)
        {
            ++currentLevel;
            Debug.Log("Player stat changed");
            if(upgrade.Name == "Deadeye")
            {
                UpdateGunStats();
            }
            else
            {
                UpdatePlayerStats();
            }
            //TO DO: ADD FUNCTIONS THAT WILL TAKE PLAYER'S ORES FROM THEIR INVENTORY AND ADJUST THEIR AFFECTED STATS
            Debug.Log("Upgrade Cost: " + upgradeCost);
            UD.inventory.SpendOres(upgradeCost);
        }
        else if (upgradeType == (int)UpgradeType.Gun)
        {
            ++currentLevel;
            Debug.Log("Gun stat changed");
            UpdateGunStats();
            //TO DO: ADD FUNCTIONS THAT WILL TAKE PLAYER'S CRYSTALS FROM THEIR INVENTORY AND ADJUST THEIR AFFECTED STATS
            Debug.Log("Upgrade Cost: " + upgradeCost);
            UD.inventory.SpendCrystals(upgradeCost);
        }
        else
        {
            Debug.Log("Invalid Upgrade Type...");
        }
    }

    private int GetUpgradeType(int type)
    {
        switch (type)
        {
            case (int)UpgradeType.Player:
                return (int)UpgradeType.Player;

            case (int)UpgradeType.Gun:
                return (int)UpgradeType.Gun;

            default:
                return (int)UpgradeType.NULL;
        }
    }

    public int GetResourceAmount()
    {
        upgradeType = GetUpgradeType(upgrade.upgradeType);

        if(upgradeType == (int)UpgradeType.Player)
        {
            return oreCount;
        }
        else if(upgradeType == (int)UpgradeType.Gun)
        {
            return crystalCount;
        }
        else
        {
            Debug.Log("No resource for type detected...");
            return -1;
        }
    }

    public void UpdatePlayerStats()
    {
        player.UpdateStats(upgrade, currentLevel);
    }

    private void UpdateGunStats()
    {
        gun.UpdateStats(upgrade, currentLevel);
    }
}
