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
		Physics2D.queriesStartInColliders = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        Vector2 looking = ( player.transform.rotation.y == 0 ) ? Vector2.right : Vector2.left;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, looking * 17, player.transform.position.x + 17);

        if( ray.collider == null ) {
            Debug.DrawRay(player.transform.position, looking * 17, Color.green);
            DashControl();
        }
        else
            Debug.DrawRay(player.transform.position, looking * 17, Color.red);
            
            
            

	}

    void DashControl() {
        if(Input.GetKeyDown(KeyCode.L)) {
            if( !isDashing ) {
                isDashing = true;
                float movPos = player.transform.position.x + 17;
                float movNeg = player.transform.position.x - 17;
                float control = ( player.transform.rotation.x == 0f ) ? movPos : movNeg;

                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

                rb.MovePosition	( new Vector2(control, player.transform.position.y) ) ;
            }
            
            
            isDashing = false;
        }
    }
}
