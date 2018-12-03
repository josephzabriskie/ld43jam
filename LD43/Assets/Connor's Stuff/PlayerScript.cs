using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {


    private int health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private int DecrementHealth() { this.health--; return this.health; }

    private int IncrementHealth() { this.health++; return this.health; }

    public int GetHealth() { return this.health; }

    public void SetHealth(int life) { health = life; }
}
