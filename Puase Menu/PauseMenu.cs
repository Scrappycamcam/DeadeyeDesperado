using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;



public class PauseMenu : MonoBehaviour
{
    public BandolierMenu BM;

    [SerializeField]
    public GameObject pauseMenuUI;



    [SerializeField]
    public GameObject bandolierMenu;

    [SerializeField]
    public GameObject upgradeMenu;

    [SerializeField]
    public static bool LoadoutMenuon = false;

    [SerializeField]
    public static bool bandolierMenuOn = false;

    [SerializeField]
    public static bool upgradeMenuOn = false;

    [SerializeField]
    public bool GameIsPaused = false;

    [SerializeField]
    public GameObject OptionsPauseMenu;

    private GameObject NotPauseCanvas;

    public static bool Objectison = false;

    public static bool created = false;

    public GameObject pauseGameobject;

    public bool gamepause =false ;




    [Header("Description Screen")]
    public GameObject red, blue, green, yellow;

    public string LevelName;


   

    private void Awake()
    {
        
        /*if (!created && SceneManager.GetActiveScene().name != "MainMenu") 
        {
            DontDestroyOnLoad(pauseGameobject);
            created = true;

        }
        */
  
       LevelName = SceneManager.GetActiveScene().name;

        NotPauseCanvas = GameObject.Find("NotPauseMenu");


    }
    private void Start()
    {

        if (GameIsPaused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenuUI.SetActive(false);
            bandolierMenu.SetActive(false);
            upgradeMenu.SetActive(false);

            Cursor.visible = (true);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = .02f * Time.timeScale;
            Time.maximumParticleDeltaTime = .03f * Time.timeScale;
        }
        Pause();
        Resume();
    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(pauseGameobject);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gamepause == false)
        {
            if (GameIsPaused)
            {
                GameIsPaused = false;
                OptionsBack();
                Objectison = false;
                Resume();

            }
			else if(Time.timeScale == 0f)
			{
				specialShot_popUp s = Object.FindObjectOfType<specialShot_popUp>();
                if (s)
                {
                    s.resume();
                }
				GameIsPaused = true;
				Pause();
			}
            else
            {
                GameIsPaused = true;
                Pause();


            }
            Time.fixedDeltaTime = .2f * Time.timeScale;
            Time.maximumParticleDeltaTime = .03f * Time.timeScale;
        }
        if (LoadoutMenuon && !Objectison)
        {
            Loadout();

        }
    }
    public void OptionsBack()
    {
        pauseMenuUI.SetActive(true);
        if (OptionsPauseMenu)
        {
            OptionsPauseMenu.SetActive(false);
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        bandolierMenu.SetActive(false);
        upgradeMenu.SetActive(false);


        if (NotPauseCanvas)
        {
            NotPauseCanvas.SetActive(true);
        }


        Player.Instance.GetComponent<PlayerMovement>().enabled = true;
        Player.Instance.GetComponentInChildren<Gun>().deadeyeBar.gameObject.SetActive(true);
        Player.Instance.GetComponentInChildren<CameraMovement>().enabled = true;
        //GameObject.Find("BlastIndicator").transform.parent.GetComponentInParent<Canvas>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);
        GameIsPaused = false;
        LoadoutMenuon = false;
        bandolierMenuOn = false;
        Time.timeScale = 1f;


    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        bandolierMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        bandolierMenuOn = false;
        upgradeMenuOn = false;

        if (NotPauseCanvas)
        {
            NotPauseCanvas.SetActive(false);
        }



        Player.Instance.GetComponent<PlayerMovement>().enabled = false;
        Player.Instance.GetComponentInChildren<Gun>().deadeyeBar.gameObject.SetActive(false);
        Player.Instance.GetComponentInChildren<CameraMovement>().enabled = false;
        //GameObject.Find("BlastIndicator").transform.parent.GetComponentInParent<Canvas>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        GameIsPaused = true;
        Cursor.visible = (true);
        Time.timeScale = 0f;

    }

    public void Options()
    {
        Cursor.lockState = CursorLockMode.Confined;

        LoadoutMenuon = true;
        pauseMenuUI.SetActive(false);
        OptionsPauseMenu.SetActive(true);
        bandolierMenu.SetActive(false);
        bandolierMenuOn = false;

        var DeletGameobject = GameObject.FindGameObjectWithTag("LoadoutDescription");

    }
    public void Save()
    {
        SaveGameEvents.OnSaveInitiated();
    }

    public void LoadBack()
    {
        OptionsPauseMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        bandolierMenu.SetActive(false);
        bandolierMenuOn = false;
        upgradeMenu.SetActive(false);
        upgradeMenuOn = false;
    }

    public void Loadout()
    {
        if (LoadoutMenuon == true)
        {
            //Cursor.lockState = CursorLockMode.Confined;

         LoadoutMenuon = false;
         Objectison = true;


        }
        else if(LoadoutMenuon == false)
        {
            LoadoutMenuon = true;

        }
    }

    public void ToggleBandolierMenu()
    {
        if(!bandolierMenuOn && Loadouts.LO.LO1Unlocked)
        {
            bandolierMenuOn = true;
            LoadoutMenuon = false;
            upgradeMenuOn = false;

            bandolierMenu.SetActive(true);
            pauseMenuUI.SetActive(false);
            upgradeMenu.SetActive(false);

            BM.DisplayInfo();
        }
        else
        {
            BM.SelectChosenLO();
        }
    }

    public void ToggleUpgradeMenu()
    {
        if (!upgradeMenuOn)
        {
            upgradeMenuOn = true;
            bandolierMenuOn = false;
            LoadoutMenuon = false;

            upgradeMenu.SetActive(true);
            bandolierMenu.SetActive(false);
            pauseMenuUI.SetActive(false);
        }
    }
    
    public void QuitGame()
    {
        Objectison = false;
        SaveLoad.Save(SceneManager.GetActiveScene().name, "NameofLevel");
        Resume();
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void BlastDescription()
    {
        
            var DeletGameobject = GameObject.FindGameObjectWithTag("LoadoutDescription");
            Destroy(DeletGameobject);

            red.gameObject.SetActive(true);
           // Blastinfoon = true;


    }
    public void FrostDescription()
    {
         var DeletGameobject = GameObject.FindGameObjectWithTag("LoadoutDescription");
            Destroy(DeletGameobject);

        blue.gameObject.SetActive(true);


        //Frostinfoon = true;



    }
    public void HealDescription()
    {
        
            var DeletGameobject = GameObject.FindGameObjectWithTag("LoadoutDescription");
            Destroy(DeletGameobject);

        green.gameObject.SetActive(true);
        //Healinfoon = true;



    }
    public void FireDescription()
    {
       
            var DeletGameobject = GameObject.FindGameObjectWithTag("LoadoutDescription");
            Destroy(DeletGameobject);

        yellow.gameObject.SetActive(true);
        //Fireinfoon = true;



    }

}

