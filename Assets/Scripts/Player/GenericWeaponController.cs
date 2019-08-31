using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericWeaponController : MonoBehaviour {

    private Vector3 offset;
    protected Arma arma;

	// Use this for initialization
	public virtual void Start () {
		offset = this.transform.position - this.GetComponentInParent<Transform>().position;
        arma = this.GetComponent<Arma>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		Translate();
        Attack();
	}

    protected abstract void Attack();

    void Translate() {

        if ( Input.GetAxis("Vertical") > 0 ) 
            this.transform.position = this.transform.parent.transform.position + Vector3.up;
        else if ( Input.GetAxis("Vertical") < 0 )
            this.transform.position = this.transform.parent.transform.position - Vector3.up;
        else
            this.transform.position += offset;
        

    }

    public Arma getWeapon() {
        return arma;
    }
}
