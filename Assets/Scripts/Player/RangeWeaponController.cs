using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeWeaponController : MonoBehaviour {

    private float nextFire = 0.0f;
    private Vector3 initialPosition;
    private Arma arma;

    private Vector3 offset;

    void Awake() {
        arma = this.GetComponent<Arma>();
        initialPosition = this.transform.position;
        offset = this.transform.position - this.GetComponentInParent<Transform>().position;
    }

	// Use this for initialization
	void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
	    Translate();
    	Attack();
	}

    void Attack() {
        
        if ( Input.GetKey(KeyCode.K) && Time.time > nextFire ) {

            // controla la velocidad de ataque. 
            nextFire = Time.time + arma.velocidad_de_ataque;

            
            Vector2 target = Vector2.zero;


            if(Input.GetAxis("Vertical") > 0 ) // Si esta apuntando arriba, el vector apunta vertical positivo
                target = new Vector2(0f, 1f);
            else if ( Input.GetAxis("Vertical") < 0) // Si esta apuntando abajo, el vector apunta vertical negativo
                target = new Vector2(0f, -1f);
            
            if( target.y == 0f )     // Si no esta apuntando vertical, controla los horizontales
                if ( transform.rotation.x == 0)
                    target = new Vector2(1f, 0f);
                else 
                    target = new Vector2(-1f, 0f);

            float aux_Y = 1 - Math.Abs(target.y); // devuelve 0 si apuntando vertical, 1 si apuntando horizontal
            Quaternion objectRotation = Quaternion.Euler( ( target.x*(target.x - 1) * 90) , 0, (aux_Y * ( (1 - target.x) * -90 )) + ( (1 - aux_Y) * ( target.y * 90 ) ) );

            foreach ( Attack shot in arma.getAttacks() ) {
                GameObject instantiatedObj = (GameObject) Instantiate(shot.getShoot(), transform.position, objectRotation);
                
                Rigidbody2D attack_rb = instantiatedObj.GetComponent<Rigidbody2D>();
                attack_rb.AddForce(target * 100, ForceMode2D.Impulse);
            }

            }
        }
    

    void Translate() {

        if ( Input.GetAxis("Vertical") > 0 ) 
            this.transform.position = this.transform.parent.transform.position + Vector3.up;
        else if ( Input.GetAxis("Vertical") < 0 )
            this.transform.position = this.transform.parent.transform.position - Vector3.up;
        else
            this.transform.position += offset;
        

    }

    public Arma getWeapon() {
        return arma;
    }
}