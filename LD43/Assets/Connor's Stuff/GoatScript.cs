using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatScript : EnemyCore {

	SpriteRenderer sr;

	void Start () {
		this.sr = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hit(){
		this.OnHit();
	}

	protected override void OnHit() {
        DecrementHealth();
		this.sr.color = Color.red;
    }

    protected override void OnKill() {
        Debug.LogError("OnKill not implemented");
    }
}
