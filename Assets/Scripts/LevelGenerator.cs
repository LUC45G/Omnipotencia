using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] floors;
    public GameObject[] enemies;
    public GameObject baseGO;
    public GameObject exitPoint;
    public GameObject chest;


    private int enemyCount;



    void Awake() {
        enemyCount = 0;
    }
	// Use this for initialization
	void Start () {
		//SpawnFloors();

        SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(enemyCount == 0) 
            EndLevel();
        
	}

    void SpawnFloors() {
        System.Random r = new System.Random();
        int rng = r.Next(1, 7);
        rng = rng + 2;

        float posY = baseGO.transform.position.y;
        float height = baseGO.GetComponent<Collider2D>().bounds.size.y;

        for (int i = 1; i<rng; i++) {
            int randomObject = r.Next(0, floors.Length);
            Instantiate( floors[randomObject], new Vector3( floors[randomObject].transform.position.x, posY + (height * i) , floors[randomObject].transform.position.z ), Quaternion.identity );
        }
    }

    void SpawnEnemies() {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        System.Random rand = new System.Random();

        for(int i = 0; i < spawnPoints.Length; i++) {
            int val = rand.Next(0, 2);

            if(val == 1) {
                int enemy = rand.Next(0, enemies.Length);
                Vector3 pos = spawnPoints[i].transform.position;
                Instantiate(enemies[enemy], pos, Quaternion.identity);

                enemyCount++;
            }

        }
    }

    public void KillEnemy() {
        enemyCount--;
    }

    void EndLevel() {
        GameObject finishPoint = GameObject.FindGameObjectWithTag("Finish");
        Instantiate(exitPoint, finishPoint.transform.position, Quaternion.identity);
        Instantiate(chest, GameObject.FindGameObjectWithTag("ChestSpawnPoint").transform.position, Quaternion.identity);
        enemyCount = -1;
    }
}
