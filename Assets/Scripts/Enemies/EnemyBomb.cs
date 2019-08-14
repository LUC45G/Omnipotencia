using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour {

    public GameObject secuela;

    private float delay = 1f;
    private float nextFire = 0.0f;

    private Jugador player;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>();
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
            player.RecibirDamage( transform.parent.GetComponent<Enemy>().damage );
            Destroy(gameObject);
        }
        
        if(col.gameObject.CompareTag("Floor")) 
            Destroy(gameObject);
           //ExpansiveWave();

    }

    void ExpansiveWave() {
        Vector2 centro = this.transform.position;
        Vector2 izq = this.transform.position;
        Vector2 der = this.transform.position;

        Destroy(gameObject);
        GameObject go = Instantiate(secuela, centro, Quaternion.Euler(0f, 0f, 0f)); 
        Destroy( go , 1f);
        

        int i = 0;

        
        while(i < 2) {
            
            if(Time.time > nextFire ) {
                // Debug.Log("i: " + i);
                nextFire = Time.time + delay;
                izq.x -= 5*i;
                der.x += 5*i;
                GameObject aux = Instantiate(secuela, izq, Quaternion.Euler(0f, 0f, 0f));
                Destroy( aux, 1f);
                GameObject aux2 = Instantiate(secuela, der, Quaternion.Euler(0f, 0f, 0f));
                Destroy(  aux2, 1f);
            }
            i++;

        }

    }
}
