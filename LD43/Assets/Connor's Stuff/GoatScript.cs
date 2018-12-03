using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatScript : CreatureCore {

	SpriteRenderer sr;
	public delegate void PickupCallback(GoatScript gs);
	public PickupCallback pc = null;
	public bool runOnCatch = true;

	void Start () {
		this.sr = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnHit() {
        DecrementHealth();
		this.sr.color = Color.red;
    }

    public override void OnKill() {
        Debug.LogError("OnKill not implemented");
	}
	
	void OnCollisionEnter2D(Collision2D col){
		//Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
		if(col.gameObject.CompareTag("Player")){
			if (this.runOnCatch){
				this.pc(this); // Run specified pickup callback
				Destroy(this.gameObject);
			}
			else{
				this.sr.color = Color.green;
			}
		}
	}
}
