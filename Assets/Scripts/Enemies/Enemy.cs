using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public double currentHealth;
    public float resistencia;
    public float damage;
    private Floor level;

	void Awake () {
        currentHealth = 500;
        resistencia = 20;
        damage = 50;
	}   
	
	// Update is called once per frame
	void Update () {
        if ( currentHealth <= 0) {
            level.KillEnemy();
            Destroy(this.gameObject);
        }
	}

    public void RecibirDamage(int dmg) {
        double a = resistencia * 0.01;
        double b = 1 - a;
        currentHealth -= dmg * b;
    }

    void OnCollisionEnter2D(Collision2D collision) {
    }

    public void SetFloor(Floor f) {
        level = f;
    }
}