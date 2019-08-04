using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

	private bool isDashing = false;
    private GameObject player;
    

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        DashControl();

	}

    void DashControl() {
        if(Input.GetKeyDown(KeyCode.L)) {
            if( !isDashing ) {
                isDashing = true;
                float movPos = player.transform.position.x + 2;
                float movNeg = player.transform.position.x - 2;
                float control = ( player.transform.rotation.x == 0f ) ? movPos : movNeg;

                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

                // rb.MovePosition	( new Vector2 ) );
            }
            
            
            isDashing = false;
        }
    }
}
