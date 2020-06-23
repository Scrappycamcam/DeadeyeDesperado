using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Gun gun;
    public GameObject FirstPersonCharacter;

    public  AudioMixer mixer;
    public Slider VoulumeSlider;

    public Slider FieldOfView;
    public float StartofView;

    public Slider SensitivtyView;
    public static float SensitivityValue;

    public float MinSensitivityVaule;
    public float MaxSensitivityVaule;


    [SerializeField]
    private  float FOVValue;

    PlayerController pc;
    public float oldFov;

    public static float TranspercyValueSaved;
    public Slider TranspercySlider;

    public Toggle ReticleRectile;

    public Toggle ColorReticle;

    public static bool ToggleRun;

    public static bool ToggleCrouch;

    //public Slider AimSensitivty;
    private void Awake()
    {
        //ReticleRectile.isOn = false;

        FirstPersonCharacter = GameObject.Find("FirstPersonCharacter");
        gun = FirstPersonCharacter.GetComponent<Gun>();

        if (SaveLoad.SaveExists("ToolTip"))
        {
            gun.SetCrosshairShape = SaveLoad.Load<bool>("ToolTip");
            ReticleRectile.isOn = SaveLoad.Load<bool>("ToolTip");       
        }

        if (SaveLoad.SaveExists("ColorTip"))
        {
            gun.SetCrosshairColor = SaveLoad.Load<bool>("ColorTip");
            ColorReticle.isOn = SaveLoad.Load<bool>("ColorTip");
        }

        SensitivityValue = SensitivtyView.value;
        if (SaveLoad.SaveExists("FOVValue"))
        {
            FieldOfView.value = SaveLoad.Load<float>("FOVVaule");
        }
        if (SaveLoad.SaveExists("TranspercyValue"))
        {
            TranspercyValueSaved = SaveLoad.Load<float>("TranspercyValue");
            TranspercySlider.value = TranspercyValueSaved;
        }
    
    }
    private void Start()
    {
        TranspercyValueSaved = TranspercySlider.value;
         pc = GameObject.FindObjectOfType<PlayerController>();

        VoulumeSlider.value = PlayerPrefs.GetFloat("Volume", 10f);
        if (SaveLoad.SaveExists("FOVVaule"))
        {
            FOVValue = SaveLoad.Load<float>("FOVVaule");
        }

        //Debug.Log("FOVVALUE" + FOVValue);
        if (SaveLoad.SaveExists("SensivityValue"))
        {
            SensitivityValue = SaveLoad.Load<float>("SensivityValue");

        }

        if (SaveLoad.SaveExists("FOVVaule"))
        {
            Camera.main.fieldOfView = SaveLoad.Load<float>("FOVVaule");
        }
        FieldOfView.value = FOVValue;

        if (SaveLoad.SaveExists("FOVVaule"))
        {
            Gun.NormalFov = SaveLoad.Load<float>("FOVVaule");
        }
        SensitivtyView.value =SensitivityValue;

        FieldOfView.maxValue = 115f;
        FieldOfView.minValue = 75f;

        SensitivtyView.minValue = MinSensitivityVaule;
        SensitivtyView.maxValue = MaxSensitivityVaule;
    }

    public void ToogleRun(bool run)
    {
        if (run == true)
        {
            ToggleRun = true;
        }
        else if (run == false)
        {
            ToggleRun = false;

        }
        SaveLoad.Save(ToggleRun, "ToggleRun");
    }

    public void ToogleCrouch(bool Crouch)
    {
        if (Crouch == true)
        {
            ToggleCrouch = true;
        }
        else if (Crouch == false)
        {
            ToggleCrouch = false;

        }
        SaveLoad.Save(ToggleCrouch, "ToggleCrouch");
    }

    public void ToogleRetticleColor(bool ReticleColor)
    {
        if (ReticleColor == true)
        {
            gun.SetCrosshairColor = true;
        }
        else if (ReticleColor == false)
        {
            gun.SetCrosshairColor = false;

        }
        SaveLoad.Save(gun.SetCrosshairColor, "ColorTip");
    }

    public void ToogleRetticle(bool Reticle)
    {
        if (Reticle == true)
        {
            gun.SetCrosshairShape = true;
        }
        else if(Reticle == false)
        {
            gun.SetCrosshairShape = false;

        }
        SaveLoad.Save(gun.SetCrosshairShape, "ToolTip");
    }
    private void Update()
    {
       

        //Debug.Log(SensitivityValue);
        SensitivtyView.minValue = MinSensitivityVaule;
        SensitivtyView.maxValue = MaxSensitivityVaule;
        //VoulumeSlider.value = PlayerPrefs.GetFloat("Volume", 10f);
        //FieldOfView.value = PlayerPrefs.GetFloat("FieldOfView", 10f);
        //SensitivtyView.value = PlayerPrefs.GetFloat("Sensitivity", 10f);

        if (!pc.isRunning)
        {
            FOVValue = Gun.NormalFov;
            oldFov = FOVValue;
        }
        





    }
    public void SetTranspercy(float TranspercyValue)
    {

        TranspercySlider.value = TranspercyValue;
        TranspercyValueSaved = TranspercyValue;
        SaveLoad.Save(TranspercyValue, "TranspercyValue");

    }
    
public void SetLevel(float sliderValue)
    {
        // change the voulume of the game.
        //Debug.Log(PlayerPrefs.GetFloat("Volume", 10f));
        mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Volume", sliderValue);
        PlayerPrefs.Save();
    }
    public void SetFieldOfView(float FOVValue)
    {
        //change the feild of view 
        FieldOfView.value = FOVValue;
        Camera.main.fieldOfView = FieldOfView.value;
        Gun.NormalFov = FieldOfView.value;
        FOVValue = Gun.NormalFov;
        SaveLoad.Save(Gun.NormalFov, "FOVVaule");
       

    }
    public void SetSensitivity(float SensitvitySet)
    {
        //change the sensivity of the camera movement
        SensitivityValue = SensitivtyView.value;
        SensitvitySet = SensitivityValue;
        SaveLoad.Save(SensitivityValue, "SensivityValue");
        

    }


}
