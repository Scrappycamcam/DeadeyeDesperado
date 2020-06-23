using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunAnimationScript : MonoBehaviour
{
    #region Variables
    public Animator GunAnimator;
    public GameObject Chamber;
    public float FireTime;
    public float ReloadTime;
    public GameObject[] Bullets =  new GameObject[7];
    public int FirePosition;
    public int ReloadPosition;
    public float LookDamp;

    public Material[] bulletMats;
    #endregion

    void Start()
    {
        Bullets[6].gameObject.SetActive(false);
        GunAnimator = this.GetComponent<Animator>();
        GunAnimator.SetBool("Is Idle", true);
    }

    public void PlayFireAnimation()
    {
       StartCoroutine(Firing());
    }
    
    IEnumerator Firing()
    {
        GunAnimator.SetBool("Is Idle", false);
        GunAnimator.SetBool("Is Running", false);
        GunAnimator.SetTrigger("Is Firing");
        TurnOffFireBullet();
        yield return new WaitForSeconds(FireTime);
        GunAnimator.SetBool("Is Idle", true);
    }

    public void PlayReloadAnimation()
    {
        GunAnimator.SetBool("Is Idle", false);
        GunAnimator.SetBool("Is Running", false);
        GunAnimator.SetTrigger("Is Reloading");
        Bullets[6].SetActive(true);
    }

    public void LoadBullet()
    {
        if (!Gun.instance.speedReloading)
        {
            Debug.Log("Bullet Loaded");
            Player.Instance.GetComponentInChildren<Gun>().LoadBullet();
            Bullets[6].SetActive(false);

            TurnOnReloadBullet();
        }
    }

    public void ReloadFinished()
    {
        Debug.Log("Reload Animation Finished");
        GunAnimator.SetBool("Is Idle", true);
    }

    public void FireCraziness(float additional)
    {
        GunAnimator.SetFloat( "FireCraziness", .4f + additional);
    }

    public void gunCheck()
    {
        StartCoroutine(controlDelay(3.5f));
        gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);
        GunAnimator.SetTrigger("GunCheck");
    }

    private IEnumerator controlDelay(float delay)
    {
        GetComponentInParent<Gun>().takingInput = false;
        yield return new WaitForSeconds(delay);
        GetComponentInParent<Gun>().takingInput = true;
    }

    public void SetBulletColor(int BulletNumber, int partIter)
    {
        CheckReloadPosition();
        if (BulletNumber < 6)
        {
            BulletNumber += ReloadPosition;
            if (BulletNumber >= 6)
            {
                BulletNumber -= 6;
            }
            Bullets[BulletNumber].gameObject.SetActive(true);
        }
        Bullets[BulletNumber].gameObject.GetComponent<Renderer>().material = bulletMats[partIter];
    }


    public void TurnOffFireBullet()
    {
        CheckFirePosition();
        Bullets[FirePosition - 1].gameObject.SetActive(false);
    }

    public void TurnOnReloadBullet()
    {
        CheckReloadPosition();
        Bullets[ReloadPosition - 1].gameObject.SetActive(true);
    }

    public void ResetMAg()
    {
        foreach(GameObject g in Bullets)
        {
            g.SetActive(true);
        }
    }

    void CheckFirePosition()
    {
        FirePosition = Chamber.GetComponent<ChamberScript>().FirePosition;
    }

    void CheckReloadPosition()
    {
        ReloadPosition = Chamber.GetComponent<ChamberScript>().FirePosition;

        if (ReloadPosition > 1)
        {
            ReloadPosition -= 1;
        }
        else ReloadPosition = 6;

    }

    public void SetHorz(float xVel)
    {
        var temp = xVel - Mathf.Max(GunAnimator.GetFloat("HorizontalAim")/2, .1f);

        GunAnimator.SetFloat("HorizontalAim", Mathf.Lerp(GunAnimator.GetFloat("HorizontalAim"), temp, Time.deltaTime));
    }

    public void SetVert(float yVel)
    {
        var temp = yVel - Mathf.Max(GunAnimator.GetFloat("VerticalAim") / 4, .1f);

        //GunAnimator.SetFloat("VerticalAim", temp);
        GunAnimator.SetFloat("VerticalAim", Mathf.Lerp(GunAnimator.GetFloat("VerticalAim"), temp, Time.deltaTime));
    }

    
    public void EnterADS(float ADSWeight)
    {
        //ADSWeight = 1;
        this.gameObject.GetComponent<Animator>().SetLayerWeight(1, ADSWeight);
        this.gameObject.GetComponent<Animator>().SetLayerWeight(0, 1 - ADSWeight);
        // yield return new WaitForSeconds(0.1f);
    }

    

    public void Die()
    {
        this.gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);
        GetComponentInParent<Gun>().ADSWeight = 0;
        GunAnimator.SetTrigger("Die");
    }


}

