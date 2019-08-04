using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public Sprite openedChest;
    public GameObject item;
    bool isOpened;

    void Awake() {
        isOpened = false;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col) {

        if( !isOpened && col.CompareTag("Player") ) {
            
            isOpened = true;
            
            GetComponent<SpriteRenderer>().sprite = openedChest;

            Instantiate(item, this.transform.position, Quaternion.identity);
            
        }

    }
}
