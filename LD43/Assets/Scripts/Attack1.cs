using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour {
	PolygonCollider2D pc2d;
	void Start () {
		this.pc2d = GetComponent<PolygonCollider2D>();
		this.pc2d.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Attack function
}
