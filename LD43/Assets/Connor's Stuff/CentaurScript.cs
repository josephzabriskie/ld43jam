using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentaurScript : EnemyCore {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetHealth();
	}

    protected override void OnHit() {
        DecrementHealth();
    }

    protected override void OnKill() {
        IncrementHealth();
    }
}
