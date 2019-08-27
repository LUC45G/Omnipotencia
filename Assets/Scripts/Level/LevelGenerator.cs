using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] floors;
    public GameObject baseGO;
    public GameObject endingFloor;
    public GameObject exitPoint;
    public GameObject chest;
    private int floorCount, currentFloor;
    private float height;
    private System.Random r = new System.Random();

    void Awake() {
        floorCount = r.Next(10, 20);
        currentFloor = 1;
        height = baseGO.GetComponent<Collider2D>().bounds.size.y;
        
        gameObject.GetComponent<BuildingController>().setHeight(height);
        Screen.SetResolution(640, 480, false);
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update () {

    }

    void SpawnNextFloor() {

        int randomObject = r.Next(0, floors.Length);
        Transform t = floors[randomObject].transform;
        Instantiate( floors[randomObject], new Vector3( t.position.x, t.position.y + (height * currentFloor) , t.position.z ), Quaternion.identity );

        currentFloor++;
        
    }

    public void EndLevel(Vector3 chestPosition, Vector3 doorPosition) {
        Instantiate(exitPoint, doorPosition, Quaternion.identity);
        
        System.Random r = new System.Random();
        int random = r.Next(0, 101);
        
        //if(random % 3 == 0)
            Instantiate(chest, chestPosition, Quaternion.identity);

        if( currentFloor < floorCount) 
            SpawnNextFloor();
        else 
            Instantiate( endingFloor, new Vector3( endingFloor.transform.position.x, endingFloor.transform.position.y + (height * floorCount) , endingFloor.transform.position.z ), Quaternion.identity );
    }
}
