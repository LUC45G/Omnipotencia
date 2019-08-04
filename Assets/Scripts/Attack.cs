using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    Jugador player;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision) {

        
        if ( player.GetComponentInChildren<WeaponController>().getWeapon().tipo == 0 ) 
            Destroy(gameObject);

        if ( collision.gameObject.CompareTag("Enemy")) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.RecibirDamage(player.CalcularDamage());
        }
        
    }
}
