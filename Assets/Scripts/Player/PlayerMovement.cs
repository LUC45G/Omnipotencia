using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    private BoxCollider2D _collider;
    private Rigidbody2D rb;
    private float defaultGravityScale;
    float velocidad_movimiento = 25;
    float fuerza_salto = 75;
    private bool estaSaltando = false, estaEnEscalera = false;

	// Use this for initialization
	void Start () {
		Physics2D.queriesStartInColliders = false;
        _collider = gameObject.GetComponent<BoxCollider2D>();

        rb = GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {

        float media = _collider.bounds.size.x / 2;
        RaycastHit2D hitRight = Physics2D.Raycast( new Vector2(transform.position.x + media, transform.position.y), Vector2.down, float.Parse( (_collider.bounds.size.y/1.7).ToString() )   );
        RaycastHit2D hitLeft = Physics2D.Raycast( new Vector2(transform.position.x - media, transform.position.y), Vector2.down, float.Parse( (_collider.bounds.size.y/1.7).ToString() )   );

        if (hitRight.collider != null || hitLeft.collider != null) // true cuando esta tocando superficie
            estaSaltando = false;
        else 
            estaSaltando = true;

        
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
        if ( col.CompareTag("Ladder") ) {
            estaEnEscalera = true;
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
        }
    }

    void OnTriggerExit2D( Collider2D col ) {

        if ( col.CompareTag("Ladder") ) {
            estaEnEscalera = false;
            rb.gravityScale = defaultGravityScale;

        }
    }
	
}
