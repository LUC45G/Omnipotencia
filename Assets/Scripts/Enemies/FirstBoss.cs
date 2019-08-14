using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : MonoBehaviour {

    private float fireRate = 2f;
    private float nextFire = 0.0f;

    public GameObject proyectil;
    public Transform player;
    public Transform attackSpawner;


    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Physics2D.IgnoreLayerCollision(10, 11);
		// Attack();
	}

    void Attack() {

        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;

            Vector2 dir = player.position - this.transform.position;
            
            dir.Normalize();
            dir.y += 1;


            GameObject instantiatedObj = (GameObject) Instantiate(proyectil, attackSpawner.position, attackSpawner.rotation);

            instantiatedObj.transform.parent = gameObject.transform;
            
            Rigidbody2D attack_rb = instantiatedObj.GetComponent<Rigidbody2D>();
            attack_rb.AddForce(dir * 12, ForceMode2D.Impulse);
            // Destroy(instantiatedObj, 1.0f);
        }
    }
}
