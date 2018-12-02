using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = GetMousePos();
	}

	Vector3 GetMousePos(){
		Vector3 mousepos = Input.mousePosition;
		mousepos.z = 5.0f;
		Vector3 pos = Camera.main.ScreenToWorldPoint(mousepos);
		return pos;
	}
}
