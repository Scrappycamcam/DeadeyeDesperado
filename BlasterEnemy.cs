using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlasterEnemy : Enemy
{

    #region Variables
    public GameObject Bullet;
    public GameObject GunMuzzle;
    private Transform PTransform;
    public float BulletDelay;
    public float TimeBetweenAttacks;

    public bool canStrike = true;
    #endregion

    private void Start()
    {
        PTransform = Player.Instance.transform;
        isDead = false;
        HP = maxHP;
        myHB = gameObject.GetComponent<Healthbar>();
        strikeTime = OrigStrikeTime;
        OrigMoveSpeed = myAgent.speed;
    }

    public void Update()
    {

        if (!isDead)//this.gameObject.GetComponent<NavMeshAgent>().enabled = true)// && myAgent.remainingDistance >1)
        {
            Quaternion LookRot = Quaternion.LookRotation(PTransform.position - transform.position);
            //float str = Mathf.Min(strength * Time.deltaTime, 1);
            //transform.rotation = targetRotation;         
            //Quaternion TargetRot = new Quaternion (transform.localRotation, Quaternion.Euler(0, 0, GoalZ), Time.deltaTime* Smooth);
            //Quaternion TempRot = Quaternion.Euler(0, 0, GoalZ);
            //transform.rotation = TempRot;
            transform.rotation = Quaternion.Euler(0, LookRot.eulerAngles.y, 0);
        }

        /*if (myAgent.isStopped)
        {
            canStrike = true;
        }
        else
        {
            canStrike = false;
        }*/

        

        //Debug.Log("isStopped = " + myAgent.isStopped);
    }


    override public IEnumerator Strike()
    {
        if (!isDead && !isStriking && !Physics.Linecast(GunMuzzle.transform.position, Gun.instance.transform.position, ~(1 << 2)))
        {
            Debug.Log("SHOOT");
            isStriking = true;
            myAgent.isStopped = true;
            AnimationContorller.SetBool("Attack", true);
            yield return new WaitForSeconds(BulletDelay);
            if (!isDead && myAgent)
            {
                Vector3 GoalPos = GunMuzzle.transform.position;
                var newBullet = Instantiate(Bullet, GoalPos, Quaternion.identity);
                newBullet.GetComponent<EnemyBullet>().Damage = strikeDamage;
                yield return new WaitForSeconds(0.1f);
                myAgent.isStopped = false;
                
                yield return new WaitForSeconds(TimeBetweenAttacks);
                isStriking = false;
            }
        }
    }

    override public IEnumerator Die()
    {
        isDead = true;

        Gun.instance.AddDeadeye(5);

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
            BodyPart.transform.position = new Vector3 (BodyPart.transform.position.x - BoxColliderOffset.x, BodyPart.transform.position.y - BoxColliderOffset.y, BodyPart.transform.position.z - BoxColliderOffset.z);
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

    public void BlasterAudio(AudioClip Fire)
    {
        GetComponent<AudioSource>().PlayOneShot(Fire);
    }
}
