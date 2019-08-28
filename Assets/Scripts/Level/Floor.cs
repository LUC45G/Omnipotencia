using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public GameObject[] enemies;
    int enemyCount;
    LevelGenerator lg;

    Vector3 chestPos = Vector3.zero, doorPos = Vector3.zero; 
    void Awake () {
        SpawnEnemies();
        lg = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LevelGenerator>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( enemyCount == 0 ) {
            FindChestAndDoor();
            lg.EndLevel( chestPos, doorPos );
            enemyCount = -1;
        }

	}

    void SpawnEnemies() {
        enemyCount = 0;
        int j = 0;
        GameObject[] auxArray = new GameObject[100];

        for (int i = 0; i < this.transform.childCount; i++) {
            Transform child = this.transform.GetChild(i);
            if (child.CompareTag("SpawnPoint"))
                auxArray[j++] = child.gameObject;
        }

        GameObject[] spawnPoints = new GameObject[j];

        for(int i = 0; i < j; i++) 
            spawnPoints[i] = auxArray[i];

        System.Random rand = new System.Random();
        for(int i = 0; i < spawnPoints.Length; i++) {
            int val = rand.Next(0, 2);

            if(val == 1) {
                int enemy = rand.Next(0, enemies.Length);
                Vector3 pos = spawnPoints[i].transform.position;
                Instantiate(enemies[enemy], pos, Quaternion.identity).GetComponent<Enemy>().SetFloor(this);

                enemyCount++;
            }

        }
    }

    public void KillEnemy() {
        enemyCount--;
    }

    void FindChestAndDoor() {
        bool chestFound = false;
        bool doorFound = false;
        bool finish = false;

        for (int i = 0; i < this.transform.childCount && !finish; i++) {
            
            Transform child = this.transform.GetChild(i);
            
            if ( !chestFound && child.CompareTag("ChestSpawnPoint")) {
                chestPos = child.transform.position;
                chestFound = true;
            }

            if( !doorFound && child.CompareTag("exitPoint")) {
                doorPos = child.transform.position;
                doorFound = true;
            }

            finish = chestFound && doorFound;
            
        }
    }
}
