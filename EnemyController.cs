using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform PTransform;

    public int sightDistance = 50;
    public int firingDistance = 150;
    public int runAwayDistance = 75;
    //public bool dodging = false;
    private Vector3 myPos;
    Enemy _enemy;
    bool allowShoot = true;

    public float dodgeSpeed = 15f;

    public bool isActive = true;




    private void Start()
    {
        PTransform = Player.Instance.transform;

    }

    public bool CombatChamber = false;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0 || !isActive) return; // this skips the update when the game is paused
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            _enemy = enemy;
            if (!enemy.isDead && enemy.gameObject.activeInHierarchy)
            {
                //Debug.Log(enemy.checkAim + " = checkAim");
                if (enemy.checkAim && enemy.GetComponentInChildren<BruiserGolem>())
                {
                    //enemy.CheckPlayerAim();
                }

                //Debug.Log("myAgent.enabled = " + enemy.myAgent.enabled);

                if (enemy.isDodging)
                {
                    myPos = enemy.transform.position;
                    enemy.transform.position = Vector3.Lerp(myPos, enemy.GetComponent<BruiserGolem>().dodgePos, dodgeSpeed/100);
                    //Debug.Log("dodgePos = " + enemy.GetComponent<BruiserGolem>().dodgePos);

                    //Debug.Log(enemy.name + " dodged");

                    if (Vector3.Distance(enemy.transform.position, enemy.GetComponent<BruiserGolem>().dodgePos) < 0.5f)
                    {
                        //dodging = false;
                        enemy.isDodging = false;
                    }
                }

                //Debug.Log("isdodging = " + enemy.isDodging);
                if (enemy.isDodging)
                {
                    enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    enemy.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    enemy.myAgent.enabled = false;
                    return;
                }

                if (!enemy.GetComponent<BalloonEnemy>())
                {
                    enemy.myAgent.enabled = true;
                    enemy.AnimationContorller.enabled = true;
                }
                
                if (!enemy.isStriking && !enemy.isStunnedAtk && !enemy.isDodging && !enemy.GetComponent<BalloonEnemy>())
                {
                    //Debug.Log("enemy is not striking or stunned attack");
                    if (!enemy.isStunnedMove)
                    {

                        //Debug.Log("enemy is not stunned move");
                        enemy.myAgent.SetDestination(PTransform.position);
                        if(enemy.myAgent.isOnOffMeshLink && !enemy.once)
                        {
                            enemy.StartCoroutine("Jump");
                            //Debug.Log("Yump");
                            enemy.once = true;
                        }
                        if (!CombatChamber)
                        {
                            if (enemy.myAgent.remainingDistance > sightDistance)
                            {
                                // Debug.Log("enemy is greater than 50 from player");
                                enemy.myAgent.isStopped = true;
                            }
                            else
                            {
                                //  Debug.Log("enemy is less than 50 from player");
                                // if (enemy.gameObject.name.Contains("Bruiser"))
                                //{
                                enemy.myAgent.isStopped = false;
                                //}
                            }
                        }
                    }
                    //Debug.Log("x = " + Vector3.Distance(PTransform.position, enemy.transform.position));
                    if (Vector3.Distance(PTransform.position, enemy.transform.position) < enemy.strikeDistance)
                    {
                        if (enemy.GetComponent<BlasterEnemy>() && enemy.GetComponent<BlasterEnemy>().canStrike)
                        {
                            enemy.StartCoroutine("Strike");
                        }

                        

                        if (enemy.GetComponent<BruiserGolem>())
                        {
                            enemy.StartCoroutine("Strike");
                        }

}
                        //  Debug.Log("strike is called");
                        
                    

                }

                if (enemy.GetComponent<BalloonEnemy>() && enemy.GetComponent<BalloonEnemy>().spin == false/*&& Vector3.Distance(PTransform.position, enemy.transform.position) < enemy.strikeDistance*/)
                {
                    //Debug.Log("Strike in controller");
                    enemy.StartCoroutine("Strike");
                    //StartCoroutine(enemy.GetComponent<BalloonEnemy>().WaitToShoot());
                    
                }
            }
            else if(enemy && enemy.myAgent && enemy.AnimationContorller)
            {
                enemy.myAgent.enabled = false;
                enemy.AnimationContorller.enabled = false;
            }

        }
        //Debug.Log("allowShoot = " + allowShoot);
    }

    
    
}


    

