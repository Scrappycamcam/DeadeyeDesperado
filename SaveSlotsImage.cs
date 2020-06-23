using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSlotsImage : MonoBehaviour
{
    private int SaveSlots;
    private int SaveSlots2;
    private int SaveSlots3;
    private string SavedScene1;
    private string SavedScene2;
    private string SavedScene3;

    public Image Blast1;
    public Image Blast2;
    public Image Blast3;

    public Image Frost1;
    public Image Frost2;
    public Image Frost3;

    public Image Heal1;
    public Image Heal2;
    public Image Heal3;

    public Image Corrosive1;
    public Image Corrosive2;
    public Image Corrosive3;

    public Image Shock1;
    public Image Shock2;
    public Image Shock3;

    public Image Void1;
    public Image Void2;
    public Image Void3;

    public bool RedBullet1;
    public bool BlueBullet1;
    public bool GreenBullet1;
    public bool OrangeBullet1;
    public bool CyanBullet1;
    public bool PurpleBullet1;

    public bool RedBullet2;
    public bool BlueBullet2;
    public bool GreenBullet2;
    public bool Orangeullet2;
    public bool CyanBullet2;
    public bool PurpleBullet2;

    public bool RedBullet3;
    public bool BlueBullet3;
    public bool GreenBullet3;
    public bool OrangeBullet3;
    public bool CyanBullet3;
    public bool PurpleBullet3;





    // Start is called before the first frame update
    void Start()
    {



        //Debug.Log(SaveSlots);
        IconsOnLoadOut1();
        IconsOnLoadOut2();
        IconsOnLoadOut3();
    }
    void IconsOnLoadOut1()
    {
      SaveSlots = PlayerPrefs.GetInt("SaveSlots", 1);
        SaveSlots = 1;
      PlayerPrefs.SetInt("SaveSlots", SaveSlots);
        if (SaveLoad.SaveExists("RedActive"))
            RedBullet1 = true;

        if (SaveLoad.SaveExists("BlueActive"))
        {
            BlueBullet1 = true;
        }
        if (SaveLoad.SaveExists("GreenActive"))
        {
            GreenBullet1 = true;
        }
        if (SaveLoad.SaveExists("YellowActive"))
        {
            OrangeBullet1 = true;
        }
        if (SaveLoad.SaveExists("CyanActive"))
        {
            CyanBullet1 = true;
        }
        if (SaveLoad.SaveExists("PurpleActive"))
        {
            PurpleBullet1 = true;
        }

    }
    void IconsOnLoadOut2()
    {
        SaveSlots = PlayerPrefs.GetInt("SaveSlots", 1);
        SaveSlots = 2;
        PlayerPrefs.SetInt("SaveSlots", SaveSlots);
        if (SaveLoad.SaveExists("RedActive"))
            RedBullet2 = true;

        if (SaveLoad.SaveExists("BlueActive"))
        {
            BlueBullet2 = true;
        }
        if (SaveLoad.SaveExists("GreenActive"))
        {
            GreenBullet2 = true;
        }
        if (SaveLoad.SaveExists("YellowActive"))
        {
            Orangeullet2 = true;
        }
        if (SaveLoad.SaveExists("CyanActive"))
        {
            CyanBullet2 = true;
        }
        if (SaveLoad.SaveExists("PurpleActive"))
        {
            PurpleBullet2 = true;
        }
    }
    void IconsOnLoadOut3()
    {
        SaveSlots = PlayerPrefs.GetInt("SaveSlots", 1);
        SaveSlots = 3;
        PlayerPrefs.SetInt("SaveSlots", SaveSlots);
        if (SaveLoad.SaveExists("RedActive"))
            RedBullet3 = true;

        if (SaveLoad.SaveExists("BlueActive"))
        {
            BlueBullet3 = true;
        }
        if (SaveLoad.SaveExists("GreenActive"))
        {
            GreenBullet3 = true;
        }
        if (SaveLoad.SaveExists("YellowActive"))
        {
            OrangeBullet3 = true;
        }
        if (SaveLoad.SaveExists("CyanActive"))
        {
            CyanBullet3 = true;
        }
        if (SaveLoad.SaveExists("PurpleActive"))
        {
            PurpleBullet3 = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (RedBullet1)
        {
            Blast1.enabled = true;

        }
        else
        {
            Blast1.enabled = false;

        }
        if (BlueBullet1)
        {
            Frost1.enabled = true;

        }
        else
        {
            Frost1.enabled = false;

        }
        if (GreenBullet1)
        {
            Heal1.enabled = true;

        }
        else
        {
            Heal1.enabled = false;

        }
        if (OrangeBullet1)
        {
            Corrosive1.enabled = true;

        }
        else
        {
            Corrosive1.enabled = false;

        }
        if (PurpleBullet1)
        {
            Void1.enabled = true;

        }
        else
        {
            Void1.enabled = false;

        }
        if (CyanBullet1)
        {
            Shock1.enabled = true;

        }
        else
        {
            Shock1.enabled = false;

        }


        if (RedBullet2)
        {
            Blast2.enabled = true;

        }
        else
        {
            Blast2.enabled = false;
        }
        if (BlueBullet2)
        {
            Frost2.enabled = true;

        }
        else
        {
            Frost2.enabled = false;

        }
        if (GreenBullet2)
        {
            Heal2.enabled = true;

        }
        else
        {
            Heal2.enabled = false;

        }
        if (Orangeullet2)
        {
            Corrosive2.enabled = true;

        }
        else
        {
            Corrosive2.enabled = false;

        }
        if (PurpleBullet2)
        {
            Void2.enabled = true;

        }
        else
        {
            Void2.enabled = false;

        }
        if (CyanBullet2)
        {
            Shock2.enabled = true;

        }
        else
        {
            Shock2.enabled = false;

        }


        if (RedBullet3)
        {
            Blast3.enabled = true;

        }
        else
        {
            Blast3.enabled = false;

        }
        if (BlueBullet3)
        {
            Frost3.enabled = true;

        }
        else
        {
            Frost3.enabled = false;

        }
        if (GreenBullet3)
        {
            Heal3.enabled = true;

        }
        else
        {
            Heal3.enabled = false;

        }
        if (CyanBullet3)
        {
            Shock3.enabled = true;

        }
        else
        {
            Shock3.enabled = false;

        }
        if (OrangeBullet3)
        {
            Corrosive3.enabled = true;

        }
        else
        {
            Corrosive3.enabled = false;

        }
        if (PurpleBullet3)
        {
            Void3.enabled = true;

        }
        else
        {
            Void3.enabled = false;

        }

    }
}
