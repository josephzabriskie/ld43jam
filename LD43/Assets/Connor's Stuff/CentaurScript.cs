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

    public override void OnHit() {
        DecrementHealth();
    }

    public override void OnKill() {
        IncrementHealth();
    }
}
