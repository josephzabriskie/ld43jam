using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCore : MonoBehaviour {
    
    //Holds enemy health and other important values accosiated with all enemies
    private int health;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected int DecrementHealth() {this.health--; return this.health;}

    protected int IncrementHealth() {this.health++; return this.health;}

    protected int GetHealth() { return this.health; }

    protected void SetHealth(int life) { health = life; }

    public abstract void OnHit();

    public abstract void OnKill();
}
