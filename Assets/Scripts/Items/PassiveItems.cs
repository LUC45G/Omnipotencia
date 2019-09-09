using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour {

    int id = 0;
    public struct Item {
        public int id;
        public int toBuff;
        public float multiplier;

        public Item (int i, int t, float m) {
            id = i; toBuff = t; multiplier = m;
        }
    }
	
    public Item getRandomItem() {
        System.Random r = new System.Random();
        
        int tb = r.Next(4);        

        float m = (float) r.NextDouble() + 1;

        Debug.Log("multiplier: " + m + " applied to: " + tb);
        
        return new Item(id, tb, m);
    }
    
}
