using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.VFX.Utils;
using UnityStandardAssets.Characters.FirstPerson;

public enum bulletType {Blast, Corrosive, Freeze, Heal, Shock, Void, Plain, NULL};

public struct bullet //struct for a bullet. Holds the bullet image and the color of the bullet. Images are more directly controlled by myBulletImages
{
    public bulletType Target;

    public bullet(bulletType target)
    {
        Target = target;
    }
}

public class Gun : MonoBehaviour
{
    /* this code is good as hell wtf, i didn't know you could do something when a value was set that makes shit so much easier and modular.
       string _life;
       life
   {
      get
      { 
         //Do something else
         return _life;
      }
   set
      { 
         if (value <= 0)
         {
            this.Destroy();
            _life = value;
            //keep doing things here or before
         }
      }
   */
    #region Variables
    private float fov;

    public AnumationOrder Anumation;
    public static Gun instance;

    public GunScriptableObject gunValues; //a scriptable object used to hold the values for the gun, things like the damage falloff curve, range and fire rate are found here

    [SerializeField] private int numPerfects = 0; //number of perfect hits in a row, once it reaches 3 the attack will deal double damage over a standard perfect hit IE 3x damage

    public int baseDamage = 2;

    public bool usesCombo = true;

    [SerializeField] private WaitForSeconds reloadWait = new WaitForSeconds(.6f); //waitforseconds used for reload coroutine, timing is .6 seconds for each round

    public Transform sightPivot; //the pivot of the sight, used for ads rotation changes

    public GameObject sights;

    public GameObject sightscolor1;

    public GameObject sightscolor2;

    private AudioSource myAudio; //where we play the reload and eventually firing sounds from

    private CameraMovement myCamMovement; //controls camera sensitivity, smoothing, etc.

    private float lastShot; //when the last shot was fired, used to determine when we can fire next based off of fire rate
    
    public Queue<bullet> magazine; //Queue of bullets used as a magazine, keeps track of targets

    //private int curBullet = 1;

    private Camera fpsCam; //the camera we are looking through

    private Loadouts myLoadouts; //player's bandolier Loadouts

    private int currentLoadout; //current loadout selected

    private int numLoadouts; //number of loadouts the player has available to them

    public Image baseCrosshairs; //the image of the revolver magazine

    public List<Image> myBulletImages; //the images of the revolver bullets above the magazine

    public List<Image> myBulletBackgrounds; //the images that are backgrounds for each bullet image

    public List<Color> myPartColors; //the colors we use for each body part

    public List<GameObject> myParticleEffects;

    public List<Sprite> myCrosshairImages;

    private Image SpecialCrosshairs;

    private LineRenderer laserLine; //the line that draws the lazers, currently disabled because it looks like poop, is confusing

    private SpecialShots mySpecialShots; //holds the list of desperado shots that the player has acquired

    private bool isReloading = false; //whether we are in the process of reloading

    private AudioClip FireClip;
    private AudioClip ReloadClip;

    [SerializeField] public bool RedActive = false;
    [SerializeField] public bool BlueActive = false;
    [SerializeField] public bool YellowActive = false;
    [SerializeField] public bool GreenActive = false;
    [SerializeField] public bool CyanActive = false;
    [SerializeField] public bool PurpleActive = false;

    private Image RedIndicator;
    private Image BlueIndicator;
    private Image YellowIndicator;
    private Image GreenIndicator;
    private Image CyanIndicator;
    private Image PurpleIndicator;

    private float redChance = .16f; //float from 0-1 to represent chances
    private float greenChance = .16f; //float from 0-1 to represent chances
    private float blueChance = .16f; //float from 0-1 to represent chances
    private float yellowChance = .16f; //float from 0-1 to represent chances
    private float cyanChance = .16f; //float from 0-1 to represent chances
    private float purpleChance = .16f; //float from 0-1 to represent chances

    private AudioClip[] ComboClips;

    private PauseMenu pauseMenu;

    //private bool stopReload = false;

    public GunAnimationScript myGAS;

    private float manTime;

    public GameObject tellLight;

    //private bool AutoSlowTimeForReload = true;

    //private bool hasTried = false;
    
    private Color origColor;

    public GameObject myCrystal;

    public float ADSWeight;

    private float ADSSpeed = 5;

    //private FirstPersonController myController;

    //private List<GameObject> HealEffects;

    private AudioMixer pitchMix;

    private bool stopReload = false;

    public int activeBandolier = 0;

    private GameObject SpeedReloadParent;

    private GameObject CircleParent;
    private GameObject ActiveCircle;
    private GameObject LeftCircle;
    private GameObject BottomCircle;
    
    private bool colorSet = false;

    private bulletType setBulletType;

    public bool takingInput = true;

    public Healthbar deadeyeBar;
    private float DeadeyeTotal = 100f;

    [SerializeField] private float minHoldTime = .25f;
    float reloadTimer = 0f;
    bool reloadPressed = false;

    public bool speedReloadEnabled = false;

    public int numSpeedReloaders = 2;

    public int maxSpeedReloaders = 2;

    public static float NormalFov = 90f;
    private float ADSFov = 50f;

    private float deadEyeDecrement = 1f;//used to fade the player in and out, tracks the timescale in here

    public float DeadeyeDrainSpeed = 15f;

    public float DeadeyeFillSpeed = 5f;

    bulletType nextBul = bulletType.NULL;
    bool hasLoaded = false;

    private bool done = false;

    private bool fireStorage = false;

    private bool isSlowed = false;

    private int[] curBandolier;
    private int[] nextBandolier;
    private int[] prevBandolier;

    private int BandolierProgress = -1;

    public int TotalUnlocked = 0;

    private KeyCode blastKeyCode = KeyCode.None;
    private KeyCode corrosiveKeyCode = KeyCode.None;
    private KeyCode freezeKeyCode = KeyCode.None;
    private KeyCode healKeyCode = KeyCode.None;
    private KeyCode shockKeyCode = KeyCode.None;
    private KeyCode voidKeyCode = KeyCode.None;

    [SerializeField] private UpgradeSO DeadeyeUpgrade;
    [SerializeField] private UpgradeSO BackupsUpgrade;
    [SerializeField] private UpgradeSO DamageUpgrade;
    

    #endregion

    private void Awake()
    {
        Anumation = GameObject.Find("NotPauseCanvas").GetComponent<AnumationOrder>();

        FireClip = Resources.Load("Sounds/Gunshot") as AudioClip;
        ReloadClip = Resources.Load("Sounds/Reload") as AudioClip;

        BlueIndicator = GameObject.Find("FreezeIndicator").transform.GetChild(1).GetComponent<Image>();
        RedIndicator = GameObject.Find("BlastIndicator").transform.GetChild(1).GetComponent<Image>();
        GreenIndicator = GameObject.Find("HealIndicator").transform.GetChild(1).GetComponent<Image>();
        YellowIndicator = GameObject.Find("CorrosiveIndicator").transform.GetChild(1).GetComponent<Image>();
        CyanIndicator = GameObject.Find("ShockIndicator").transform.GetChild(1).GetComponent<Image>();
        //PurpleIndicator = GameObject.Find("VoidIndicator").transform.GetChild(1).GetComponent<Image>();
        baseCrosshairs = GameObject.Find("Crosshairs").GetComponent<Image>();
        SpecialCrosshairs = GameObject.Find("CrosshairsSpecial").GetComponent<Image>();

        ActiveCircle = GameObject.Find("RuneCircleActive");
        LeftCircle = GameObject.Find("RuneCircleLeft");
        BottomCircle = GameObject.Find("RuneCircleBottom");
        CircleParent = ActiveCircle.transform.parent.gameObject;
        SpeedReloadParent = GameObject.Find("SpeedReloadTracker");

        sights = myParticleEffects[6].transform.parent.Find("Sight").GetChild(0).gameObject;
        sightscolor1 = myParticleEffects[6].transform.parent.Find("SightColor1").GetChild(0).gameObject;
        sightscolor2 = myParticleEffects[6].transform.parent.Find("SightColor2").GetChild(0).gameObject;

        myLoadouts = GetComponentInChildren<Loadouts>();

        myCamMovement = GetComponent<CameraMovement>();

        myGAS = GetComponentInChildren<GunAnimationScript>();

        //HealEffects = new List<GameObject>();
        ComboClips = new AudioClip[6];
        for (int i = 0; i < 6; i++) 
        {
            ComboClips[i] = Resources.Load("Sounds/SFX_Combo_" + (i + 1).ToString()) as AudioClip;
        }

        for(int i = 1; i < 7; i++)
        {
            myBulletImages[i - 1] = GameObject.Find("Bullet " + i.ToString()).GetComponent<Image>();
        }
        pitchMix = Resources.Load("Audio/AudioMixer") as AudioMixer;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        instance = this;
       
        LoadGun();  
    }

