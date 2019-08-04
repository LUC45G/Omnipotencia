using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

    GameObject[] puertas;
    Transform camara;
    GameObject jugador;
    int nivelActual;
    bool checkDoors = false;
    float height;

    void Awake() {

        puertas = GameObject.FindGameObjectsWithTag("Finish");
        jugador = GameObject.FindGameObjectWithTag("Player");
        camara = GameObject.FindGameObjectWithTag("MainCamera").transform;
        nivelActual = 0;

    }

	// Use this for initialization
	void Start () {
		puertas = GameObject.FindGameObjectsWithTag("Finish");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SubirDeNivel() {
        jugador.transform.position = puertas[++nivelActual].transform.position;
        camara.position = new Vector3( camara.position.x, camara.position.y + height, camara.position.z);
    }

    public void setHeight( float h ) {
        height = h;
    }
}
