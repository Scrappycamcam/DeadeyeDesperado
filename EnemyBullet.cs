using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Transform PTransform;
    private Vector3 PlayerPosWhenFired;
    public Vector3 Direction;
    public float Speed;
    public int Damage;

    public bool isExplosive = false;
    public float explosionRadius = 3f; //radius of explosion
    public float explosionMultiplier = 1.0f; //damage reduction from explosive vs direct hit

    public GameObject explosionEffect;

    private void Start()
    {
        PTransform = Player.Instance.transform;
        PlayerPosWhenFired = PTransform.position;
        Direction = new Vector3(PTransform.position.x - transform.position.x, PTransform.position.y - transform.position.y, PTransform.position.z - transform.position.z);
        transform.LookAt(Player.Instance.transform);
        //this.gameObject.GetComponent<ParticleSystem>().veloc//velocityOverLifetime. = Direction.x;
        StartCoroutine("DelayDie");
    }

    public void Update()
    {
        // transform.position = Vector3.MoveTowards(transform.position, Direction + transform.position, Speed * Time.deltaTime);
        //this.gameObject.GetComponent<Rigidbody>().AddForce(Direction);
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet Hit Somthing");
        Destroy(this.gameObject);
     
    }
    */

    private bool hasExploded = false;

    void OnParticleCollision(GameObject collision)
    {

        //Debug.Log(collision.name);
        if (!(collision.gameObject.tag == "BruiserEnemy") && !(collision.gameObject.tag == "BlasterEnemy"))
        {
            //Debug.Log(collision.gameObject.name);
            //Debug.Log(collision.gameObject.tag);
            //Debug.Log("Bullet touched an object");
            Destroy(transform.GetChild(1).gameObject);
            Destroy(transform.GetChild(0).gameObject);
            Destroy(GetComponent<ParticleSystem>());
            if (isExplosive && !hasExploded)
            {
                Debug.Log("Watch Me EXPLOOOOODE");
                Instantiate(explosionEffect, collision.transform.position, Quaternion.identity, null);
                hasExploded = true;
                float distance = Vector3.Distance(Player.Instance.transform.position, collision.transform.position);
                if (distance < explosionRadius)
                {
                    Debug.Log("Dealing Explosive Damage to Player");
                    int dam = Mathf.RoundToInt((float)Damage * explosionMultiplier * ((explosionRadius - distance) / explosionRadius));
                    Debug.Log(dam);
                    Player.Instance.Damage(dam); //multiplty the damage by the multiplier, then multiply by a fraction based on how far the player is from the explosion

                }
            }
            StartCoroutine("Die");
            //Destroy(this.gameObject);
        }


    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0f);
        Destroy(this.gameObject);
    }

    IEnumerator DelayDie()
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }

}
