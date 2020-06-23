using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*put this and crystal_controller on the object 
* where the Player gets the new colored shots.
*/
public class specialShot_popUp : MonoBehaviour
{
	public enum color {red, blue, green, orange, cyan, purple, upgrade};
	public GameObject red, blue, green, yellow, bandolier, cyan, purple, upgrade;
	private GameObject player;
	[HideInInspector]public color Color;

    public void display()
    {
        Debug.Log("display");
        player = Player.Instance.gameObject;
        switch (Color)
        {
            case color.red:
                player.GetComponentInChildren<Gun>().unlockColor('r');
                Instantiate(red);
                break;
            case color.blue:
                player.GetComponentInChildren<Gun>().unlockColor('b');
                Instantiate(blue);
                break;
            case color.green:
                player.GetComponentInChildren<Gun>().unlockColor('g');
                Instantiate(green);
                break;
            case color.orange:
                player.GetComponentInChildren<Gun>().unlockColor('o');
                Instantiate(yellow);
                break;
            case color.cyan:
                player.GetComponentInChildren<Gun>().unlockColor('c');
                Instantiate(cyan);
                break;
            case color.purple:
                player.GetComponentInChildren<Gun>().unlockColor('p');
                Instantiate(purple);
                break;
            case color.upgrade:
                Instantiate(upgrade);
                break;
            default:
                Debug.Log("No Color");
                break;
        }
        Player.Instance.GetComponentInChildren<CameraMovement>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }


    private bool bandolierShown = false;

	public void resume()
    {
        Debug.Log("ResumeBase");
        if (Gun.instance.TotalUnlocked == 0 && !bandolierShown && !gameObject.name.Contains("Bandolier") && !gameObject.name.Contains("Upgrade"))
        {
            Debug.Log("Display Bandolier");
            Instantiate(bandolier);
            bandolierShown = true;
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Resume");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            Player.Instance.GetComponentInChildren<CameraMovement>().enabled = true;
            Destroy(this.gameObject);
        }
    }
}
