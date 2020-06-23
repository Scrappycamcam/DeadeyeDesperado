using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandolier : MonoBehaviour
{
    public static Bandolier B;
    public Loadouts loadouts;
    public BandolierMenu bandolierMenu;

    public int[][] loadout = new int[6][];

    public int chamberNum;
    public int loadoutNum;
    public int bulletNum;
    public int maxBulletType;
    public int maxNum;

    private void Awake()
    {
        if(B == null)
        {
            B = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bandolierMenu = BandolierMenu.BM;
        maxNum = 6;

        //Carries the loadous for customization
        loadout[0] = loadouts.loadout1;
        loadout[1] = loadouts.loadout2;
        loadout[2] = loadouts.loadout3;
        loadout[3] = loadouts.loadout4;
        loadout[4] = loadouts.loadout5;
        loadout[5] = loadouts.loadout6;

    }

    public void CycleBulletChoice()
    {
        bulletNum = loadout[loadoutNum][chamberNum]; //find the current bullet num we're starting at
        var origBulletNumn = bulletNum; //save the original number so we can know if we loop around
        ++bulletNum; //increment the bullet number
        while (!Gun.instance.isColorUnlocked(bulletNum) && bulletNum != origBulletNumn) //while the bullet you're looking for isn't unlocked, and you haven't circled back around
        {
            bulletNum++;
            if (bulletNum >= maxNum)
            {
                bulletNum = 0;
            }
        }
        if (bulletNum >= maxNum) //if the bullet is above the max (IE 6), circle it back to 0
        {
            bulletNum = 0;
        }
        UpdateLO(); //update the loadout with this new number
        BandolierMenu.BM.UpdateDisplay((loadoutNum + 1)); //update the bandolier display
        BandolierMenu.BM.DisplayBulletInfo(bulletNum); //update the bullet info
    }

    public void GetLO()
    {
        loadout[0] = loadouts.loadout1;
        loadout[1] = loadouts.loadout2;
        loadout[2] = loadouts.loadout3;
        loadout[3] = loadouts.loadout4;
        loadout[4] = loadouts.loadout5;
        loadout[5] = loadouts.loadout6;
    }

    public void UpdateLO()
    {
        loadout[loadoutNum][chamberNum] = bulletNum;
        switch(loadoutNum)
        {
            case 0:
                for (int i = 0; i < maxNum; ++i)
                {
                    loadouts.loadout1[i] = loadout[loadoutNum][i];
                }
                break;

            case 1:
                for (int i = 0; i < maxNum; ++i)
                {
                    loadouts.loadout2[i] = loadout[loadoutNum][i];
                }
                break;

            case 2:
                for (int i = 0; i < maxNum; ++i)
                {
                    loadouts.loadout3[i] = loadout[loadoutNum][i];
                }
                break;

            case 3:
                for (int i = 0; i < maxNum; ++i)
                {
                    loadouts.loadout4[i] = loadout[loadoutNum][i];
                }
                break;

            case 4:
                for (int i = 0; i < maxNum; ++i)
                {
                    loadouts.loadout5[i] = loadout[loadoutNum][i];
                }
                break;

            case 5:
                for (int i = 0; i < maxNum; ++i)
                {
                    loadouts.loadout6[i] = loadout[loadoutNum][i];
                }
                break;
        }
        
    }
}
