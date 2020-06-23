using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenuScript : MonoBehaviour
{
    public int SaveSlots;

    public bool NewGameChange;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject LoadMenu;
    public GameObject Credits;
    public string SavedScene;
    public bool Newgame;
    public GameObject LoadText;
    public GameObject NewGameImg;
    private int SavedNumber;
    public string LastLevel;
    public GameObject LoadScreen;

    private bool isLoading = false;
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = (true);
        SavedNumber = PlayerPrefs.GetInt("CaseNumberOfSave", 1);
    }
    void Start()
    {
        OptionsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
       // Debug.Log(SavedScene);
        SaveSlots = PlayerPrefs.GetInt("SaveSlots", 1);
    }

    public void Continue()
    {
        if (File.Exists(Application.persistentDataPath + "/player1.save") || File.Exists(Application.persistentDataPath + "/player2.save") || File.Exists(Application.persistentDataPath + "/player3.save"))
        {

            //GetComponent<Button>().interactable = true;
            OptionsMenu.SetActive(false);
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
                    //Debug.LogError("Main Menu Save Not Working");
                    break;
            }

        }
        else
        {
            GetComponent<Button>().interactable = false;
        }

    }
   
  


    public void NewGame()
    {
        OptionsMenu.SetActive(false);
        
        LoadMenu.SetActive(true);
        LoadText.SetActive(false);
        NewGameImg.SetActive(true);
        MainMenu.SetActive(false);
        NewGameChange = true;


    }
    public void StartGame()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/player1.save"))
        {

        }
    }
    public void LoadGame()
    {
        NewGameChange = false;
        NewGameImg.SetActive(false);
        LoadText.SetActive(true);

        LoadMenu.SetActive(true);
        MainMenu.SetActive(false);

    }
    public void Options()
    {
        OptionsMenu.SetActive(true);
        MainMenu.SetActive(false);
    }
    public void QuitGame()
    {
       // Debug.Log("Quitting Game...");
        Application.Quit();
    }
    public void OptionsBack()
    {
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void LoadBack()
    {
        LoadMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void SaveOne()
    {
        //Debug.Log("StartSaveOne");
        SaveSlots = 1;
        LoadScreen.SetActive(true);
        if (NewGameChange)
        {
            if (SaveLoad.SaveExists("NameofLevel"))
            {
                SceneManager.LoadSceneAsync(1);
                //Debug.Log("save Delete try to load scene");
                SaveSlots = 1;
                PlayerPrefs.SetInt("SaveSlots", SaveSlots);
                SaveLoad.SeriosulyDelteAllSAVEFILES(1);
                Debug.Log("/SaveSlot 1 delted !!");
            }
            else
            {
                SceneManager.LoadSceneAsync(1);

              //  Debug.Log("try to load scene");
                SaveSlots = 1;
                PlayerPrefs.SetInt("SaveSlots", SaveSlots);
                SaveLoad.SeriosulyDelteAllSAVEFILES(1);

                Debug.Log("/SaveSlot 1 is not delted !!");

            }

        }
        else
        {
           
            LoadSaveOne();
            Debug.Log("LOADING SAVE 1!!!!!!!!!!!");
        }
        SavedNumber = 0;
        PlayerPrefs.SetInt("CaseNumberOfSave", SavedNumber);

    }
    public void SaveTwo()
    {
        SaveSlots = 2;
        LoadScreen.SetActive(true);
        if (NewGameChange)
        {
            if (SaveLoad.SaveExists("NameofLevel"))
            {
                SceneManager.LoadSceneAsync(1);
              //  Debug.Log("save Delete try to load scene");
                SaveSlots = 2;
                PlayerPrefs.SetInt("SaveSlots", SaveSlots);
                SaveLoad.SeriosulyDelteAllSAVEFILES(2);

            }
            else
            {
                SceneManager.LoadSceneAsync(1);

               // Debug.Log("try to load scene");
                SaveSlots = 2;
                PlayerPrefs.SetInt("SaveSlots", SaveSlots);
                SaveLoad.SeriosulyDelteAllSAVEFILES(2);

            }

        }

        else
        {
            /*
            PlayerData2 data = SaveSystemPlayer2.Loadplayer();
            SavedScene = data.level2;
            // Debug.Log("try to load scene");
            SaveSlots = 2;
            PlayerPrefs.SetInt("SaveSlots", SaveSlots);
            SceneManager.LoadSceneAsync(SavedScene);
            */
            LoadSaveTwo();
        }
        SavedNumber = 0;
        PlayerPrefs.SetInt("CaseNumberOfSave", SavedNumber);
    }
    public void SaveThree()
    {
        SaveSlots = 3;

        LoadScreen.SetActive(true);
        if (NewGameChange)
        {
            if (SaveLoad.SaveExists("NameofLevel"))
            {
                SceneManager.LoadSceneAsync(1);
               // Debug.Log("save Delete try to load scene");
                SaveSlots = 3;
                SaveLoad.SeriosulyDelteAllSAVEFILES(3);

                PlayerPrefs.SetInt("SaveSlots", SaveSlots);
            }
            else
            {
                SceneManager.LoadSceneAsync(1);

               // Debug.Log("try to load scene");
                SaveSlots = 3;
                PlayerPrefs.SetInt("SaveSlots", SaveSlots);
                SaveLoad.SeriosulyDelteAllSAVEFILES(3);

            }

        }

        else
        {
            /*
            PlayerData3 data = SaveSystemPlayer3.Loadplayer();
            SavedScene = data.level3;
            // Debug.Log("try to load scene");
            SaveSlots = 3;
            PlayerPrefs.SetInt("SaveSlots", SaveSlots);
            SceneManager.LoadSceneAsync(SavedScene);
            */
            LoadSaveThree();

        }
        SavedNumber = 0;
        PlayerPrefs.SetInt("CaseNumberOfSave", SavedNumber);
    }
    private void LoadSaveOne()
    {
        SaveSlots = 1;
        PlayerPrefs.SetInt("SaveSlots", SaveSlots);

        Load();
    }
    private void LoadSaveTwo()
    {
        SaveSlots = 2;
        PlayerPrefs.SetInt("SaveSlots", SaveSlots);

        Load();

    }
    private void LoadSaveThree()
    {
        SaveSlots = 3;
        PlayerPrefs.SetInt("SaveSlots", SaveSlots);

        Load();


    }
    public void Load()
    {
        SaveSlots = PlayerPrefs.GetInt("SaveSlots", 1);

        if (SaveLoad.SaveExists("NameofLevel"))
        {

            LastLevel = SaveLoad.Load<string>("NameofLevel");
            PlayerPrefs.SetInt("SaveSlots", SaveSlots);
            SavedNumber = 0;
            PlayerPrefs.SetInt("CaseNumberOfSave", SavedNumber);
            SceneManager.LoadScene(LastLevel);


        }
        else
        {
            Debug.Log("SAVELOAD IS NOT WORKING AND NOT EXISITNG");
        }
    }
    private bool CreditsOpen = false;

    public void DoCredits()
    {
        CreditsOpen = !CreditsOpen;
        Credits.SetActive(CreditsOpen);
    }


}

