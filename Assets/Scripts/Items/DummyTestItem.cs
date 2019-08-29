using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTestItem : MonoBehaviour {

    private Jugador player;

    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>();
    }
	// Use this for initialization
	void Start () {
		player.AumentarVida(750);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
