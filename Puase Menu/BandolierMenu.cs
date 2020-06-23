using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BandolierMenu : MonoBehaviour
{
    public static BandolierMenu BM;

    public Bandolier bandolier;

    public PauseMenu pauseMenu;
    private EventSystem es;


    private bool menuLoaded;

    public int[] selectedLO;
    /*public int[] LO1;
    public int[] LO2;
    public int[] LO3;
    public int[] LO4;
    public int[] LO5;
    public int[] LO6;*/

    private int currentLO;

    public Transform selectedLODisplay;
    public Transform LO1Display;
    public Transform LO2Display;
    public Transform LO3Display;
    public Transform LO4Display;
    public Transform LO5Display;
    public Transform LO6Display;

    [SerializeField]
    private Transform chosenLO;

    public Image[] selectedLOImage;
    public Image[] LO1;
    public Image[] LO2;
    public Image[] LO3;
    public Image[] LO4;
    public Image[] LO5;
    public Image[] LO6;

    [Header("Bullet Information")]
    public List<BulletInfo> Bullets = new List<BulletInfo>();

    [Header("Bullet Info Display")]
    public Text bulletName;
    public Image bulletSymbol;
    public Text bulletDescription;
    public Text bulletDamage;
    public Text bulletOnHit;
    public Text bulletCombo;
    public Image bulletBG;

    private void Awake()
    {
        if (BM == null)
        {
            BM = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bandolier = Bandolier.B;
        //GetComponent<Button>().onClick.AddListener(delegate () { DisplayChoice(int.Parse(gameObject.transform.GetChild(6).GetComponent<Text>().text), gameObject.transform); });
        menuLoaded = false;
        es = EventSystem.current;
        UpdateWhichActive();

    }

    public void DisplayInfo()
    {
        UpdateWhichActive();
        bandolier.GetLO();
        if (!menuLoaded)
        {
            for (int i = 0; i < bandolier.maxNum; ++i)
            {
                selectedLOImage[i] = selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>();
                LO1[i] = LO1Display.Find("Bullet" + (i + 1)).GetComponent<Image>();
                LO2[i] = LO2Display.Find("Bullet" + (i + 1)).GetComponent<Image>();
                LO3[i] = LO3Display.Find("Bullet" + (i + 1)).GetComponent<Image>();
                LO4[i] = LO4Display.Find("Bullet" + (i + 1)).GetComponent<Image>();
                LO5[i] = LO5Display.Find("Bullet" + (i + 1)).GetComponent<Image>();
                LO6[i] = LO6Display.Find("Bullet" + (i + 1)).GetComponent<Image>();
            }

            //Sets up the Bandolier system based on what the player's current loadout is
            for (int i = 0; i < bandolier.maxNum; ++i)
            {
                TranslateInt(LO1Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[0][i]);
                Debug.Log("LO1 Bullet Int:" + bandolier.loadout[0][i]);
            }
            for (int i = 0; i < bandolier.maxNum; ++i)
            {
                TranslateInt(LO2Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[1][i]);
            }
            for (int i = 0; i < bandolier.maxNum; ++i)
            {
                TranslateInt(LO3Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[2][i]);
            }
            for (int i = 0; i < bandolier.maxNum; ++i)
            {
                TranslateInt(LO4Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[3][i]);
            }
            for (int i = 0; i < bandolier.maxNum; ++i)
            {
                TranslateInt(LO5Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[4][i]);
            }
            for (int i = 0; i < bandolier.maxNum; ++i)
            {
                TranslateInt(LO6Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[5][i]);
            }

            DisplayChoice((LO1Display.GetSiblingIndex() - 6), LO1Display);

            menuLoaded = !menuLoaded;
        }
    }

    private void UpdateWhichActive()
    {
        LO1Display.gameObject.SetActive(bandolier.GetComponent<Loadouts>().LO1Unlocked);
        LO2Display.gameObject.SetActive(bandolier.GetComponent<Loadouts>().LO2Unlocked);
        LO3Display.gameObject.SetActive(bandolier.GetComponent<Loadouts>().LO3Unlocked);
        LO4Display.gameObject.SetActive(bandolier.GetComponent<Loadouts>().LO4Unlocked);
        LO5Display.gameObject.SetActive(bandolier.GetComponent<Loadouts>().LO5Unlocked);
        LO6Display.gameObject.SetActive(bandolier.GetComponent<Loadouts>().LO6Unlocked);
    }

    public void UpdateDisplay(int loadout)
    {
        UpdateWhichActive();
        switch (loadout)
        {
            case 1:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    TranslateInt(LO1Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[(loadout-1)][i]);
                }
                break;
            case 2:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    TranslateInt(LO2Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[(loadout - 1)][i]);
                }
                break;
            case 3:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    TranslateInt(LO3Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[(loadout - 1)][i]);
                }
                break;
            case 4:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    TranslateInt(LO4Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[(loadout - 1)][i]);
                }
                break;
            case 5:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    TranslateInt(LO5Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[(loadout - 1)][i]);
                }
                break;
            case 6:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    TranslateInt(LO6Display.Find("Bullet" + (i + 1)).GetComponent<Image>(), bandolier.loadout[(loadout - 1)][i]);
                }
                break;
        }
        DisplayChoice(loadout, es.currentSelectedGameObject.transform);
    }

    public void DisplayChoice(int loadoutNum, Transform LODisplay)
    {
        currentLO = loadoutNum;

        chosenLO = LODisplay;
        Debug.Log(LODisplay);
        //Debug.Log("loadout Num is " + loadoutNum);
        SetSelectedGameObject();
        UpdateWhichActive();
        switch (loadoutNum)
        {
            case 1:
                for(int i = 0; i < bandolier.maxNum; ++i)
                {
                    selectedLO[i] = bandolier.loadout[0][i];
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color;
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite;
                }
                break;
            case 2:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    selectedLO[i] = bandolier.loadout[1][i];
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color;
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite;
                }
                break;
            case 3:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    selectedLO[i] = bandolier.loadout[2][i];
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color;
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite;
                }
                break;
            case 4:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    selectedLO[i] = bandolier.loadout[3][i];
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color;
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite;
                }
                break;
            case 5:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    selectedLO[i] = bandolier.loadout[4][i];
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color;
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite;
                }
                break;
            case 6:
                for (int i = 0; i < bandolier.maxNum; ++i)
                {
                    selectedLO[i] = bandolier.loadout[5][i];
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().color;
                    selectedLODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite = LODisplay.Find("Bullet" + (i + 1)).GetComponent<Image>().sprite;
                }
                break;
        }
        DisplayBulletInfo(selectedLO[0]);
    }

    public void CycleChoice(/*I need the current selected LO display, current bullet number*/)
    {
        bandolier.chamberNum = es.currentSelectedGameObject.transform.GetSiblingIndex() - 6;
        SetSelectedGameObject();
        if (es.currentSelectedGameObject.gameObject.transform == chosenLO)
        {
            bandolier.loadoutNum = (es.currentSelectedGameObject.transform.GetSiblingIndex() - 8);
            Debug.Log("loadoutNum is " + bandolier.loadoutNum);
            bandolier.CycleBulletChoice();
        }
    }

     //Translate the player's current loadout int in order to display properly
    void TranslateInt(Image loImage, int bulletInt)
    {
        Debug.Log("(BandolierMenu.TranslateInt) Current Bullet: " + Bullets[bulletInt].Name);
        Debug.Log("Bullet Int: " + bulletInt);
        loImage.sprite = Bullets[bulletInt].Symbol;
        loImage.color = Gun.instance.GetColor(bulletInt);
    }

    public void DisplayBulletInfo(int bulletInt)
    {
        bulletName.text = Bullets[bulletInt].Name;
        bulletSymbol.sprite = Bullets[bulletInt].Symbol;
        bulletDescription.text = bulletName.text + " Shot: " + Bullets[bulletInt].Description;
        bulletDamage.text = "Damage: " + Bullets[bulletInt].DamageAmount;
        bulletOnHit.text = "On Hit: " + Bullets[bulletInt].OnHitInfo;
        bulletCombo.text = "On Match: " + Bullets[bulletInt].ComboInfo;
        bulletBG.color = SetColor(bulletInt);
    }

    Color SetColor(int selection)
    {
        switch (selection)
        {
            case (int)bulletType.Blast:
                return Color.red;


            case (int)bulletType.Corrosive:
                return Color.yellow;


            case (int)bulletType.Freeze:
                return Color.blue;

            case (int)bulletType.Heal:
                return Color.green;

            case (int)bulletType.Shock:
                return Color.cyan;

            case (int)bulletType.Void:
                return Color.magenta;

            case (int)bulletType.Plain:
                return Color.white;

            default:
                return Color.black;
        }
    }

    private void SetSelectedGameObject()
    {
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(chosenLO.gameObject);
    }

    public void SelectChosenLO()
    {
        SetSelectedGameObject();
    }
}
