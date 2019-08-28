using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    Jugador player;
    public GameObject shoot;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Jugador>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision) {

        
        if ( player.GetComponentInChildren<RangeWeaponController>().getWeapon().tipo == 0 ) 
            Destroy(gameObject);

        if ( collision.gameObject.CompareTag("Enemy")) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.RecibirDamage(player.CalcularDamage());
        }
        
    }

    public GameObject getShoot() {
        return shoot;
    }
}
