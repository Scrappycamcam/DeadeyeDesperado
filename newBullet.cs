using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newBullet : MonoBehaviour
{
	[SerializeField]private GameObject popUp, bullet, chest, instantiatedBullet;
	Transform PTransform;
	public enum color {red, blue, green, orange, cyan, purple};
	[Tooltip("This is the color of the bullet you will need in the next puzzle room.")]
	public color Color;
    private Material Mat;
	Vector3 chestLoc;
	GameObject[] g;
    private Animator chestAnimator;
    public GameObject ps;

    private bool hit = false;

    private void Awake()
    {
        chestAnimator = chest.GetComponent<Animator>();
        if (SaveLoad.SaveExists(SceneManager.GetActiveScene().name + gameObject.name))
        {
            hit = SaveLoad.Load<bool>(SceneManager.GetActiveScene().name + gameObject.name);
            chestAnimator.speed = 10;
            chestAnimator.SetTrigger("OpenChest");
        }
        else
        {
            hit = false;
        }
    }

    void Start()
	{
        PTransform = Player.Instance.transform;
		//chest = GameObject.Find(.Contains
		chestLoc = chest.transform.position;
        popUp = Instantiate(popUp);
        switch (Color)
		{
			case color.red:
				popUp.GetComponent<specialShot_popUp>().Color = specialShot_popUp.color.red;
                Mat = Resources.Load("Bullets/BlastAmmo/BlastBulletTexture") as Material;
                break;
			case color.blue:
				popUp.GetComponent<specialShot_popUp>().Color = specialShot_popUp.color.blue;
                Mat = Resources.Load("Bullets/IceAmmo/IceBulletTexture") as Material;
                break;
			case color.green:
				popUp.GetComponent<specialShot_popUp>().Color = specialShot_popUp.color.green;
                Mat = Resources.Load("Bullets/HealAmmo/HealBulletTexture") as Material;
                break;
			case color.orange:
				popUp.GetComponent<specialShot_popUp>().Color = specialShot_popUp.color.orange;
                Mat = Resources.Load("Bullets/CorrosiveAmmo/CorrosiveBulletTexture") as Material;
                break;
            case color.cyan:
                popUp.GetComponent<specialShot_popUp>().Color = specialShot_popUp.color.cyan;
                Mat = Resources.Load("Bullets/ShockAmmo/ShockBulletTexture") as Material;
                break;
            case color.purple:
                popUp.GetComponent<specialShot_popUp>().Color = specialShot_popUp.color.purple;
                Mat = Resources.Load("Bullets/VoidAmmo/VoidBulletTexture") as Material;
                break;
        }
	}

	public void instantiateBullet()
	{
        if (!hit)
        {
            hit = true;
            StartCoroutine(chestOpen());
        }
	}

    private IEnumerator chestOpen()
    {
        chestAnimator.SetTrigger("OpenChest");
        //yield return new WaitForSeconds(.3f);
        ps.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        instantiatedBullet = Instantiate(bullet, chestLoc + Vector3.up*5, Quaternion.identity);
        if (Color != color.orange)
        {
            instantiatedBullet.transform.localScale *= 10;
        }
        instantiatedBullet.GetComponentInChildren<Renderer>().material = Mat;
        while (instantiatedBullet)
        {
            if (Time.timeScale > 0)
            {
                instantiatedBullet.transform.position = Vector3.Slerp(instantiatedBullet.transform.position, PTransform.position, .02f);
                instantiatedBullet.transform.Rotate(Vector3.up, Time.deltaTime * 180f);
                if (Vector3.Distance(instantiatedBullet.transform.position, PTransform.position) < 1)
                {
                    Debug.Log("Destroying Bullet");
                    SaveLoad.Save<bool>(hit, SceneManager.GetActiveScene().name + gameObject.name);
                    popUp.GetComponent<specialShot_popUp>().display();
                    PTransform.GetComponentInChildren<Gun>().myGAS.gunCheck();
                    Destroy(instantiatedBullet);
                }
            }
            yield return null;
        }
    }

	/*void Update()
	{
		//float f = (Time.time - startTime)/2f;
		if(Time.timeScale == 0)
		{
			return;
		}
		else if(instantiatedBullet != null)
		{
			instantiatedBullet.transform.position = Vector3.Slerp(instantiatedBullet.transform.position, PTransform.position, .01f);
			if(Vector3.Distance(instantiatedBullet.transform.position, PTransform.position) < 3)
			{
				Destroy(instantiatedBullet);
			}
		}
	}*/
}
