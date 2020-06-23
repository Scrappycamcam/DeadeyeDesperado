using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberScript : MonoBehaviour
{
    #region Variables
   // public Animator GunAnimator;
    public GameObject Gun;
    public float RotateTime;
    public int FirePosition; //this is the position of the bullet in Fire position
    public float GoalZ;
    public float Smooth = 10.0f;    
    public bool IsIdle;
    #endregion

    void Start()
    {        
        FirePosition = 1;
        IsIdle = true;
        //GunAnimator = Gun.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!(IsIdle))
        {            
            FindGoalZ();
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, GoalZ), Time.deltaTime * Smooth);                  
        }

        else
        {
            FindGoalZ();            
            Quaternion asdf = Quaternion.Euler(0, 0, GoalZ);
            this.gameObject.transform.localRotation = asdf;
        }

    }

    public IEnumerator RotateCW()
    {
        IsIdle = false;
        //Debug.Log("Clockwise Chamber");
        if (FirePosition > 1)
        {
            FirePosition = FirePosition - 1;
        }
        else FirePosition = 6;
        yield return new WaitForSeconds(RotateTime);
        IsIdle = true;
    }

    public IEnumerator RotateCCW()
    {
        IsIdle = false;
        //Debug.Log("C Clockwise Chamber");
        if (FirePosition < 6)
        {
            FirePosition = FirePosition + 1;
        }
        else FirePosition = 1;
        yield return new WaitForSeconds(RotateTime);
        IsIdle = true;
    }
        
    void FindGoalZ()
    {
        switch (FirePosition)
        {
            case 1:
                GoalZ = 0;
                break;
            case 2:
                GoalZ = 300;
                break;
            case 3:
                GoalZ = 240;
                break;
            case 4:
                GoalZ = 180;
                break;
            case 5:
                GoalZ = 120;
                break;
            case 6:
                GoalZ = 60;
                break;
            default:
                Debug.Log("turning off current bullet returned with a position not 1-6");
                break;
        }
        GoalZ += 20f;

    }
}


//Random Notes and stuff
//this.gameObject.transform.Rotate(0, 0, 60);
//var x = -1 * transform.localEulerAngles.x; 
//var y = -1 * transform.localEulerAngles.y;
//Quaternion asdf = Quaternion.Euler(0, 0, transform.localRotation.z);
//this.gameObject.transform.localRotation = asdf;

//Quaternion asdf = Quaternion.Euler(0, 0, transform.rotation.z);
//this.gameObject.transform.rotation = asdf;
//var z = Mathf.Lerp(transform.localEulerAngles.z, GoalZ - transform.localEulerAngles.z, 0);// - (-1*transform.localEulerAngles.z);
//var x = -1 * transform.localEulerAngles.x;
//var y = -1 * transform.localEulerAngles.y;
//this.gameObject.transform.Rotate(x, y, 0); 
//var x = -1 * transform.localEulerAngles.x; //this.transform.rotation.x - transform.localEulerAngles.x;
//var y = -1 * transform.localEulerAngles.y; //this.transform.rotation.y - transform.localEulerAngles.y;
//var z = GoalZ - transform.localEulerAngles.z;
//Mathf.Lerp(minimum, GoalZ, 0)

//GoalRot.x = GoalRot.x - this.transform.localRotation.eulerAngles.x;
//GoalRot.y = GoalRot.y - this.transform.localRotation.eulerAngles.y;
//FindGoalRotZ();
//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(GoalRot), Time.deltaTime * Smooth);

// GoalRotQuat = this.gameObject.transform.rotation //this.transform.localRotation;
//FindGoalRotZ();
//this.gameObject.transform.localRotation = new Quaternion(0, 0, GoalRot.z, 0);
//this.gameObject.transform.Rotate(-1 * this.transform.localRotation.eulerAngles.x, -1 * this.transform.localRotation.eulerAngles.y, 0);
//CurrentRot = this.transform.localEulerAngles; 
//GoalRot = new Vector3(CurrentRot.x, CurrentRot.y, 0);
//FindGoalRotZ();
//CurrentRot = this.transform.localEulerAngles;
//GoalRot = new Vector3(CurrentRot.x, CurrentRot.y, 0);
//FindGoalRotZ();