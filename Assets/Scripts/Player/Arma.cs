using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {

    public int tipo;
    public float danio;
    public float velocidad_de_ataque;
    public GameObject ataque;


	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AumentarVelocidadDeAtaque(float plus) {
        // posiblemente esto deba ser un multiplicador pero bueno
        velocidad_de_ataque += plus;
    }
}
