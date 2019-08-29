using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour {

    public GameObject secuela;

    private Jugador player;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Jugador>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Physics2D.IgnoreLayerCollision(10, 8);
	}

    void OnCollisionEnter2D(Collision2D col) {
        
        if(col.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            player.RecibirDamage( transform.parent.GetComponent<Enemy>().damage );
        }
        
        if(col.gameObject.CompareTag("Floor")) 
            Destroy(gameObject);

    }
}
