using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour {

    private PlayerMovementController player;
    private Rigidbody2D rb;
    private bool canDoubleJump;
    private float doubleJumpDelay = 5f, nextDoubleJump = 0f; 

    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        canDoubleJump = true;
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if ( player.CanDoubleJump() && this.canDoubleJump) {
            if(Input.GetKeyDown(KeyCode.K)) {
                rb.AddForce(Vector2.up * player.GetJumpForce(), ForceMode2D.Impulse);
                this.canDoubleJump = false;
            }
        }
	}

    void Update() {
        if ( !player.CanDoubleJump() && Time.time > nextDoubleJump) {
            nextDoubleJump = Time.time + doubleJumpDelay;

            this.canDoubleJump = true;
        }
    }
}