    private void Start()
    {
        //myController = GetComponentInParent<FirstPersonController>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        ADSWeight = 0;
        SetIndicators();
        lastShot = Time.time;
        myAudio = GetComponent<AudioSource>(); //audio source for playing reload and firing noises
        mySpecialShots = GetComponent<SpecialShots>(); //component that holds the desperado shots for the player
        LoadShots(); //loads shots in manually, for now until we can make prefabs or a text doc to store them
        //laserLine = GetComponent<LineRenderer>();
        fpsCam = GetComponent<Camera>(); //camera attached to the player, main viewport
        magazine = new Queue<bullet>(); //queue that tracks the magazine, uses bullet struct

        UpdateSpellCircleUI();

        for (int i = 0; i < 6; i++) //loads the initial magazine for the player
        {
            //if (!myBulletBackgrounds[i])
            {
                myBulletBackgrounds[i] = GameObject.Find("BulletBackground " + (i + 1).ToString()).GetComponent<Image>();
            }
            //myBulletImages[i] = GameObject.Find("Bullet " + (i + 1).ToString()).GetComponent<Image>();
            bullet temp;
            if (TotalUnlocked > 1)
            {
                temp = LoadFromBandolier(true);
            }
            else
            {
                temp = randBul();
            }
            
            Color c = myPartColors[(int)temp.Target];
            myBulletImages[i].color = c;
            magazine.Enqueue(temp); //loads the bullet
            myGAS.SetBulletColor(i, (int)temp.Target);
        }
        SetFlairColor(myBulletImages[0].color);
        setCrystalTracker(0);
        var fov = Camera.main.fieldOfView;

        sights.SetActive(false);
        sightscolor1.SetActive(false);
        sightscolor2.SetActive(false);

        baseCrosshairs.gameObject.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        blastKeyCode = Anumation.GetRuneKey(bulletType.Blast);
        freezeKeyCode = Anumation.GetRuneKey(bulletType.Freeze);
        healKeyCode = Anumation.GetRuneKey(bulletType.Heal);
        corrosiveKeyCode = Anumation.GetRuneKey(bulletType.Corrosive);
        shockKeyCode = Anumation.GetRuneKey(bulletType.Shock);
        voidKeyCode = Anumation.GetRuneKey(bulletType.Void);

        if (SaveLoad.SaveExists("SpeedReloaders"))
        {
            numSpeedReloaders =  SaveLoad.Load<int>("SpeedReloaders");
        }

        //SetEmblems(false);
    }

