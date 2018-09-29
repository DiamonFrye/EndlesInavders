using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public int maxEnemyNumber = 3;
    public GameObject enemy;
    public float spawnRate = 2f;

    private float randX;
    private float nextSpawn;
    private Vector2 spawnPosition;
    private float camHorizontalExtend;
    private float camVerticalExtend;

    // Use this for initialization
    void Start () {
        camVerticalExtend = Camera.main.orthographicSize;
        camHorizontalExtend = Camera.main.orthographicSize * Screen.width / Screen.height;
    }
	
	// Update is called once per frame
	void Update () {
        float mySpawnRate = 0f;
        int numberOfEnemysInGame = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (numberOfEnemysInGame <= maxEnemyNumber / 2)
        {
            mySpawnRate = spawnRate / 3;
        }
        else 
        {
            mySpawnRate = spawnRate;
        }
        Debug.Log("Spawn rate: " + mySpawnRate);
        if(numberOfEnemysInGame < maxEnemyNumber)
        {
            if(Time.time >= nextSpawn)
            { 
                nextSpawn = Time.time + mySpawnRate;
                randX = Random.Range(-camHorizontalExtend + 1f, camHorizontalExtend - 1f);
                spawnPosition = new Vector2(randX, camVerticalExtend + 2f);
                Instantiate(enemy, spawnPosition, Quaternion.identity);
            }
        }
		
	}
}
