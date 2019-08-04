using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

    GameObject[] puertas;
    int nivelActual;
    GameObject jugador;
    bool checkDoors = false;

    void Awake() {

        puertas = GameObject.FindGameObjectsWithTag("Finish");
        nivelActual = 0;
        jugador = GameObject.FindGameObjectWithTag("Player");

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// esto lo hice para que llame cuando ya se genero todo el nivel, despues vere como hacerlo mas clean
        if(!checkDoors) 
            puertas = GameObject.FindGameObjectsWithTag("Finish");
	}

    public void SubirDeNivel() {
        jugador.transform.position = puertas[++nivelActual].transform.position;
    }
}
