using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] floors;
    public GameObject baseGO;
    public GameObject exitPoint;
    public GameObject chest;

    void Awake() {

    }
	// Use this for initialization
	void Start () {
		SpawnFloors();
	}
	
	// Update is called once per frame
    void Update () {

    }

    void SpawnFloors() {
        System.Random r = new System.Random();
        int rng = r.Next(1, 7);
        rng = rng + 2;

        float posY = baseGO.transform.position.y;
        float height = baseGO.GetComponent<Collider2D>().bounds.size.y;

        for (int i = 1; i<rng; i++) {
            int randomObject = r.Next(0, floors.Length);
            Transform t = floors[randomObject].transform;
            Instantiate( floors[randomObject], new Vector3( t.position.x, t.position.y + (height * i) , t.position.z ), Quaternion.identity );
        }
    }

    public void EndLevel(Vector3 pos) {
        GameObject finishPoint = GameObject.FindGameObjectWithTag("Finish");
        Instantiate(exitPoint, finishPoint.transform.position, Quaternion.identity);
        Instantiate(chest, pos, Quaternion.identity);
    }
}
