using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	public GameObject follow;
    public int CameraHeight;
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, CameraHeight);
	}
}
