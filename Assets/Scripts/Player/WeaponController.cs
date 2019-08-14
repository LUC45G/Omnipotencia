using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour {

    private float nextFire = 0.0f;

    private Arma arma;


    void Awake() {
        arma = GetComponentInParent<Arma>();
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Attack();
	}

    void Attack() {
        
        if ( Input.GetKey(KeyCode.K) && Time.time > nextFire ) {

            // controla la velocidad de ataque. 
            // TODO: Cambiar la velocidad de ataque local por la del arma
            nextFire = Time.time + arma.velocidad_de_ataque;

            Vector2 target = new Vector2(0f, 0f);


            if(Input.GetAxis("Vertical") > 0 ) // Si esta apuntando arriba, el vector apunta vertical positivo
                target = new Vector2(0f, 1f);
            if ( Input.GetAxis("Vertical") < 0) // Si esta apuntando abajo, el vector apunta vertical negativo
                target = new Vector2(0f, -1f);
            
            if( target.y == 0f )     // Si no esta apuntando vertical, controla los horizontales
                if ( transform.rotation.x == 0)
                    target = new Vector2(1f, 0f);
                else 
                    target = new Vector2(-1f, 0f);

            GameObject instantiatedObj = (GameObject) Instantiate(arma.ataque, transform.position, transform.rotation);

            float aux_Y = 1 - Math.Abs(target.y); // devuelve 0 si apuntando vertical, 1 si apuntando horizontal

            instantiatedObj.transform.rotation = Quaternion.Euler( ( target.x*(target.x - 1) * 90) , 0, (aux_Y * ( (1 - target.x) * -90 )) + ( (1 - aux_Y) * ( target.y * 90 ) ) );

            if(arma.tipo == 0) { // Si el arma es a rango
                Rigidbody2D attack_rb = instantiatedObj.GetComponent<Rigidbody2D>();
                attack_rb.AddForce(target * 100, ForceMode2D.Impulse);
                Destroy(instantiatedObj, 1.0f);
            }
            else { // Si es melee
                Destroy(instantiatedObj, 0.1f);
            }
            
        }
    }

    public Arma getWeapon() {
        return arma;
    }
}