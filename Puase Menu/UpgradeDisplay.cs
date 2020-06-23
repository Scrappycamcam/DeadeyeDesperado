using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    //Gets the Upgrade class
    public Upgrade upgrade;

    //Gets the player's Inventory
   public Inventory inventory;

    //Upgrade Menu UI for the Upgrade Scriptable Object
    public Text upgradeName;
    public Text upgradeLevels;
    public Text currentUpgradeStat;
    public Text nextUpgradeStat;
    public Text upgradeCost;
    public Text oreCount;
    public Text crystalCount;

    public Button upgradeButton;

    public Image currentUpgradeBG;
    public List<Sprite> upgradeBackgrounds;


    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        upgrade.AwakeUpgrade();
        //Debug.Log("FOUNDTHESAVELOAD!!!!!!!");
        if (upgrade != null)
        {
            upgrade.SetValues(this.gameObject);
        }
    }

    private void Update()
    {
        ToggleBuyButton();
    }

    //Toggles the player's ability to click on the purchase button in order to purchase an upgrade
    public void ToggleBuyButton()
    {
        if (!upgrade.reachedMax)
        {
            if (upgrade.CheckInventory(this.gameObject))
            {
                upgradeButton.interactable = true;
            }
            else
            {
                upgradeButton.interactable = false;
            }
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    //Function that is attached to the purchase button in the Upgrade Menu
    //Updates the Upgrade Menu display and checks if the player can purchase the next upgrade
    public void GetUpgrade()
    {
        upgrade.GetUpgrade(this.gameObject);
        upgrade.SetValues(this.gameObject);
        //ToggleBuyButton();
    }
}