/* Notes and Old Stuff
 
      
    IEnumerator Firing()
    {
        //if (GunAnimator.GetBool("Is Idle"))
        {
            CheckFirePosition();
            CheckIfCurrentBulletIsActive();
            if (CurrentBulletActive)
            {
                GunAnimator.SetBool("Is Idle", false);
                GunAnimator.SetTrigger("Is Firing");
                TurnOffCurrentBullet();
                StartCoroutine(Chamber.GetComponent<ChamberScript>().RotateCCW());

                yield return new WaitForSeconds(FireTime);

                GunAnimator.SetBool("Is Idle", true);
            }
            else
            {
                GunAnimator.SetBool("Is Idle", false);
                StartCoroutine(Chamber.GetComponent<ChamberScript>().RotateCCW());
                GunAnimator.SetBool("Is Idle", true);
            }
        }
        //else
        {
           // GunAnimator.SetTrigger("Is Firing");
        }
    }
    
    
    IEnumerator StartReloading()
    {
        GunAnimator.SetBool("Done Reloading", false);
        GunAnimator.SetBool("Is Idle", false);
        GunAnimator.SetBool("Is Reloading", true);
        looper = 1;
        
        CheckIfAnyBulletIsInactive();
        while (AnyBulletInactive)
        {
            looper++;
            Debug.Log(looper);
            if (looper > 500)
            {
                break;
            }
            CheckIfReloadBulletIsActive();
            if (!(ReloadBulletActive))
            {
                GunAnimator.SetBool("Is Idle", false);
                GunAnimator.SetBool("Is Reloading", true);
                yield return new WaitForSeconds(0.001f);
                Bullets[6].SetActive(true);
                yield return new WaitForSeconds(ReloadTime*3/4);
                Bullets[6].SetActive(false);
                TurnOnReloadBullet();
                StartCoroutine(Chamber.GetComponent<ChamberScript>().RotateCW());
                yield return new WaitForSeconds(ReloadTime/4);


                GunAnimator.SetBool("Is Idle", true);
                GunAnimator.SetBool("Is Reloading", false);
            }
            else if(AnyBulletInactive)
            {
                yield return new WaitForSeconds(.05f);
                StartCoroutine(Chamber.GetComponent<ChamberScript>().RotateCW());
            }
            CheckIfAnyBulletIsInactive();
            if (!AnyBulletInactive)
            {
                Debug.Log("Break");
                break;
            }
        }
        GunAnimator.SetBool("Done Reloading", true);
        GunAnimator.SetBool("Is Idle", true);
        GunAnimator.SetBool("Is Reloading", false);
    }
    

        
    void CheckIfCurrentBulletIsActive()
    {        
        CurrentBulletActive = false;
        if (Bullets[FirePosition - 1].gameObject.activeInHierarchy)
        {
            CurrentBulletActive = true;
        }
    }

    void CheckIfReloadBulletIsActive()
    {
        CheckReloadPosition();
        ReloadBulletActive = false;
        if (Bullets[ReloadPosition - 1].gameObject.activeInHierarchy)
        {
            ReloadBulletActive = true;
        }
    }

    void CheckIfAnyBulletIsInactive()
    {
        AnyBulletInactive = false;
        for(int i = 0; i < 6; i++)
        {
            if (!(Bullets[i].gameObject.activeInHierarchy))
            {
                AnyBulletInactive = true;
            }
        }
    }

    

    void SetAllBulletColors()
    {
        List<Image> colors = Player.Instance.GetComponentInChildren<Gun>().myBulletImages;
        for (int i = 0; i < 6; i++)
        {
            var spot = FirePosition - 1 - i;
            if(spot < 0)
            {
                spot += 5;
            }
            Bullets[i].GetComponent<Renderer>().material.SetColor("_BaseColor", colors[5-i].color);
        }
    }

   


    */


  
 
