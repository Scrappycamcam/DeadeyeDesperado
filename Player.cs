using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class Player : MonoBehaviour
{
    #region Variables
    private static Player instance = null;
    
   public static Player Instance { get { return instance; } }
	
	public string Level;
    public int SaveSlots;
    public float maxHealth = 100;
    public float curHealth = 100;

    public int maxArmor = 1;
    public int curArmor = 0;

    public float regenDelay = .1f;
    private bool canRegen = true;
    private Healthbar myHB;
	public GameObject gameOver;

    public Image BulletObjective; // The BulletImage on the NotPauseCanvas

    public bool TurnOff;
    public bool StopCoRotuine;

    public int CurrentSaveNumber;

    public int CheckpointSaveNumber1;
    
    public int CheckpointSaveNumber2;
    
    public int CheckpointSaveNumber3;

    public GameObject[] Blockersaved;

    public int SavedNumber;

    [Header("Player Upgrades")]
    [SerializeField] private UpgradeSO HealthUpgrade;
    [SerializeField] private UpgradeSO ArmorUpgrade;


    

    [Header("First Save")]
    public bool RedBullet1;
    public bool BlueBullet1;
    public bool YellowBullet1;
    public bool GreenBullet1;
    public bool CyanBullet1;
    public bool PurpleBullet;
    public bool RedUnlock1;
    public bool BlueUnlock1;
    public bool GreenUnlock1;
    public bool YellowUnlock1;
    public bool RedChallengeCompelete1;
    public bool GreenChallengeCompelete1;
    [SerializeField]
    public bool RedLevelCompelete1;
    
    
    [SerializeField]
    public bool BlueLevelCompelete1;
    [SerializeField]
    public bool GreenLevelCompelete1;

    public int BulletCount = 0;


    [SerializeField]
    public bool YellowLevelCompelete1;
   
    public bool RedUnlock2;
    public bool BlueUnlock2;
    public bool GreenUnlock2;
    public bool YellowUnlock2;

    public bool RedChallengeCompelete2;
    public bool GreenChallengeCompelete2;
    [SerializeField]
    public bool RedLevelCompelete2;
    
    [SerializeField]
    public bool BlueLevelCompelete2;

   
    [SerializeField]
    public bool GreenLevelCompelete2;

    [SerializeField]
    public bool YellowLevelCompelete2;

 
    public bool RedUnlock3;
    public bool BlueUnlock3;
    public bool GreenUnlock3;
    public bool YellowUnlock3;
    public bool RedChallengeCompelete3;
    public bool GreenChallengeCompelete3;
    [SerializeField]
    public bool RedLevelCompelete3;

    [SerializeField]
    public bool BlueLevelCompelete3;
    [SerializeField]
    public bool GreenLevelCompelete3;

    [SerializeField]
    public bool YellowLevelCompelete3;



    public FirstPersonController controller;

    GameObject[] ArmorPieces;

    private bool isPoisoned = false;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        SaveSlots = PlayerPrefs.GetInt("SaveSlots", 1);

        SaveGameEvents.SaveInitiated += SavePlayer;

        myHB = gameObject.GetComponent<Healthbar>();
        Level = SceneManager.GetActiveScene().name;

        ArmorPieces = new GameObject[4];

        for(int i = 0; i < 4; i++)
        {
            ArmorPieces[i] = GameObject.Find("Armor" + (i+1));
        }
        LoadPlayer();
        SetArmor();
   
       
    
}
    private void Start()
    {
       

        CheckpointSaveNumber1 =0;

    CheckpointSaveNumber2= 0;

    CheckpointSaveNumber3= 0;

        Blockersaved = GameObject.FindGameObjectsWithTag("Blocker");


        StartCheckpoint();
    }

    private void SetArmor() //sets the UI for armor pieces
    {
        SaveLoad.Save<int>(curArmor, "curArmor");
        for(int i = 0; i < 4; i++)
        {
            if(i < curArmor)
            {
                ArmorPieces[i].SetActive(true);
            }
            else
            {
                ArmorPieces[i].SetActive(false);
            }
        }
    }
    
    public void StartCheckpoint()
    {
        switch (SaveSlots)
        {
            case 1:
                CurrentSaveNumber = CheckpointSaveNumber1;
                break;
            case 2:
                CurrentSaveNumber = CheckpointSaveNumber2;
                break;
            case 3:
                CurrentSaveNumber = CheckpointSaveNumber3;

                break;
        }
    }

    public void SavePlayer()
    {
        
        RedBullet1 = Gun.instance.RedActive;
        BlueBullet1 = Gun.instance.BlueActive;
        GreenBullet1 = Gun.instance.GreenActive;
        YellowBullet1= Gun.instance.YellowActive;
        CyanBullet1= Gun.instance.CyanActive;
        PurpleBullet = Gun.instance.PurpleActive;
        if (RedBullet1 == true)
        {
            SaveLoad.Save(RedBullet1, "RedSaveBullets");
           
        }
        if (BlueBullet1 == true)
        {
            SaveLoad.Save(BlueBullet1, "BlueSaveBullets");
            
        }
        if (GreenBullet1 == true)
        {
            SaveLoad.Save(GreenBullet1, "GreenSaveBullets");
          

        }
        if (YellowBullet1 == true)
        {
            SaveLoad.Save(YellowBullet1, "YellowBullet1Bullets");
           
        }
        if (YellowBullet1 == true)
        {
            SaveLoad.Save(YellowBullet1, "YellowBullet1Bullets");

        }
        if (YellowBullet1 == true)
        {
            SaveLoad.Save(YellowBullet1, "YellowBullet1Bullets");

        }
        Debug.Log("SAVED BULLETS!!!!!!!!!!");
                
            
        
    }
    private void LoadPlayer()
    {

       
        SavedNumber = PlayerPrefs.GetInt("CaseNumberOfSave", 1);

        if (SaveLoad.SaveExists("RedRoomFinish"))
           RedUnlock1 = true;

        if (SaveLoad.SaveExists("BlueRoomFinish"))
        {
            BlueUnlock1 = true;
        }
        if (SaveLoad.SaveExists("GreenRoomFinish"))
        {
            GreenUnlock1 = true;
        }
        if (SaveLoad.SaveExists("YellowRoomFinish"))
        {
            YellowUnlock1 = true;
        }


        if (SaveLoad.SaveExists("curArmor"))
        {
            GiveArmor(SaveLoad.Load<int>("curArmor"));
        }
        /* if (GameObject.Find("FirstPersonCharacter").GetComponent<Gun>().RedActive == true)
             {

             }
             else
             {
             if (SaveLoad.SaveExists("RedSaveBullets" ))
                 {
                 RedBullet1 = SaveLoad.Load<bool>("RedSaveBullets");

                 Gun.instance.RedActive = RedBullet1;
                 }
         }
         if (Gun.instance.BlueActive == true)
         {

         }
         else
         {
             if (SaveLoad.SaveExists("BlueSaveBullets" ))
             {
                 Debug.Log("BulletBULLETLOAD!!!!!!!!!!!!!!!!!!!!");

                 BlueBullet1 = SaveLoad.Load<bool>("BlueSaveBullets" );
                 Gun.instance.BlueActive = BlueBullet1;
             }
         }
         if (Gun.instance.GreenActive == true)
         {

         }
         else
         {
             if (SaveLoad.SaveExists("GreenSaveBullets" ))
             {
                 GreenBullet1 = SaveLoad.Load<bool>("GreenSaveBullets" + BulletCount);
                 Gun.instance.GreenActive = GreenBullet1;
             }
         }
         if (Gun.instance.YellowActive == true)
         {

         }
         else
         {
             if (SaveLoad.SaveExists("YellowBullet1Bullets" ))
             {
                 YellowBullet1 = SaveLoad.Load<bool>("YellowBullet1Bullets");
                 Gun.instance.YellowActive = YellowBullet1;
             }
         }
         */
        if (SaveLoad.SaveExists("RedPuzzleChallege"))
        {
            RedChallengeCompelete1 = SaveLoad.Load<bool>("RedPuzzleChallege");
        }
        if (SaveLoad.SaveExists("GreenPuzzleChallege"))
        {
            GreenChallengeCompelete1 = SaveLoad.Load<bool>("GreenPuzzleChallege");
        }
     
    }
    

    private IEnumerator PassiveHealthRegen(float delay)
    {
        canRegen = true;
        yield return new WaitForSeconds(delay);

        while(canRegen && curHealth < maxHealth)
        {
            curHealth++;
            myHB.UpdateBar(curHealth/maxHealth);
            yield return new WaitForSeconds(regenDelay);
        }
        StopCoroutine("PassiveHealthRegen");
    }

    public void GiveArmor(int amount)
    {
        if(amount + curArmor >= maxArmor)
        {
            curArmor = maxArmor;
        }
        else
        {
            curArmor += amount;
        }
        SetArmor();
    }

    public void Damage(int damage)
    {
        if (curArmor > 0)
        {
            curArmor--;
            SetArmor();
        }
        else
        {
            if (damage > 0)
            {
                canRegen = false;
                StopCoroutine("PassiveHealthRegen");
                StartCoroutine("PassiveHealthRegen", 5f);
                GetComponent<AudioSource>().PlayOneShot(Resources.Load("Sounds/SFX_Player_hit") as AudioClip);
            }
            curHealth -= damage;
            if (curHealth > 100)
            {
                curHealth = 100;
            }
            myHB.UpdateBar(curHealth / maxHealth);
            if (curHealth <= 0)
            {
                curHealth = 0;
                //canRegen = false;
                StartCoroutine("Die");
            }
        }
    }

    private IEnumerator Die()
    {
        GetComponent<PlayerInput>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponentInChildren<Gun>().takingInput = false;
        GetComponentInChildren<Gun>().myGAS.Die();
        yield return new WaitForSeconds(1.8f);
        Instantiate(gameOver);
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = (true);
        //Time.timeScale = 0f;
        yield return null;
        //Pull up "You Died" Screen
        //Pull up options to do stuff
        //IE
        //Restart at Checkpoint
        //Main Menu
        //ETC
    }


	public void reloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arm")
        {
            var v = other.gameObject.GetComponentInParent<Enemy>();
            if (v)
            {
                Damage(v.strikeDamage);
            }
        }
        else if (other.tag == "Cart")
        {
            Damage((int)other.gameObject.GetComponent<Minecart>().speed);
        }
        if (other.gameObject.tag=="CheckPoint")
        {
            ChallengeCompelete();
        }
        if (other.gameObject.tag == "SavePoint")
        {
            SaveGameEvents.SaveInitiated += SavePlayer;
            SavePlayer();

        }
        if(other.gameObject.tag == "DeathBox")
        {
            Damage(1000);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Poison" && !isPoisoned)
        {
            StartCoroutine("PoisonMe");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other.name);
        if(other.gameObject.tag == "Poison" && !isPoisoned)
        {		
            StartCoroutine("PoisonMe");		
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            Damage(other.GetComponent<EnemyBullet>().Damage);
        }
    }	
    		
    IEnumerator PoisonMe()		
    {		
        isPoisoned = true;
        curHealth -= 5;
        canRegen = false;
        StopCoroutine("PassiveHealthRegen");
        if (curHealth > 100)
        {
            curHealth = 100;
        }
        myHB.UpdateBar(curHealth / maxHealth);
        //canRegen = false;
        //StartCoroutine("PassiveHealthRegen", 5f);
        if (curHealth <= 0)
        {
            curHealth = 0;
            //canRegen = false;
            StartCoroutine("Die");
        }
        GetComponent<AudioSource>().PlayOneShot(Resources.Load("Sounds/SFX_Poison_Cough") as AudioClip);
        yield return new WaitForSeconds(2f);
        isPoisoned = false;
        StartCoroutine("PassiveHealthRegen", 5f);
    }
    public void NextCheckpoint()
    {
        //Debug.Log("NextCheckpoint");

        if (StopCoRotuine)
        {
            StartCoroutine(CheckpointChecked());
        }
        
       

    }
    IEnumerator CheckpointChecked()
    {
        BulletObjective.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        BulletObjective.gameObject.SetActive(false);
        StopCoRotuine = false;
        TurnOff = false;
        switch (SaveSlots)
        {
            case 1:
                CurrentSaveNumber = CheckpointSaveNumber1;
                break;
            case 2:
                CurrentSaveNumber = CheckpointSaveNumber2;
                break;
            case 3:
                CurrentSaveNumber = CheckpointSaveNumber3;

                break;
            default:
                break;
        }
    }
    public void ChallengeCompelete()
    {     
                switch (SceneManager.GetActiveScene().name)
                {
                    case "RedBullet_BlockOut":
                        //GreenChallengeCompelete1;
                        RedChallengeCompelete1 = true;
                        SaveLoad.Save<bool>(RedChallengeCompelete1, "RedPuzzleChallege");
                        break;
                    case "GreenBullet_Blockout":
                        GreenChallengeCompelete1 = true;
                        SaveLoad.Save<bool>(GreenChallengeCompelete1, "GreenPuzzleChallege");
                        break;
                        default:
                        break;
                }
               
    }
    public void RoomFinish()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "RedBullet_BlockOut":
                //GreenChallengeCompelete1;
                RedUnlock1 = true;
                SaveLoad.Save<bool>(RedUnlock1, "RedRoomFinish");
                break;
            case "BlueBullet_Blockout":
                BlueUnlock1 = true;
                SaveLoad.Save<bool>(BlueUnlock1, "BlueRoomFinish");
                break;
            case "GreenBullet_Blockout":
                GreenUnlock1 = true;
                SaveLoad.Save<bool>(GreenUnlock1, "GreenRoomFinish");
                break;
            case "YellowBullet_Blockout":
                YellowUnlock1 = true;
                SaveLoad.Save<bool>(YellowUnlock1, "YellowRoomFinish");
                break;
            default:
                break;
        }
    }
   
    public void UpdateStats(UpgradeSO upgrade, int currLevel)
    {
        if (upgrade == HealthUpgrade)
        {
            //Debug.Log("Old maxHealth: " + maxHealth);
            maxHealth = HealthUpgrade.statList[currLevel];
            //Debug.Log("New maxHealth: " + maxHealth);
            curHealth = maxHealth;
            myHB.UpdateBar(1);
        }
        else if (upgrade == ArmorUpgrade)
        {
            //Debug.Log("Old maxArmor: " + maxArmor);
            maxArmor = ArmorUpgrade.statList[currLevel];
            //Debug.Log("New maxArmor: " + maxArmor);
            curArmor = maxArmor;
            SetArmor();
        }
    }
  
    }

    
    
