using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject thingToSpawn = null;
    [SerializeField] private float delay = 5f;
    [SerializeField] private int totalToSpawn = 0;

    public int numSpawned = 0;
    private float timer;
    private GameObject myCrystals;

    public Transform placeToSpawnEnemies;


    private void Start()
    {
        if (totalToSpawn >= 0)
        {
            SpawnEnemy();
        }
        timer = Time.time;
        myCrystals = transform.parent.Find("Crystals_LP").GetChild(0).gameObject;
    }

    private void Update()
    {
        if((totalToSpawn > numSpawned || totalToSpawn == 0) && timer + delay < Time.time)
        {
            SpawnEnemy();
        }
        if (numSpawned == totalToSpawn || totalToSpawn < 0)
        {
            myCrystals.SetActive(false);
        }
        else
        {
            myCrystals.SetActive(true);
        }
    }

    private void SpawnEnemy()
    {
        GameObject g = Instantiate(thingToSpawn, placeToSpawnEnemies.transform.position, thingToSpawn.transform.rotation);
        g.GetComponent<Enemy>().mySpawner = this;
        timer = Time.time;
        numSpawned++;
        transform.GetComponentInParent<enemy_puzzle_controller>().incCount();

    }
}
