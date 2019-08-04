using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public GameObject[] enemies;
    int enemyCount;
    LevelGenerator lg;
    void Awake () {
        enemyCount = 0 ;
        SpawnEnemies();
        lg = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LevelGenerator>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( enemyCount == 0 ) {
            Vector3 pos = FindChestInChildren();
            lg.EndLevel( pos );
            enemyCount = -1;
        }

	}

    void SpawnEnemies() {
        enemyCount = 0;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        int j = 0;
        for (int i = 0; i < this.transform.childCount; i++) {
            Transform child = this.transform.GetChild(i);
            if (child.CompareTag("SpawnPoint")) {
                spawnPoints[j++] = child.gameObject;
            }
        }

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

    Vector3 FindChestInChildren() {
        Vector3 pos = Vector3.zero;
        bool chestFound = false;

        for (int i = 0; i < this.transform.childCount && !chestFound; i++) {
            
            Transform child = this.transform.GetChild(i);
            
            if (child.CompareTag("ChestSpawnPoint")) {
                pos = child.transform.position;
                chestFound = true;
            }
            
        }

        return pos;
    }
}