    private void Update()
    {
        
        if (Time.timeScale == 0) return;
        if (takingInput)
        {
            if (GetComponentInParent<PlayerController>().isRunning)
            {
                stopReload = true;
            }

            if (Input.GetButton("Fire2")) //hold RMB to go slow-mo mode
            {
                if (ADSWeight < 1f)
                {
                    ADSWeight += ADSSpeed * Time.deltaTime;
                }
                else
                if (ADSWeight > 1f)
                {
                    ADSWeight = 1f;

                    sights.SetActive(true);
                    sightscolor1.SetActive(true);
                    sightscolor2.SetActive(true);

                    baseCrosshairs.gameObject.SetActive(false);
                }
            }
            else  //if(Input.GetButtonUp("Fire2"))
            {
                if (ADSWeight > 0f)
                {
                    ADSWeight -= ADSSpeed * Time.deltaTime;
                    //myController.StopSprintCheck();
                }
                else
                if (ADSWeight < 0f)
                {
                    ADSWeight = 0f;

                    sightPivot.localRotation = Quaternion.Euler(Vector3.zero);

                    sights.SetActive(false);
                    sightscolor1.SetActive(false);
                    sightscolor2.SetActive(false);

                    baseCrosshairs.gameObject.SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.E) && TotalUnlocked > 1)
            {
                activeBandolier++;
                if (activeBandolier > TotalUnlocked - 1)
                {
                    activeBandolier = 0;
                }
            }

            DeadEyeTime();
            deadeyeBar.UpdateBar(DeadeyeTotal / 100f);
            //Debug.Log(DeadeyeTotal / 100f);
            myGAS.EnterADS(ADSWeight);
            myCamMovement.ADSSensitivty(ADSWeight);
            var fov = Mathf.Clamp(NormalFov - ADSWeight * (NormalFov - ADSFov), ADSFov, NormalFov);
            //Debug.Log(fov + " " + NormalFov + " " + ADSFov);
            Camera.main.fieldOfView = fov;


            Ray ray = fpsCam.ViewportPointToRay(new Vector3(.5f, .5f, 0));

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Set Reticle");
                var hitCrystal = hit.collider.GetComponent<crystal>();
                if (hitCrystal)
                {
                    SetReticle(hitCrystal.myColor);
                }
                else
                {
                    var tag = hit.collider.tag;
                    switch (tag)
                    {
                        case "Blast":
                            SetReticle(bulletType.Blast);
                            break;
                        case "vfx":
                            SetReticle(bulletType.Blast);
                            break;
                        case "Corrosive":
                            SetReticle(bulletType.Corrosive);
                            break;
                        case "Freeze":
                            SetReticle(bulletType.Freeze);
                            break;
                        case "Cart":
                            SetReticle(bulletType.Freeze);
                            break;
                        case "Heal":
                            SetReticle(bulletType.Heal);
                            break;
                        case "greenEffect":
                            SetReticle(bulletType.Heal);
                            break;
                        case "Shock":
                            SetReticle(bulletType.Shock);
                            break;
                        case "Void":
                            SetReticle(bulletType.Void);
                            break;
                        case "Plain":
                            SetReticle(bulletType.Plain);
                            break;
                        default:
                            SetReticle(bulletType.NULL);
                            break;
                    }

                }

            }

            if (Input.GetKey(blastKeyCode) && RedActive && magazine.Contains(new bullet(bulletType.NULL)) && !isReloading && (lastShot + gunValues.fireRate <= Time.time)) //turn off/on red gui
            {
                //Debug.Log("Ye");
                isReloading = true;
                StartCoroutine(reloadSingle(new bullet(bulletType.Blast)));
            }
            if (Input.GetKey(freezeKeyCode) && BlueActive && magazine.Contains(new bullet(bulletType.NULL)) && !isReloading && (lastShot + gunValues.fireRate <= Time.time)) // ^^^ Blue Gui
            {
                //Debug.Log("Ye");
                isReloading = true;
                StartCoroutine(reloadSingle(new bullet(bulletType.Freeze)));
            }
            if (Input.GetKey(healKeyCode) && GreenActive && magazine.Contains(new bullet(bulletType.NULL)) && !isReloading && (lastShot + gunValues.fireRate <= Time.time)) // ^^^ Green Gui
            {
                //Debug.Log("Ye");
                isReloading = true;
                StartCoroutine(reloadSingle(new bullet(bulletType.Heal)));
            }
            if (Input.GetKey(corrosiveKeyCode) && YellowActive && magazine.Contains(new bullet(bulletType.NULL)) && !isReloading && (lastShot + gunValues.fireRate <= Time.time)) // ^^^ Yellow Gui
            {
                //Debug.Log("Ye");
                isReloading = true;
                StartCoroutine(reloadSingle(new bullet(bulletType.Corrosive)));
            }
            if (Input.GetKey(shockKeyCode) && CyanActive && magazine.Contains(new bullet(bulletType.NULL)) && !isReloading && (lastShot + gunValues.fireRate <= Time.time)) // ^^^ Cyan Gui
            {
                //Debug.Log("Ye");
                isReloading = true;
                StartCoroutine(reloadSingle(new bullet(bulletType.Shock)));
            }
            if (Input.GetKey(voidKeyCode) && PurpleActive && magazine.Contains(new bullet(bulletType.NULL)) && !isReloading && (lastShot + gunValues.fireRate <= Time.time)) // ^^^ Purple Gui
            {
                //Debug.Log("Ye");
                isReloading = true;
                StartCoroutine(reloadSingle(new bullet(bulletType.Void)));
            }

            if ((Input.GetButtonDown("Fire1") || fireStorage) && (lastShot + gunValues.fireRate <= Time.time) && magazine.Count > 0) //if presses "Fire1", has bullets and fire rate allows for it
            {
                if (isReloading && !fireStorage) //if is reloading
                {
                    stopReload = true; //set variable
                    fireStorage = true;
                }
                else
                {
                    lastShot = Time.time;
                    fireStorage = false;
                    isReloading = false;
                    Fire();
                }
            }

            if (speedReloadEnabled && !isReloading && (lastShot + gunValues.fireRate <= Time.time))
            {
                Debug.Log("Got Into Reload Area");

                if ((Input.GetKeyDown("r"))) //if hits "r" down, isn't reloading already, waiting for gun to finish firing
                {
                    Debug.Log("Reload pressed Down");
                    reloadPressed = true; //reload is now pressed
                    reloadTimer = Time.time; //set the baseline for the timer
                }

                if (Input.GetKeyUp("r")) //if they let go of the key, constituting a press instead of a hold also check to make sure reload is currently pressed
                {
                    if (reloadPressed)
                    {
                        Debug.Log("Reload released");
                        reloadPressed = false; //reload is no longer pressed
                        stopReload = false;
                        isReloading = true; //we are now reloading
                        StartCoroutine("reload"); //start normal reload
                    }
                    else
                    {
                        reloadPressed = false; //reload is no longer pressed
                        stopReload = false;
                        isReloading = false; //we are now reloading
                    }
                }

                if (Input.GetKey("r") && reloadPressed) //if 'r' key is being held and reload is pressed which means the player is still holding r
                {
                    if (Time.time - reloadTimer > minHoldTime && numSpeedReloaders > 0) //if the specified amount of time has passed, and the player has a speed reloader
                    {
                        Debug.Log("Speed Reload");
                        reloadPressed = false; //reload is no longer pressed
                        stopReload = false;
                        isReloading = true; //we are now reloading
                        numSpeedReloaders--;
                        SaveLoad.Save(numSpeedReloaders, "SpeedReloaders");
                        StartCoroutine("speedReload"); //start a speed reload
                    }
                }
            } //speed reload system
            else //speed reload is disabled, this is a normal reload. Automatically realods when empty
            {
                if ((Input.GetKeyDown("r") || isEmpty()) && !isReloading && (lastShot + gunValues.fireRate <= Time.time)) //if hits "r", and not already reloading, and you wait for the gun animation to play, and you aren't doing a speed reload
                {
                    Debug.Log("Normal Reload");
                    isReloading = true;
                    StartCoroutine("reload");
                }

            }



            if (Input.mouseScrollDelta.y != 0 && !isReloading)
            {
                /*if (isReloading) //if is reloading
                {
                    stopReload = true;
                }*/
                manTime = Time.time;
                if (Input.mouseScrollDelta.y > 0) //makes sure scroll wheel is up, then rotates
                {
                    ManualRevolve(0, false); //rotates counter-clockwise
                }
                else
                {
                    ManualRevolve(magazine.Count - 1, true); //rotates clockwise
                }
                SetFlairColor(myBulletImages[0].color);
            }


            myGAS.SetHorz(Mathf.Clamp(myCamMovement._smoothMouse.y, -1, 1));
            myGAS.SetVert(Mathf.Clamp(myCamMovement._smoothMouse.x, -1, 1));

            if (ADSWeight == 1)
            {
                Quaternion DestinationRotation = Quaternion.Euler(new Vector3(1, Mathf.Clamp(myCamMovement._smoothMouse.x, -2, 2), Mathf.Clamp(myCamMovement._smoothMouse.x, -2, 2) * 5f));
                sightPivot.localRotation = Quaternion.Slerp(sightPivot.localRotation, DestinationRotation, .1f);
                //Debug.Log(sightPivot.localRotation);
            }
            SetFlairColor(myBulletImages[0].color);
        }
        UpdateSpellCircleUI();
    }
   
    public void GiveSpeedReloader()
    {
        if (maxSpeedReloaders > numSpeedReloaders)
        {
            numSpeedReloaders++;
            SaveLoad.Save(numSpeedReloaders, "SpeedReloaders");
        }
    }

    void LoadGun()
    {
        if (SaveLoad.SaveExists("RedAcitve"))
        {
           
        RedActive = SaveLoad.Load<bool>("RedAcitve");
            
           
        }
        if (SaveLoad.SaveExists("BlueActive"))
        {
        
        BlueActive = SaveLoad.Load<bool>("BlueActive");
         
           
        }
        if (SaveLoad.SaveExists("GreenActive"))
        {
           
            GreenActive = SaveLoad.Load<bool>("GreenActive");
            
        }
        if (SaveLoad.SaveExists("YellowActive"))
        {
            
             YellowActive = SaveLoad.Load<bool>("YellowActive");
            
        }
        if (SaveLoad.SaveExists("CyanActive"))
        {
            
             CyanActive = SaveLoad.Load<bool>("CyanActive");

            
        }
        if (SaveLoad.SaveExists("PurpleActive"))
        {
        
            PurpleActive = SaveLoad.Load<bool>("");
            
        }
    }

    public int numBulletsUnlocked()
    {
        return ((RedActive ? 1 : 0) + (BlueActive ? 1 : 0) + (GreenActive ? 1 : 0) + (YellowActive ? 1 : 0) + (CyanActive ? 1 : 0) + (PurpleActive ? 1 : 0));
    }

    public int[] whichBulletsActive()
    {
        int[] container = new int[6];
        int count = 0;
        if (RedActive)
        {
            container[count] = (int)bulletType.Blast;
            count++;
        }
        if (BlueActive)
        {
            container[count] = (int)bulletType.Freeze;
            count++;
        }
        if (GreenActive)
        {
            container[count] = (int)bulletType.Heal;
            count++;
        }
        if (YellowActive)
        {
            container[count] = (int)bulletType.Corrosive;
            count++;
        }
        if (CyanActive)
        {
            container[count] = (int)bulletType.Shock;
            count++;
        }
        if (PurpleActive)
        {
            container[count] = (int)bulletType.Void;
            count++;
        }
        return container;
    }

    public void unlockColor(char c)
	{
        
		switch(c)
		{
			case 'r': 
				RedActive = true;
                SetAllSame(bulletType.Blast);
                //add different activated to the RuneOrder
                Anumation.RuneOrder.Add("RedActive" + Anumation.count);
                
                Loadouts.LO.UnlockLO(0);
                //call the anumation function to turn on an icon.
                Anumation.TurnOnRune();
                //add to the count 
                Anumation.count++;
                SaveLoad.Save(RedActive, "RedAcitve");
                blastKeyCode = Anumation.GetRuneKey(bulletType.Blast);
                Debug.Log(blastKeyCode);

                break;
			case 'b':
				BlueActive = true;
                SetAllSame(bulletType.Freeze);
                Anumation.RuneOrder.Add("BlueActive" + Anumation.count);
                Loadouts.LO.UnlockLO(2);
                Anumation.TurnOnRune();

                Anumation.count++;
                SaveLoad.Save(BlueActive, "BlueActive");

                freezeKeyCode = Anumation.GetRuneKey(bulletType.Freeze);
                Debug.Log(freezeKeyCode);

                break;
			case 'g':
				GreenActive = true;
                SetAllSame(bulletType.Heal);
                Anumation.RuneOrder.Add("GreenActive" + Anumation.count);
                Anumation.TurnOnRune();

                Anumation.count++;
                Loadouts.LO.UnlockLO(3);
                healKeyCode = Anumation.GetRuneKey(bulletType.Heal);
                Debug.Log(healKeyCode);
                SaveLoad.Save(GreenActive, "GreenActive");


                break;
			case 'o':
				YellowActive = true;
                SetAllSame(bulletType.Corrosive);
                Anumation.RuneOrder.Add("YellowActive" + Anumation.count);
                Loadouts.LO.UnlockLO(1);
                Anumation.TurnOnRune();

                Anumation.count++;

                corrosiveKeyCode = Anumation.GetRuneKey(bulletType.Corrosive);
                SaveLoad.Save(YellowActive, "YellowActive");
                Debug.Log(corrosiveKeyCode);

                break;
            case 'c':
                CyanActive = true;
                SetAllSame(bulletType.Shock);
                Anumation.RuneOrder.Add("CyanActive" + Anumation.count);
                Anumation.TurnOnRune();

                Anumation.count++;
                SaveLoad.Save(CyanActive, "CyanActive");

                Loadouts.LO.UnlockLO(4);
                shockKeyCode = Anumation.GetRuneKey(bulletType.Shock);
                break;
            case 'p':
                PurpleActive = true;
                SetAllSame(bulletType.Void);
                Anumation.RuneOrder.Add("PurpleActive" + Anumation.count);
                Anumation.TurnOnRune();

                Anumation.count++;

                Loadouts.LO.UnlockLO(5);
                SaveLoad.Save(PurpleActive, "PurpleActive");

                voidKeyCode = Anumation.GetRuneKey(bulletType.Void);
                break;
        }
        SetIndicators();
    }
    
    private void SetAllSame(bulletType myPart)
    {
        magazine.Clear();
        for(int i = 0; i < 6; i++)
        {
            magazine.Enqueue(new bullet(myPart));
            myBulletImages[i].color = myPartColors[(int)myPart];
            myGAS.SetBulletColor(i, (int)myPart);
        }
        myGAS.ResetMAg();
        SetFlairColor(myBulletImages[0].color);
        SetIndicatorsColor();
        setBulletType = myPart;
        colorSet = true;
    }

    private void LoadShots()
    {
        DesperadoShot temp;
        temp.bPart = bulletType.Freeze;
        temp.passiveDuration = 1f;
        temp.activeDuration = 5f;
        mySpecialShots.equipShot(temp);
        temp.bPart = bulletType.Corrosive;
        temp.passiveDuration = 2.5f;
        temp.activeDuration = 4.5f;
        mySpecialShots.equipShot(temp);
        temp.bPart = bulletType.Blast;
        temp.passiveDuration = 1f;
        temp.activeDuration = 2f;
        mySpecialShots.equipShot(temp);
        temp.bPart = bulletType.Heal;
        temp.passiveDuration = 20f;
        temp.activeDuration = 100f;
        mySpecialShots.equipShot(temp);
        temp.bPart = bulletType.Shock;
        temp.passiveDuration = 5f;
        temp.activeDuration = 10f;
        mySpecialShots.equipShot(temp);
        temp.bPart = bulletType.Void;
        temp.passiveDuration = 5f;
        temp.activeDuration = 10f;
        mySpecialShots.equipShot(temp);
    }

    public void AddDeadeye(float amount)
    {
        DeadeyeTotal = Mathf.Clamp(DeadeyeTotal + amount, 0f, 100f);
    }

    public bool SetCrosshairColor = false;

    public bool SetCrosshairShape = true;

    public float crosshairOpacity = 1f;

    private void SetReticle(bulletType type)
    {
        if (type != bulletType.NULL && (SetCrosshairShape || SetCrosshairColor))
        {
            SpecialCrosshairs.color = new Color(SpecialCrosshairs.color.r, SpecialCrosshairs.color.g, SpecialCrosshairs.color.b, AudioManager.TranspercyValueSaved);
            SpecialCrosshairs.gameObject.SetActive(true);

            if (SetCrosshairShape)
            {
                SpecialCrosshairs.sprite = myCrosshairImages[(int)type];
            }
            else if(SetCrosshairShape == false)
            {
                SpecialCrosshairs.gameObject.SetActive(false);
            }
            if (SetCrosshairColor)
            {
                SpecialCrosshairs.color = myPartColors[(int)type];
                SpecialCrosshairs.color = new Color(SpecialCrosshairs.color.r, SpecialCrosshairs.color.g, SpecialCrosshairs.color.b,AudioManager.TranspercyValueSaved);
            }
            else
            {
                SpecialCrosshairs.color = myPartColors[6];
            }
        }
        else
        {
            SpecialCrosshairs.gameObject.SetActive(false);
        }
    }

    private IEnumerator ComboSound(float delay, int clip)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log(clip);
        if (clip < 7 && clip > 0)
        {
            myAudio.PlayOneShot(ComboClips[clip - 1]);
            if (usesCombo)
            {
                for (int i = 0; i < numPerfects; i++)
                {
                    myBulletBackgrounds[i].enabled = true;
                }
                for (int i = numPerfects; i < 6; i++)
                {
                    myBulletBackgrounds[i].enabled = false;
                }
            }
        }
    }

    private void DeadEyeTime()
    {
        //slow time, decrement deadeyetotal,

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isSlowed = !isSlowed;
        }

        if (DeadeyeTotal > 0 && isSlowed)
        {
            if(deadEyeDecrement > .5f)
            {
                deadEyeDecrement -= ADSSpeed * Time.deltaTime;
            }
            if(deadEyeDecrement <= .5f)
            {
                deadEyeDecrement = .5f;
            }
            Time.timeScale = deadEyeDecrement;
            Time.fixedDeltaTime = .02f * Time.timeScale;
            Time.maximumParticleDeltaTime = .03f * Time.timeScale;
            DeadeyeTotal -= Time.fixedUnscaledDeltaTime * DeadeyeDrainSpeed;
            pitchMix.SetFloat("Pitch", Mathf.Clamp(Time.timeScale, .75f, 1));
            return;
        }
        else//break case, player has no deadeye left :(
        {
            isSlowed = false;
            if (deadEyeDecrement < 1f)
            {
                deadEyeDecrement += ADSSpeed * Time.deltaTime;
            }
            if (deadEyeDecrement >= 1f)
            {
                deadEyeDecrement = 1f;
            }
            Time.timeScale = deadEyeDecrement;
            Time.fixedDeltaTime = .02f * Time.timeScale;
            Time.maximumParticleDeltaTime = .03f * Time.timeScale;
            pitchMix.SetFloat("Pitch", Mathf.Clamp(Time.timeScale, .75f, 1));
            if (DeadeyeTotal < TotalDeadeye)
            {
                DeadeyeTotal += Time.fixedUnscaledDeltaTime * DeadeyeFillSpeed;
            }
            else
            {
                DeadeyeTotal = TotalDeadeye;
            }
            return;
        }
    }

    private float TotalDeadeye;

    private IEnumerator reloadSingle(bullet loading)
    {
        //Debug.Log("Ye2");
        //myController.StopSprintCheck();
        done = false;
        myGAS.GunAnimator.SetBool("Done Reloading", false);
        while (!done)
        {
            hasLoaded = false;
            var time = new WaitForSeconds(.3f * .15f);
            var arMag = magazine.ToArray();
            if (arMag[5].Target == bulletType.NULL)
            {
                var t = Time.time;
                var temp = loading;
                nextBul = loading.Target;
                myGAS.PlayReloadAnimation();
                myGAS.SetBulletColor(6, (int)temp.Target);
                while (!hasLoaded)
                {
                    if (stopReload)
                    {
                        Debug.Log("StopReload");
                        isReloading = false;
                        myGAS.GunAnimator.SetBool("Done Reloading", true);
                        stopReload = false;
                        fireStorage = true;
                        yield break;
                    }
                    yield return null;
                }
                while (t + (.3f * .1f) > Time.time)
                {
                    yield return null;
                }
                //LoadBullet();
                done = true;
            }
            else
            {
                time = new WaitForSeconds(.1f);
            }
            //Debug.Log("Ye3");
            ManualRevolve(5, true);
            SetFlairColor(myBulletImages[0].color);
            yield return time;
        }
        isReloading = false;
        myGAS.GunAnimator.SetBool("Done Reloading", true);
        //myController.StartSprintCheck();
    }

    private IEnumerator reload() //manuvers the magazine and reloads one bullet at a time, loading into the spot left of the firing point
    {
        Debug.Log("Try Reload");
        BandolierProgress = -1;
        while (magazine.Contains(new bullet(bulletType.NULL)) && !stopReload)
        {
            var arMag = magazine.ToArray();
            if (arMag.Length == 6)
            {
                Debug.Log("Start Reload");
                hasLoaded = false;
                myGAS.GunAnimator.SetBool("Done Reloading", false);
                var time = new WaitForSeconds(.3f * .15f); //wait time if the reload animation goes off
                if (arMag[5].Target == bulletType.NULL)
                {
                    var t = Time.time;
                    bullet temp;
                    if (TotalUnlocked > 0)
                    {
                        temp = LoadFromBandolier(false);
                    }
                    else
                    {
                        temp = randBul();
                    }
                    nextBul = temp.Target;
                    myGAS.SetBulletColor(6, (int)temp.Target);
                    myGAS.PlayReloadAnimation();
                    while (!hasLoaded)
                    {
                        if (stopReload) //stops the player's realoading, due to firing
                        {
                            Debug.Log("StopReload");
                            isReloading = false;
                            myGAS.GunAnimator.SetBool("Done Reloading", true);
                            stopReload = false;
                            yield break;
                        }
                        yield return null;
                    }
                    while (t + (.3f * .1f) > Time.time)
                    {
                        yield return null;
                    }
                }
                else
                {
                    time = new WaitForSeconds(.1f); //waits a 10th of a second to allow chamber to rotate, this is the case where the slot is full
                }
                ManualRevolve(5, true);
                SetFlairColor(myBulletImages[0].color);
                yield return time; //this is where we wait
            }
        }
        BandolierProgress = -1;
        isReloading = false;
        myGAS.GunAnimator.SetBool("Done Reloading", true);
    }

    public void LoadBullet()
    {
        if (!speedReloading)
        {
            Debug.Log("LoadBulletGun");
            myGAS.SetBulletColor(5, (int)nextBul);
            var arMag = magazine.ToArray();
            var t = Time.time;
            myAudio.PlayOneShot(ReloadClip);
            arMag[5] = new bullet(nextBul);
            myBulletImages[5].color = myPartColors[(int)nextBul];
            SetFlairColor(myBulletImages[0].color);
            magazine.Clear();
            foreach (bullet b in arMag)
            {
                magazine.Enqueue(b);
            }
            hasLoaded = true;
        }
    }

    public bool speedReloading = false;

    private IEnumerator speedReload()
    {

        speedReloading = true;

        Debug.Log("SpeedReloading");

        var t = Time.time;

        myGAS.GunAnimator.SetBool("Done Reloading", false);
        //start reload animation
        myGAS.PlayReloadAnimation();

        while (t + (.5f * .75f) > Time.time) //waits for animation to play
        {
            yield return null;
        }

        myAudio.PlayOneShot(ReloadClip);

        //generate new magazine
        bullet[] newMag = new bullet[6];
        Color c = myPartColors[0];
        var temp = new bullet();

        BandolierProgress = -1;

        /*if (TotalUnlocked > 0)
        {
            temp = LoadFromBandolier(false);
        }
        else
        {
            temp = randBul();
        }*/
        for (int i = 0; i < 6; i++)
        {
            if (TotalUnlocked > 0)
            {
                temp = LoadFromBandolier(false);
            }
            else
            {
                temp = randBul();
            }
            newMag[5 - i] = temp;
            c = myPartColors[(int)temp.Target];
            myBulletImages[i].color = c;
            myGAS.SetBulletColor(5 - i, (int)temp.Target);
        }

        SetFlairColor(myBulletImages[0].color);
        magazine.Clear();
        foreach (bullet b in newMag) //loads in new magazine
        {
            magazine.Enqueue(b);
        }

        while (t + (.5f * .1f) > Time.time) //waits for rest of animation
        {
            yield return null;
        }
        
        isReloading = false;
        myGAS.GunAnimator.SetBool("Done Reloading", true);
        speedReloading = false;
        //myController.StartSprintCheck();
    }

    private void ManualRevolve(int pos, bool Clockwise)//recursive function that simulates a queue for the images of bullets on screen. This one is manual, so it doesn't set any to NULL
    {
        //myController.StopSprintCheck();
        if ((pos == 0 && !Clockwise) || (pos == magazine.Count - 1 && Clockwise)) //start case
        {
            origColor = myBulletImages[pos].color; //saves the first color in order to assign it to the last bullet
            if (Clockwise)
            {
                //Debug.Log("Clockwise");
                StartCoroutine(myGAS.Chamber.GetComponent<ChamberScript>().RotateCW());
            }
            else
            {
                //Debug.Log("CounterClockwise");
                StartCoroutine(myGAS.Chamber.GetComponent<ChamberScript>().RotateCCW());
            }
        }
        if ((pos == magazine.Count - 1 && !Clockwise) || (pos == 0 && Clockwise)) //exit case, if pos has reached the last or first object in the list
        {
            //Debug.Log(pos + " " + Clockwise);
            myBulletImages[pos].color = origColor;
            if (!Clockwise) //if counter-clockwise, simply move the first item in queue to the end
            {
                magazine.Enqueue(magazine.Dequeue());
            }
            else //if clockwise, you need a temporary queue to hold the bullets before the final one, then you throw them all back on top of the final bullet, which is now the first bullet
            {
                Queue<bullet> temp = new Queue<bullet>();
                while(magazine.Count > 1) //move all but final bullet into temp queue
                {
                    temp.Enqueue(magazine.Dequeue());
                }
                while(temp.Count > 0) //move the temp queue back on top of the final bullet
                {
                    magazine.Enqueue(temp.Dequeue());
                }
            }
        }
        else //recursion
        {
            if (!Clockwise) //makes the difference between +1 and -1
            {
                myBulletImages[pos].color = myBulletImages[pos + 1].color; //assigns the color of the image to be the color of the next one
                
                ManualRevolve(pos + 1, Clockwise); //recursively calls the function
            }
            else
            {
                myBulletImages[pos].color = myBulletImages[pos - 1].color; //assigns the color of the image to be the color of the next one
                ManualRevolve(pos - 1, Clockwise); //recursively calls the function
            }
        }

        //myController.StartSprintCheck();
    }

    private void revolve(int pos)//recursive function that simulates a queue for the images of bullets on screen. This is automatic, so it assumes you just fired and sets the last bullet to inactive
    {
        //Debug.Log(pos);
        if(pos >= 5) //exit case
        {
            myBulletImages[pos].color = myPartColors[7];
        }
        else
        {
            //Debug.Log(pos);
            myBulletImages[pos].color = myBulletImages[pos + 1].color;
            revolve(pos + 1);
        }
        SetFlairColor(myBulletImages[0].color);
    }

    private void Bang()
    {
        Debug.Log("Bang");

        myGAS.FireCraziness((float)(numPerfects + 1f) * .1f);

        myGAS.PlayFireAnimation();

        myAudio.PlayOneShot(FireClip);

        myParticleEffects[6].GetComponent<ParticleSystem>().Play();
    }

    private void Fire()
    {
        Debug.Log("Fire");

        bullet cur = magazine.Dequeue(); //dequeues the bullet which is being fired

        BandolierProgress = -1;

        //myController.StopSprintCheck();


        //stopReload = true;

        //isReloading = false;

        if (cur.Target != bulletType.NULL)
        {
            Debug.Log("Not a Blank");

            Ray ray = fpsCam.ViewportPointToRay(new Vector3(.5f, .5f, 0));

            RaycastHit hit;

            GameObject obj = null;

            //laserLine.SetPosition(0, gunEnd.position);

            //int layerMask = 1 << 2;
            //layerMask = ~layerMask;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit Something");

                var isCrystal = false;
                var same = false;
                var tag = hit.collider.tag;
                var crys = hit.collider.GetComponent<crystal>();
                Debug.Log(tag);
                if (tag == cur.Target.ToString())
                {
                    same = true;
                }
                else if (crys)
                {
                    Debug.Log("Hit a Crystal");
                    if(((tag == "red" || tag == "blue") && cur.Target == crys.myColor) || tag == "overworld" || tag == "greenEffect")
                    {
                        same = true;
                        isCrystal = true;
                    }
                }
                int dam = bulletHit(same, isCrystal);
                Bang();
                var enem = hit.transform.gameObject.GetComponentInParent<Enemy>();
                if ((int)cur.Target < 6)
                {
                    Debug.Log("Spawning Particle Effect");
                    obj = Instantiate(myParticleEffects[(int)cur.Target], hit.point, Quaternion.Euler(hit.normal)) as GameObject;
                    if (cur.Target == bulletType.Heal)
                    {
                        foreach (var v in obj.GetComponent<VFXParameterBinder>().GetParameterBinders<VFXPositionBinder>())
                        {
                            if (v.Parameter.Contains("Attractive Target Poistion"))
                            {
                                v.Target = Player.Instance.transform;
                                //HealEffects.Add(obj);
                            }
                        }
                    }
                    else
                    {
                        if (enem)
                        {
                            obj.transform.SetParent(enem.transform);
                        }
                        obj.transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(90, 0, 0);
                    }
                }
                var curShot = mySpecialShots.checkShot(cur.Target);
                if (enem)
                {
                    Debug.Log("We Have Enemy Hit");
                    if (numPerfects < 6 && usesCombo)
                    {
                        numPerfects++;
                        StartCoroutine(ComboSound(.4f, numPerfects));
                    }
                    var stat = enem.GetComponent<StatusEffect>();
                    //Debug.Log("About To Apply Effect " + same + " " + cur.Target);
                    //Debug.Log(numPerfects);
                    if (stat)
                    {
                        switch (cur.Target) //handles bullet effects on enemies
                        {
                            case bulletType.Blast:
                                {

                                    if (same)
                                    {
                                        if (numPerfects == 6 || (!usesCombo))
                                        {
                                            stat.AddEffect(StatusEffect.EffectType.Explosion, curShot.activeDuration);
                                            ResetCombo();
                                        }
                                        else if (numPerfects < 6)
                                        {
                                            stat.AddEffect(StatusEffect.EffectType.Explosion, curShot.passiveDuration);
                                        }
                                    }
                                    else if (!usesCombo)
                                    {
                                        stat.AddEffect(StatusEffect.EffectType.Explosion, curShot.passiveDuration);
                                    }
                                    break;
                                }
                            case bulletType.Freeze:
                                {
                                    if (same)
                                    {
                                        if (numPerfects == 6 || (!usesCombo))
                                        {
                                            stat.AddEffect(StatusEffect.EffectType.Stun, curShot.activeDuration);
                                            ResetCombo();
                                        }
                                        else if (numPerfects < 6)
                                        {
                                            stat.AddEffect(StatusEffect.EffectType.Stun, curShot.passiveDuration);
                                        }
                                    }
                                    else if (!usesCombo)
                                    {
                                        stat.AddEffect(StatusEffect.EffectType.Stun, curShot.passiveDuration);
                                    }
                                    break;
                                }
                            case bulletType.Heal:
                                {
                                    if (same)
                                    {
                                        if (numPerfects == 6 || (!usesCombo))
                                        {
                                            Player.Instance.Damage((int)-curShot.activeDuration);
                                            ResetCombo();
                                        }
                                        else if (numPerfects < 6)
                                        {
                                            Player.Instance.Damage((int)-curShot.passiveDuration);
                                        }
                                    }
                                    else if (!usesCombo)
                                    {
                                        Player.Instance.Damage((int)-curShot.passiveDuration);
                                    }
                                    break;
                                }
                            case bulletType.Corrosive:
                                {
                                    if (same)
                                    {
                                        if (numPerfects == 6 || (!usesCombo))
                                        {
                                            stat.AcidPool();
                                            //stat.AddEffect(StatusEffect.EffectType.DoT, curShot.passiveDuration);
                                            ResetCombo();
                                        }
                                        else if (numPerfects < 6)
                                        {
                                            stat.AddEffect(StatusEffect.EffectType.DoT, curShot.passiveDuration);
                                        }
                                    }
                                    else if (!usesCombo)
                                    {
                                        stat.AddEffect(StatusEffect.EffectType.DoT, curShot.passiveDuration);
                                    }
                                    break;
                                }
                            case bulletType.Shock:
                                {
                                    if (same)
                                    {
                                        if (numPerfects == 6 || !usesCombo)
                                        {
                                            stat.AddEffect(StatusEffect.EffectType.ChainWeaken, curShot.activeDuration);
                                            ResetCombo();
                                        }
                                        else if (numPerfects < 6)
                                        {
                                            stat.AddEffect(StatusEffect.EffectType.Weaken, curShot.passiveDuration);
                                        }
                                    }
                                    else
                                    {
                                        stat.AddEffect(StatusEffect.EffectType.Weaken, curShot.passiveDuration);
                                    }
                                    break;
                                }
                            case bulletType.Void: //not yet implemented
                                {
                                    if (same)
                                    {
                                        if (numPerfects == 6 || (usesCombo))
                                        {
                                            stat.AddEffect(StatusEffect.EffectType.Explosion, curShot.activeDuration);
                                            ResetCombo();
                                        }
                                        else if (numPerfects < 6)
                                        {
                                            stat.AddEffect(StatusEffect.EffectType.Explosion, curShot.passiveDuration);
                                        }
                                    }
                                    break;
                                }

                            case bulletType.Plain:
                                break;
                            case bulletType.NULL:
                                break;
                            default:
                                break;
                        }
                    }
                    Debug.Log("Made it Past Bullet Effects on Enemy");
                    enem.hit(dam, myPartColors[(int)cur.Target]);
                }
                else if (tag == "newBullet")
                {
                    Debug.Log("Hitting Chest for new bullet");
                    hit.collider.GetComponentInParent<newBullet>().instantiateBullet();
                    Destroy(obj);
                } //chest
                else if ((tag == "red" || tag == "blue")) //crystals being hit
                {
                    Debug.Log("Actually Hitting The Crystal");
                    if (hit.transform.gameObject.GetComponent<crystal>().myColor == cur.Target)
                    {
                        if (obj)
                        {
                            obj.transform.SetParent(hit.transform);
                        }
                        Debug.Log("Red Hit");
                        if (!hit.transform.gameObject.GetComponent<crystal>().checkHit() && usesCombo)
                        {
                            numPerfects--;
                        }
                        var temp = hit.collider.gameObject.GetComponentInParent<Minecart>();
                        if (temp && cur.Target == bulletType.Freeze) //we hit a minecart crystal boys
                        {
                            //var stat = temp.gameObject.GetComponent<StatusEffect>();
                            //if (!stat)
                            //{
                            var stat = temp.gameObject.GetComponent<StatusEffect>();
                            //}
                            if (numPerfects == 6 || (!usesCombo))
                            {
                                stat.AddEffect(StatusEffect.EffectType.Stun, curShot.activeDuration);
                            }else
                            {
                                stat.AddEffect(StatusEffect.EffectType.Stun, curShot.passiveDuration);
                            }
                        }
                        if (cur.Target == bulletType.Heal && numPerfects == 6 || (!usesCombo))
                        {
                            Player.Instance.Damage((int)-mySpecialShots.checkShot(cur.Target).activeDuration * 10);
                            ResetCombo();
                        }else
                        if (cur.Target == bulletType.Heal && numPerfects < 6)
                        {
                            Player.Instance.Damage((int)-mySpecialShots.checkShot(cur.Target).activeDuration);
                        }
                    }
                } //crystals
                else if (tag == "overworld")
                {
                    Debug.Log("Black Hit");
                    hit.transform.gameObject.GetComponent<crystal>().hit = true;
                } //overworld crystals
                else if (hit.collider.tag == "vfx")
                {
                    Debug.Log("Hit VFX Thingy");
                    if (hit.collider.GetComponent<vfx_controller>().bulType == cur.Target)
                    {
                        hit.transform.gameObject.GetComponent<vfx_controller>().hit();
                    }
                } //vfx IE barrels and such
                else if (cur.Target == bulletType.Blast)
                {
                    foreach (Enemy e in FindObjectsOfType<Enemy>())
                    {
                            if (Vector3.Distance(e.transform.position, hit.point) < 4)
                            {
                                e.hit(1, myPartColors[0]);
                            }
                    }
                }
                else if (hit.collider.tag == "Cart" && cur.Target == bulletType.Freeze)
                {
                    Debug.Log("Hitting Minecart");
                    Destroy(obj);
                    var temp = hit.collider.GetComponent<Minecart>();
                    var stat = temp.gameObject.AddComponent<StatusEffect>();
                    if (numPerfects == 6 || (!usesCombo))
                    {
                        stat.AddEffect(StatusEffect.EffectType.Stun, curShot.activeDuration);
                    }else
                    {
                        stat.AddEffect(StatusEffect.EffectType.Stun, curShot.passiveDuration);
                    }
                    
                } //minecarts
                else if (tag == "greenEffect" && cur.Target == bulletType.Heal)
                {
                    Debug.Log("green Effect activator hit");
                    Player.Instance.Damage(-10);
                    hit.transform.GetComponentInParent<bridgeActivator>().Activate();
                } //green crystal activators
                else if (hit.collider.tag == "Corrosive" && cur.Target == bulletType.Corrosive)
                {
                    Destroy(obj);
                    Debug.Log("Bars Hit");
                    hit.collider.GetComponent<DissolveBars>().DissolveMe();
                } //corrosive bars
                else if (tag == "Shock" && cur.Target == bulletType.Shock)
                {
                    Destroy(obj);
                    Debug.Log("Platform Hit");
                    hit.collider.GetComponentInParent<ShockPlatform>().Activate();
                } //shock platform
                else if (hit.collider.name.Contains("Tutorial_crystal"))
                {
                    Debug.Log("Hitting Tutorial Crystal");
                    hit.transform.GetComponent<tutorial>().display();
                } //tutorial crystals
                else if (cur.Target == bulletType.Heal) //case for when a heal shot misses
                {
                    Debug.Log("Deprecated Edge case for when heal shot misses");
                    Destroy(obj);
                } //edge case
            }
        }
        StartCoroutine(myGAS.Chamber.GetComponent<ChamberScript>().RotateCCW());
        Debug.Log("Got Past Chamber Revolution 'Animation'");
        magazine.Enqueue(new bullet(bulletType.NULL)); //place an empty bullet in the magazine
        Debug.Log("Got Past Blank Bullet Added");
        revolve(0); //revolve entire magazine after firing
        Debug.Log("Got Past Chamber Revolution in Script, IE DONE");
        //lastShot -= gunValues.fireRate;
    }

    public int bulletHit(bool sameColor, bool isCrystal) //function for determining the damage a bullet should deal, takes whether or not it matched, and the distance of the shot
    {
        int damage = baseDamage; /*gunValues.damageCurve.Evaluate(distance/gunValues.weaponRange);
        if(damage > 1)
        {
            damage = 1;
        }*/
        if (sameColor && usesCombo)
        {
            numPerfects++;
            if (numPerfects > 6)
            {
                numPerfects = 6;
            }
            if (!isCrystal)
            {
                StartCoroutine(ComboSound(.2f, numPerfects));
            }
        }
        //Debug.Log(numPerfects);
        if (numPerfects > 0 && usesCombo)
        {
            damage = baseDamage + numPerfects;
        }

        if(!usesCombo)
        {
            if (sameColor)
            {
                damage = baseDamage + 1;
            }
            else
            {
                damage = baseDamage;
            }
        }

        //Debug.Log(damage + " Damage");
        return damage;
    }

    public void ResetCombo()
    {
        if (usesCombo)
        {
            numPerfects = 0;
            //myCrystal.GetComponentInChildren<SpriteRenderer>().enabled = false;
            //myCrystal.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Runes/rune_numbers_1_inverted") as Sprite;
            foreach (Image i in myBulletBackgrounds)
            {
                i.enabled = false;
            }
        }
    }

    private bool isEmpty()
    {
        foreach(bullet b in magazine)
        {
            if(b.Target != bulletType.NULL)
            {
                return false;
            }
        }
        return true;
    }

    private bullet randBul()
    {
        //Debug.Log(colorSet);
        if (!colorSet)
        {
            float roll = Random.value;
            //Debug.Log(roll);
            float chance = redChance;
            if (roll <= chance && RedActive)
            {
                return new bullet(bulletType.Blast);
            }
            chance += greenChance;
            if (roll <= chance && GreenActive)
            {
                return new bullet(bulletType.Heal);
            }
            chance += blueChance;
            if (roll <= chance && BlueActive)
            {
                return new bullet(bulletType.Freeze);
            }
            chance += yellowChance;
            if (roll <= chance && YellowActive)
            {
                return new bullet(bulletType.Corrosive);
            }
            chance += cyanChance;
            if (roll <= chance && CyanActive)
            {
                return new bullet(bulletType.Shock);
            }
            chance += purpleChance;
            if (roll <= chance && PurpleActive)
            {
                return new bullet(bulletType.Void);
            }
            return new bullet(bulletType.Plain);
        }
        else
        {
            return new bullet(setBulletType);
        }
    }

    private bullet LoadFromBandolier(bool FirstLoad)
    {
        Debug.Log(Loadouts.LO.gameObject);

        curBandolier = Loadouts.LO.LoadoutChoice(activeBandolier);

        BandolierProgress++; //starts at -1, gets incremented as we go

        if (FirstLoad)
        {
            Debug.Log("Loading " + ((bulletType)curBandolier[BandolierProgress]).ToString());
            return new bullet((bulletType)curBandolier[BandolierProgress]); //return the bullets to be loaded
        }
        else
        {
            Debug.Log("Loading " + ((bulletType)curBandolier[5 - BandolierProgress]).ToString());
            return new bullet((bulletType)curBandolier[5 - BandolierProgress]); //alternative loading method that loads bullets in reverse
        }

    }

    public bool SetChances(float red, float green, float blue, float yellow)
    {
        if((red+green+blue+yellow != 1.0f) || red < 0.0f || green < 0.0f || blue < 0.0f || yellow < 0.0f) //if they don't add up to 1 or any of them are negative
        {
            return false;
        }
        else
        {
            redChance = red;
            greenChance = green;
            blueChance = blue;
            yellowChance = yellow;
            return true;
        }
            
    }

    public void SetFlairColor(Color newColor)
    {
        //if (newColor != magazineImage.color)
        {
            newColor.a = .8f;
            //myCrystal.GetComponent<Renderer>().material.SetColor("_EmissiveColor", newColor/10f);

            foreach(Material m in Resources.LoadAll<Material>("GunMats/"))
            {
                m.SetColor("_EmissiveColor", newColor / 10f);
            }

            baseCrosshairs.color = newColor;

            sightscolor1.GetComponent<Renderer>().material.SetColor("_BaseColor", newColor);
            sightscolor2.GetComponent<Renderer>().material.SetColor("_BaseColor", newColor);
            //GetComponentInParent<Healthbar>().UpdateColors(newColor);
        }
    }

    public void setCrystalTracker(int numCrystals) //for the rune that tracks number of crystals hit
    {
        if (numCrystals > 0)
        {
            myCrystal.GetComponentInChildren<SpriteRenderer>().enabled = true;
            myCrystal.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Runes/rune_numbers_" + (numCrystals) + "_inverted") as Sprite;
            StartCoroutine(ComboSound(.2f, numCrystals));
        }
        else
        {
            myCrystal.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    public void SetIndicators()
    {
        float count = 0;
        count += (RedActive) ? 1 : 0;
        count += (BlueActive) ? 1 : 0;
        count += (GreenActive) ? 1 : 0;
        count += (YellowActive) ? 1 : 0;
        count += (CyanActive) ? 1 : 0;
        count += (PurpleActive) ? 1 : 0;
        count  = 1/count;
        
        //RedIndicator.gameObject.SetActive(RedActive);
        //RedIndicator.color = (RedActive) ?  myPartColors[0] : myPartColors[6];
        redChance = (RedActive) ? count : 0;
        //Debug.Log(RedActive);
        //YellowIndicator.gameObject.SetActive(YellowActive);
        //YellowIndicator.color = (YellowActive) ? myPartColors[1] : myPartColors[6];
        yellowChance = (YellowActive) ? count : 0;
        //Debug.Log(YellowActive);
        //BlueIndicator.gameObject.SetActive(BlueActive);
        BlueIndicator.color = (BlueActive) ? myPartColors[2] : myPartColors[6];
        blueChance = (BlueActive) ? count : 0;
        //Debug.Log(BlueActive);
        //GreenIndicator.gameObject.SetActive(GreenActive);
        //GreenIndicator.color = (GreenActive) ? myPartColors[3] : myPartColors[6];
        greenChance = (GreenActive) ? count : 0;
        //Debug.Log(GreenActive);
        //CyanIndicator.gameObject.SetActive(CyanActive);
        //CyanIndicator.color = (CyanActive) ? myPartColors[4] : myPartColors[6];
        cyanChance = (CyanActive) ? count : 0;

        //PurpleIndicator.gameObject.SetActive(PurpleActive);
        //PurpleIndicator.color = (PurpleActive) ? myPartColors[5] : myPartColors[6];
        purpleChance = (PurpleActive) ? count : 0;

    }

    public void SetIndicatorsColor()
    {
        //RedIndicator.gameObject.SetActive((setBulletType == bulletType.Blast));
        //RedIndicator.color = ((setBulletType == bulletType.Blast)) ? myPartColors[0] : myPartColors[6];
        //Debug.Log(RedActive);
        //YellowIndicator.gameObject.SetActive((setBulletType == bulletType.Corrosive));
        //YellowIndicator.color = ((setBulletType == bulletType.Corrosive)) ? myPartColors[1] : myPartColors[6];
        //Debug.Log(YellowActive);
        //BlueIndicator.gameObject.SetActive((setBulletType== bulletType.Freeze));
        //BlueIndicator.color = ((setBulletType == bulletType.Freeze)) ? myPartColors[2] : myPartColors[6];
        //Debug.Log(BlueActive);
        //GreenIndicator.gameObject.SetActive((setBulletType == bulletType.Heal));
        //GreenIndicator.color = ((setBulletType == bulletType.Heal)) ? myPartColors[3] : myPartColors[6];
        //Debug.Log(GreenActive);
        //GreenIndicator.gameObject.SetActive((setBulletType == bulletType.Shock));
        //CyanIndicator.color = ((setBulletType == bulletType.Shock)) ? myPartColors[4] : myPartColors[6];

        //GreenIndicator.gameObject.SetActive((setBulletType == bulletType.Void));
        //PurpleIndicator.color = ((setBulletType == bulletType.Void)) ? myPartColors[5] : myPartColors[7];

    }

    public Color GetColor(int partColor)
    {
        return myPartColors[partColor];
    }

    public bool isColorUnlocked(int whichColor)
    {
        switch ((bulletType)whichColor)
        {
            case bulletType.Blast:
                return RedActive;
            case bulletType.Freeze:
                return BlueActive;
            case bulletType.Corrosive:
                return YellowActive;
            case bulletType.Heal:
                return GreenActive;
            case bulletType.Shock:
                return CyanActive;
            case bulletType.Void:
                return PurpleActive;

            default:
                return false;
        }
    }

    private void UpdateSpellCircleUI()
    {
        TotalUnlocked = ((!Loadouts.LO.LO1Unlocked ? 0 : 1) + (!Loadouts.LO.LO2Unlocked ? 0 : 1) + (!Loadouts.LO.LO3Unlocked ? 0 : 1) + (!Loadouts.LO.LO4Unlocked ? 0 : 1) + (!Loadouts.LO.LO5Unlocked ? 0 : 1) + (!Loadouts.LO.LO6Unlocked ? 0 : 1));


        if (TotalUnlocked == 0) //none are unlocked yet, set all to false
        {
            CircleParent.SetActive(false);
            BottomCircle.SetActive(false);
            LeftCircle.SetActive(false);
            ActiveCircle.SetActive(false);
            //Debug.Log("Deactivate Bandolier HuD UI");
        }else
        if (TotalUnlocked == 1) //one is unlocked, set active true
        {
            CircleParent.SetActive(true);
            BottomCircle.SetActive(false);
            LeftCircle.SetActive(false);
            ActiveCircle.SetActive(true);
        }else
        if (TotalUnlocked == 2) //two are unlocked, set active and left true
        {
            CircleParent.SetActive(true);
            BottomCircle.SetActive(false);
            ActiveCircle.SetActive(true);
            LeftCircle.SetActive(true);
        }
        else //three or more are unlocked, activate the whole thing
        {
            CircleParent.SetActive(true);
            BottomCircle.SetActive(true);
            ActiveCircle.SetActive(true);
            LeftCircle.SetActive(true);
        }

        if (Loadouts.LO.LO1Unlocked)
        {
            SpeedReloadParent.GetComponentInChildren<Text>().text = numSpeedReloaders.ToString();

            int nextActiveBandolier = activeBandolier + 1;
            int prevActiveBandolier = activeBandolier - 1;

            if (activeBandolier == 0)
            {
                prevActiveBandolier = TotalUnlocked - 1;
            }
            if (activeBandolier == TotalUnlocked - 1)
            {
                nextActiveBandolier = 0;
            }

            curBandolier = Loadouts.LO.LoadoutChoice(activeBandolier);
            prevBandolier = Loadouts.LO.LoadoutChoice(prevActiveBandolier);
            nextBandolier = Loadouts.LO.LoadoutChoice(nextActiveBandolier);

            //Debug.Log("Prev: " + prevActiveBandolier + " Cur: " + activeBandolier + " Next: " + nextActiveBandolier);

            for (int i = 0; i < 6; i++) //updates all circles
            {
                var rune = ActiveCircle.transform.GetChild(i + 1).GetChild(1).GetComponent<Image>();
                rune.color = GetColor(curBandolier[i]);
                rune.sprite = BandolierMenu.BM.Bullets[curBandolier[i]].Symbol;

                rune = LeftCircle.transform.GetChild(i + 1).GetChild(1).GetComponent<Image>();
                rune.color = GetColor(nextBandolier[i]);
                rune.sprite = BandolierMenu.BM.Bullets[nextBandolier[i]].Symbol;

                rune = BottomCircle.transform.GetChild(i + 1).GetChild(1).GetComponent<Image>();
                rune.color = GetColor(prevBandolier[i]);
                rune.sprite = BandolierMenu.BM.Bullets[prevBandolier[i]].Symbol;
            }
        }

    }

    public void UpdateStats(UpgradeSO upgrade, int currLevel)
    {
        if (upgrade == DeadeyeUpgrade)
        {
            //Debug.Log("Old DeadeyeTotal: " + DeadeyeTotal);
            TotalDeadeye = DeadeyeUpgrade.statList[currLevel];
            //Debug.Log("New DeadeyeTotal: " + DeadeyeTotal);
            DeadeyeTotal = TotalDeadeye;
            deadeyeBar.UpdateBar(1);
            
        }
        else if (upgrade == BackupsUpgrade)
        {
            //Debug.Log("Old maxSpeedReloaders: " + maxSpeedReloaders);
            maxSpeedReloaders = BackupsUpgrade.statList[currLevel];
            //Debug.Log("New maxSpeedReloaders: " + maxSpeedReloaders);
            numSpeedReloaders = maxSpeedReloaders;
        }
        else if (upgrade == DamageUpgrade)
        {
            //Debug.Log("Old baseDamage: " + baseDamage);
            baseDamage = DamageUpgrade.statList[currLevel];
            //Debug.Log("New baseDamage: " + baseDamage);
        }
    }
    
}



