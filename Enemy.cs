using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    #region Variables
    public int HP;
    public int maxHP;
    public NavMeshAgent myAgent;
	[HideInInspector]
    public bool once = false;
    public bool onceCount = false;

    public bool isStunnedMove = false;
    public bool isStriking  = false;
    public bool isStunnedAtk = false;
    public bool isAimedAt = false;
    public bool checkAim = true;
    public bool isDodging = false;


    public float OrigStrikeTime;
    //public float OrigWindUpTime;
    public float strikeDistance;
    public int strikeDamage;

    public int baseStrikeDamage;
    
    public float strikeTime;
    public float OrigMoveSpeed;
    public Healthbar myHB;
    public bool isDead;

    public float bufferDist = .5f;
    public float burstForce = 1000f;
    //public Rigidbody rb;

    public Animator AnimationContorller;

    public GameObject[] BodyParts;
    public GameObject[] ThingsToDestroy;
    //public GameObject[] Joints;

    EnemyController enemyContr;

    public Spawner mySpawner;

    private bool isAcid = false;
    #endregion

    private void Awake()
    {
        if (gameObject.tag != "Dummy")
        {
            enemyContr = FindObjectOfType<EnemyController>();
            myAgent = gameObject.GetComponent<NavMeshAgent>();
            strikeTime = OrigStrikeTime;

            if (!this.GetComponent<BalloonEnemy>())
            {
                OrigMoveSpeed = myAgent.speed;
            }
            
            AnimationContorller = this.gameObject.GetComponent<Animator>();
        }
        GetComponentInChildren<Canvas>().enabled = false;
        isDead = false;
        HP = maxHP;
        DamageTextController.Initialize();

        baseStrikeDamage = strikeDamage;


        //rb = GetComponent<Rigidbody>();
    }

    public virtual void hit(int dam, Color damColor) //function to deal damage to the enemy
    {
        DamageTextController.CreateDamageText(dam.ToString(), transform, damColor);

        if (gameObject.tag != "Dummy")
        {
            HP -= dam;
            myHB.UpdateBar((float)HP / maxHP);
            if (HP <= 0 && !isDead)
            {
                foreach (StatusEffect s in GetComponents<StatusEffect>())
                {
                    Destroy(s);
                }
                if (mySpawner)
                {
                    enemy_puzzle_controller g = mySpawner.GetComponentInParent<enemy_puzzle_controller>();
                    if (g)
                    {
                        g.decCount();
                    }
                }
                if (Random.Range(0, 4) == 0)
                {
                    if(Random.Range(0,2) == 0)
                    {
                        if (name.Contains("Blaster"))
                        {
                            Instantiate(Resources.Load<GameObject>("Armor"), transform.position + Vector3.up, Quaternion.identity, null);
                        }
                        else
                        {
                            Instantiate(Resources.Load<GameObject>("Armor"), transform.position, Quaternion.identity, null);
                        }

                    }
                    else
                    {
                        if (name.Contains("Blaster")) {
                            Instantiate(Resources.Load<GameObject>("SpeedReloader"), transform.position + Vector3.up, Quaternion.identity, null);
                        }
                        else
                        {
                            Instantiate(Resources.Load<GameObject>("SpeedReloader"), transform.position, Quaternion.identity, null);
                        }

                    }
                }
                StartCoroutine("Die");
            }
        }
    }
    
    virtual public IEnumerator Strike()
    {
        yield return null; //this is done in the child script
    }

    virtual public IEnumerator Die()
    {
        yield return null; //this is done in the child script
    }

    private IEnumerator Jump()
    {
        myAgent.isStopped = true;
        AnimationContorller.SetTrigger("Jump");
        Debug.Log("Jump");
        yield return new WaitForSeconds(.5f);
        myAgent.isStopped = false;
        while (myAgent.isOnOffMeshLink)
        {
            yield return null;
        }
        myAgent.isStopped = true;
        yield return new WaitForSeconds(.3f);
        myAgent.isStopped = false;
        once = false;
    }

    private IEnumerator WaitForDodge()
    {
        checkAim = false;

        yield return new WaitForSeconds(10f);
        checkAim = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cart")
        {
            hit(Mathf.FloorToInt(other.gameObject.GetComponent<Minecart>().speed/10f), Color.blue);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "acidPool" && !isAcid)
        {
            StartCoroutine("AcidMe");
        }
    }

    private IEnumerator AcidMe()
    {
        isAcid = true;
        hit(1, Gun.instance.GetColor((int)bulletType.Corrosive));
        yield return new WaitForSeconds(.5f);
        isAcid = false;
    }

    private void OnMouseEnter()
    {
        if (!isDead && Time.timeScale > 0)
        {
            GetComponentInChildren<Canvas>().enabled = true;
            isAimedAt = true;
        }
    }

    private void OnMouseExit()
    {
        if (!isDead)
        {
            GetComponentInChildren<Canvas>().enabled = false;
            isAimedAt = false;
        }
    }

    //check player's aim 
    public void CheckPlayerAim()
    {
        int randNum = 0;
        bool isTure = true;

        //Debug.Log("isAimedAt = " + isAimedAt);

        //if player is aiming at this enemy -> randomly dodge
        if (isAimedAt)
        {
            randNum = Random.Range(GetComponentInChildren<BruiserGolem>().randNumMin, GetComponentInChildren<BruiserGolem>().randNumMax);
            //Debug.Log("rightnum = " + GetComponentInChildren<BruiserGolem>().rightNum + " randnum = " + randNum);

            if (/*randNum == GetComponentInChildren<BruiserGolem>().rightNum*/ isTure && checkAim)
            {
                GetComponentInChildren<BruiserGolem>().Dodge();
                //StartCoroutine(GetComponentInChildren<BruiserGolem>().Dodge());
                myAgent.enabled = false;
                isDodging = true;
                //Debug.Log("They match!");

            }
            StartCoroutine("WaitForDodge");
            //Debug.Log("Check aim! randNum = " + randNum);
            
        }
    
    }

}
