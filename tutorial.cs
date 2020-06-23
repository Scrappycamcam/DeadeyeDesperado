using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
	[SerializeField] private string Text;
	private bool hit;
    // Start is called before the first frame update
    void Awake()
	{
		if(Text.Length > 0)
		{
			gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = Text;
		}
		else
		{
			gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "You forgot to add the text!";
		}
	}
	public void display()
	{
		if(hit)
		{
			hide();
		}
		else
		{
			gameObject.transform.GetChild(1).gameObject.SetActive(true);
			GetComponent<AudioSource>().Play();
			hit = true;
		}
    }
	private void hide()
	{
		gameObject.transform.GetChild(1).gameObject.SetActive(false);
		GetComponent<AudioSource>().Play();
		hit = false;
	}
}
