using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour {

    float velocidad_movimiento = 25;
    float fuerza_salto = 75;
    public Slider slider_vida;
    public WeaponController weaponController;
    private BuildingController bc;


    private Rigidbody2D rb;
    private bool estaSaltando, estaEscalando, estaEnEscalera;
    private float defaultGravityScale;

    // Atributos del personaje
    private int vida_maxima, vida_actual;
    private float resistencia, danio;

    void Awake() {
        vida_maxima = 100;
        vida_actual = vida_maxima;
        slider_vida.value = vida_maxima;
        resistencia = 20f;
        danio = 15.7f;
        bc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildingController>();
    }

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		
        Physics2D.IgnoreLayerCollision(8, 9);
        
        Move();
        Jump();

        if ( estaEnEscalera )
            Escalar();
	}

    void Move() {
        // Mueve al bicho atras o adelante
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += move * velocidad_movimiento * Time.deltaTime;


        // Gira al bicho segun donde este mirando
        if (move.x < 0) {
            transform.rotation = Quaternion.Euler(180, 0, 180);
            // GetComponent<Animator>().SetTrigger("Trigger");
        }
        if (move.x > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            // GetComponent<Animator>().SetTrigger("Trigger");
        }
        if(move.x == 0) {
            // GetComponent<Animator>().Rebind();
        }
    }

    void Jump() {
        // Si se aprieta espacio y está en el suelo, salta
        if(Input.GetKeyDown(KeyCode.J)) {
            if( !estaSaltando && !estaEnEscalera ) {
                estaSaltando = true;
                rb.AddForce(Vector2.up * fuerza_salto, ForceMode2D.Impulse);
            }
        }
    }

    void Escalar() {
        transform.Translate (new Vector2(0, Input.GetAxis("Vertical")) * Time.deltaTime*velocidad_movimiento);
    }

    void OnTriggerEnter2D(Collider2D col) {

        if(col.CompareTag("Object")) {
            String nombre_item = col.name.Remove(col.name.Length - 7);
            System.Type nombre = System.Type.GetType(nombre_item + ",Assembly-CSharp");
            gameObject.AddComponent(nombre);

            Destroy(col.gameObject);
        }

        if (col.CompareTag("exitPoint")) {
            bc.SubirDeNivel();
        }

        if ( col.CompareTag("Ladder") ) {
            estaEnEscalera = true;
            rb.gravityScale = 0;
            if (estaSaltando) {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = 0f;
            }
                
        }
    }

    void OnTriggerStay2D ( Collider2D col ) {

        

    }

    void OnTriggerExit2D( Collider2D col ) {

        if ( col.CompareTag("Ladder") ) {
            estaEnEscalera = false;
            rb.gravityScale = defaultGravityScale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        // Si la colision fue contra un enemigo, reinicia el nivel
        if ( collision.gameObject.CompareTag("Enemy") ) {
            RecibirDamage(collision.gameObject.GetComponent<Enemy>().damage);

            float xPushDir = transform.position.x - collision.transform.position.x;
            rb.AddForce( new Vector2(xPushDir * 5f, 0f), ForceMode2D.Impulse );
        }

        if ( collision.gameObject.CompareTag("JumpPlatform") ) 
            if ( collision.contacts.Length > 0 ) {
                ContactPoint2D contact = collision.contacts[0];
                
                if(Vector3.Dot(contact.normal, Vector3.up) > 0.5) 
                    rb.AddForce( new Vector2(0f, 125f), ForceMode2D.Impulse );
            }

    }

    void OnCollisionStay2D(Collision2D collision) {

        
        // Detecta que la colision haya sido al piso y no a una pared o techo
        ContactPoint2D contact = collision.contacts[0];
        if(Vector3.Dot(contact.normal, Vector3.up) > 0.5) {
            // La colision fue desde abajo
            estaSaltando = false; 
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        estaSaltando = true;
    }

    public void RecibirDamage(float enemyD) {
        vida_actual -= (int) Math.Truncate(enemyD * (1-resistencia/100));
        slider_vida.value = vida_actual;

        if ( vida_actual < 0)
            SceneManager.LoadScene(0);
    }


    public int CalcularDamage() {
        return (int) (danio + weaponController.getWeapon().danio)/2;

    }

}
