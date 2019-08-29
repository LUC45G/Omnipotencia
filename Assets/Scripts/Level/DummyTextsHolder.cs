using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTextsHolder : MonoBehaviour {

    public GameObject[] texts;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowText(int id) {
        GameObject aux = (GameObject) Instantiate(texts[id-1], this.transform.position, Quaternion.identity);
        aux.transform.SetParent(this.transform);
        Destroy(aux, 1.5f);
    }
}
