using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour {

    public Slider slider_vida;
    public RangeWeaponController weaponController;
    public PassiveItems passives;
    private BuildingController bc;

    private List<PassiveItems.Item> items;



    // Atributos del personaje
    private int vida_maxima, vida_actual;
    private float resistencia, danio;

    void Awake() {
        vida_maxima = 250;
        vida_actual = vida_maxima;
        slider_vida.maxValue = vida_maxima;
        slider_vida.value = vida_maxima;
        resistencia = 20f;
        danio = 5.7f;
        bc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BuildingController>();
        items = new List<PassiveItems.Item>();
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		slider_vida.value = vida_actual;
    }

    void OnTriggerEnter2D(Collider2D col) {

        if(col.CompareTag("Item")) {
            String nombre_item = col.name.Remove(col.name.Length - 7);
            System.Type nombre = System.Type.GetType(nombre_item + ",Assembly-CSharp");

            if(gameObject.GetComponent(nombre) == null)
                gameObject.AddComponent(nombre);

            Destroy(col.gameObject);
        } else if (col.CompareTag("exitPoint")) {
            bc.SubirDeNivel();
        } else if ( col.CompareTag("EndBuilding") ) {
            SceneManager.LoadScene(1);
        } else if ( col.CompareTag("PassiveItem") ) {
            PassiveItems.Item temp = passives.getRandomItem();
            

            switch(temp.toBuff) {
                case 0:
                    danio *= temp.multiplier;
                    Debug.Log("new dmg: " + danio);
                    break;
                case 1:
                    resistencia *= temp.multiplier;
                    Debug.Log("new resistance: " + resistencia);
                    break;
                case 2:
                    vida_maxima = (int) (vida_maxima * temp.multiplier);
                    vida_actual = (int) (vida_actual * temp.multiplier);
                    Debug.Log("new max hp: " + vida_maxima);
                    Debug.Log("new current hp: " + vida_actual);
                    break;
                case 3:
                    weaponController.getWeapon().AumentarVelocidadDeAtaque(temp.multiplier);
                    break;
            }

            items.Add(temp);
            Destroy(col.gameObject);
        }
    }

    void OnTriggerStay2D ( Collider2D col ) {
    }

    void OnCollisionEnter2D(Collision2D collision) {

        // Si la colision fue contra un enemigo
        if ( collision.gameObject.CompareTag("Enemy") )
            RecibirDamage(collision.gameObject.GetComponent<Enemy>().damage);
        

        if ( collision.gameObject.CompareTag("JumpPlatform") ) 
            if ( collision.contacts.Length > 0 ) {
                ContactPoint2D contact = collision.contacts[0];
                
                if(Vector3.Dot(contact.normal, Vector3.up) > 0.5) 
                    GetComponent<Rigidbody2D>().AddForce( new Vector2(0f, 125f), ForceMode2D.Impulse );
            }

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

    public void AumentarVida(int plus) {
        vida_maxima += plus;
        slider_vida.maxValue = vida_maxima;
        vida_actual += plus;
    }

    public void AumentarDanio(float plus) {
        danio += plus;
    }


    public void AumentarResistencia(float plus) {
        resistencia += plus;
    }
}
