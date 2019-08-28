using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

    GameObject[] puertas;
    Transform camara;
    GameObject jugador;
    int nivelActual;
    float height;

    void Awake() {
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
        int i = ++nivelActual;
        puertas = GameObject.FindGameObjectsWithTag("Finish");
        jugador.transform.position = new Vector3 (puertas[i].transform.position.x, puertas[i].transform.position.y, -25);
        camara.position = new Vector3( camara.position.x, camara.position.y + height, camara.position.z);
    }

    public void setHeight( float h ) {
        height = h;
    }
}
