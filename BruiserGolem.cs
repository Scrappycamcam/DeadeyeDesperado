using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BruiserGolem : Enemy
{

    #region Variables
    public List<GameObject> Arms;
    public GameObject Tornado;
    public int rightNum = 2;
    public int randNumMin = 0;
    public int randNumMax = 10;
    public float dodgeWait = 15f;
    //public Vector3 dodgeForce;
    public Vector3 dodgePos;
    //public float dodgeForceMin = 1;
    //public float dodgeForceMax = 5;
    public float dodgePosMin = 1;
    public float dodgePosMax = 5;
    public bool moveLeft = false;
    public bool moveRight = false;
    Rigidbody rb;

    public float rayDistance;
    //private bool IsAimedAt = false;
    #endregion


    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public IEnumerator DodgeMove()
    { 
        
            myAgent.enabled = false;
        /*Vector3 moveVect = Vector3.zero;
        if (moveLeft)
        {
            moveVect = Vector3.left;
        }
        else if (moveRight)
        {
            moveVect = Vector3.right;
        }
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;*/
        //Debug.Log("moveVect = " + moveVect);
        //Debug.Log("I'm moving " + moveVect * burstForce);
        //Debug.Log("dodgePos = " + dodgePos);
        //rb.AddForce(dodgePos * burstForce, ForceMode.Impulse);
        //Debug.Log("added force!");
        yield return new WaitForSeconds(.5f);
        //myAgent.enabled = true;
        moveLeft = false;
        moveRight = false;
    }

    override public IEnumerator Strike()
    {
        if (!isDead)
        {
            isStriking = true;
            myAgent.isStopped = true;
            if (this.gameObject.name.Contains("Golem"))
            {
                GetComponent<AudioSource>().Play();
                AnimationContorller.SetTrigger("Attack");
                Tornado.SetActive(true);

            }
            yield return new WaitForSeconds(0.25f);

            foreach (GameObject Arm in Arms)
            {
                if (!isDead)
                {
                    Arm.gameObject.GetComponent<Collider>().enabled = true;
                }
            }

            StartCoroutine("WaitForDodge");

            yield return new WaitForSeconds(strikeTime * 3 / 4);
            foreach (GameObject Arm in Arms)
            {
                if (Arm)
                {
                    //Debug.Log("turn off arm colliders");
                    Arm.gameObject.GetComponent<Collider>().enabled = false;
                }
            }
            //Tornado.SetActive(false);
            if (myAgent)
            {
                if (myAgent.enabled)
                {
                    myAgent.isStopped = false;
                }
            }
            isStriking = false;
        }
    }

    override public IEnumerator Die()
    {
        isDead = true;

        Player.Instance.GetComponentInChildren<Gun>().AddDeadeye(10);
        

        //this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        //myAgent.enabled = false; //this isnt working
        //this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //yield return new WaitForSeconds(5);

        int i = 0;


        foreach (GameObject BodyPart in BodyParts)
        {
            BodyPart.gameObject.GetComponent<BoxCollider>().enabled = true;
            //BodyPart.AddComponent(typeof(Rigidbody));
            BodyPart.gameObject.GetComponent<Rigidbody>().useGravity = true;
            BodyPart.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Vector3 BoxColliderOffset = new Vector3(BodyPart.gameObject.GetComponent<BoxCollider>().center.x, BodyPart.gameObject.GetComponent<BoxCollider>().center.y, BodyPart.gameObject.GetComponent<BoxCollider>().center.z);
            BodyPart.transform.position = new Vector3(BodyPart.transform.position.x - BoxColliderOffset.x, BodyPart.transform.position.y - BoxColliderOffset.y, BodyPart.transform.position.z - BoxColliderOffset.z);
            BodyPart.gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
            //Vector3 TempPos = Joints[i].gameObject.transform.position;
            //Vector3 TempLocalPos = Joints[i].gameObject.transform.localPosition;
            //Quaternion TempLocalRot = Joints[i].gameObject.transform.localRotation;
            //BodyPart.gameObject.GetComponent<BoxCollider>().center = TempLocalPos;
            //BodyPart.transform.position = TempPos;
            //BodyPart.transform.localRotation = TempLocalRot;
            i++;
        }

        foreach (GameObject Thing in ThingsToDestroy)
        {
            Destroy(Thing);

        }

        Destroy(this.gameObject.GetComponent<Animator>());
        Destroy(this.gameObject.GetComponent<NavMeshAgent>());
        yield return new WaitForSeconds(5);

        for (int LoopVar = 0; LoopVar < 50; LoopVar++)
        {
            foreach (GameObject BodyPart in BodyParts)
            {
                BodyPart.GetComponent<Rigidbody>().isKinematic = true;
                Vector3 TempPos = BodyPart.gameObject.transform.position;
                BodyPart.gameObject.transform.position = new Vector3(TempPos.x, TempPos.y - 0.025f, TempPos.z);

            }
            yield return new WaitForSeconds(0.00001f);
        }

        Destroy(this.gameObject);


        
    }

    public void Step1Audio(AudioClip Step)
    {
        //Debug.Log("Step");
        GetComponent<AudioSource>().PlayOneShot(Step);
    }

    //if player is aiming at enemy dodge
    public void Dodge()
    {
        //Debug.Log("DODGE");
        if (!this.once)
        {
            if (Physics.Raycast(transform.position, Vector3.left, out var hit, rayDistance))
            {
                if (hit.transform.gameObject.layer == SortingLayer.NameToID("Vault"))
                {
                    Debug.Log("STOPPED A DODGE");
                    return;
                }

            }

            if (Physics.Raycast(transform.position, Vector3.right, out var hit2, rayDistance))
            {
                if (hit2.transform.gameObject.layer == SortingLayer.NameToID("Vault"))
                {
                    Debug.Log("STOPPED A DODGE");
                    return;
                }
            }
            Debug.Log("Made it here");
            //play animation
            //move enemy
            //this.StartCoroutine("Jump");

            //dodgeForce = DetermineDodgeForce();

            dodgePos = DetermineDodgeForce();
            StartCoroutine(DodgeMove());
            
        }

    }

    Vector3 DetermineDodgeForce()
    {
        Vector3 curPos = this.transform.position;
        Vector3 newPos;

        //determine left or right
        int randNum1 = Random.Range(0, 2);
        float randNum2 = Random.Range(dodgePosMin, dodgePosMax);

        //convert to two decimals
        randNum2 = randNum2 - (float)(randNum2  % .01);

        //Debug.Log("randNum1 = " + randNum1 + "  randNum2 = " + randNum2);

        if(randNum1 == 1)
        {
            //left movement
            moveLeft = true;
            //newForce = this.transform.position + (Vector3.left * randNum2);

            newPos = this.transform.position + (Vector3.left * randNum2);
        }
        else
        {
            //right movement
            moveRight = true;
            //newForce = this.transform.position + (Vector3.right * randNum2);

            newPos = this.transform.position + (Vector3.right * randNum2);
        }


        return newPos;
    }



}
