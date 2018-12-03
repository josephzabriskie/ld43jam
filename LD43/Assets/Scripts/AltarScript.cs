using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarScript : MonoBehaviour {

	public delegate void SacrificedCallback();
	public SacrificedCallback sc = null;
	public bool acceptingSacrifice = false;
	public bool acceptedSacrifice = false;
	bool filling = false;
	Animator ac;

	void Start () {
		this.ac = GetComponent<Animator>();
	}
	
	void OnCollisionEnter2D(Collision2D col){
		Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
		if(col.gameObject.CompareTag("Player") && this.acceptingSacrifice && !this.filling){
			this.filling = true;
			Debug.Log("Start the fill");
			this.ac.SetTrigger("StartFill");
			StartCoroutine(Sacrifice(2.5f));
			//Play filling sound
		}
	}
	
	IEnumerator Sacrifice(float time){
		float eTime = 0.0f; // elapsed time
		while(eTime < time){
			eTime += Time.deltaTime;
			yield return null;
		}
		this.acceptedSacrifice = true;
		this.sc();
	}
}
