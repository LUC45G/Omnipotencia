using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {

    public int tipo;
    public float danio;
    public float velocidad_de_ataque;
    public List<Attack> ataque;


	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AumentarVelocidadDeAtaque(float plus) {
        // posiblemente esto deba ser un multiplicador pero bueno
        Debug.Log("prev AS: " + velocidad_de_ataque);
        velocidad_de_ataque *= System.Math.Abs(plus-2);
        Debug.Log("new AS: " + velocidad_de_ataque);

    }

    public List<Attack> getAttacks() {
        return ataque;
    }
}
