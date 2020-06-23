using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Inventory playerInventory;
    public string Name;

    public float spinMultiplier = 20;
    private float spinSpeed;

    public float amplitude = 0.25f; //height of the bob from the starting y position
    public float frequency = 0.75f; //how many times the object moves up and down per in-game second
    private Vector3 posOffset = new Vector3(); //object's starting position
    private Vector3 tempPos = new Vector3(); //object's new position

    private string LevelName;
    private string Postion;
    private string ObjectName;
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private int armorAmount;

    public bool needsToBeSaved = false;
  
    // Start is called before the first frame update
    void Start()
    {
        if (needsToBeSaved)
        {
            LevelName = SceneManager.GetActiveScene().name;
            Postion = this.transform.position.x.ToString() + "," + this.transform.position.y.ToString() + "," + this.transform.position.z.ToString();
            ObjectName = this.name;
            if (SaveLoad.SaveExists(LevelName + Postion + ObjectName))
            {
                Destroy(this.gameObject);

            }
        }
        this.gameObject.tag = Name;
        Debug.Log(this.gameObject.tag);
        posOffset = transform.position;

        Player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
        Float();
    }

    void Spin()
    {
        spinSpeed = Time.deltaTime * spinMultiplier;
        transform.Rotate(0, spinSpeed, 0, Space.World);
    }

    void Float()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

    //When the player touches the collectable 
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (needsToBeSaved)
            {
                SaveLoad.Save<string>(ObjectName, LevelName + Postion + ObjectName);
            }
            Destroy(this.gameObject);
            if(this.gameObject.tag=="Ore")
            {
                playerInventory.AddOre();
                if (playerInventory.totalCollectablesAcquired() == 1)
                {
                    GetComponent<specialShot_popUp>().Color = specialShot_popUp.color.upgrade;
                    GetComponent<specialShot_popUp>().display();
                }
            }
            else if (this.gameObject.tag == "Crystal")
            {
                playerInventory.AddCrystal();
                if (playerInventory.totalCollectablesAcquired() == 1)
                {
                    GetComponent<specialShot_popUp>().Color = specialShot_popUp.color.upgrade;
                    GetComponent<specialShot_popUp>().display();
                }
            }
            else if (this.gameObject.tag == "Armor")
            {
                Player.GetComponent<Player>().GiveArmor(armorAmount);
            }
            else if (this.gameObject.tag == "Speed")
            {
                Gun.instance.GiveSpeedReloader();
            }
        }
    }
}
