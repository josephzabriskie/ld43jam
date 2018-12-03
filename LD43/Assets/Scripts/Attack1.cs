using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour {
	PolygonCollider2D pc2d;
	bool attackRunning = false;
	public float shotDelay = 0.5f;

	void Start () {
		this.pc2d = GetComponent<PolygonCollider2D>();
		this.pc2d.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	//Attack function
	public bool Attack(){
		if (!this.attackRunning){
			StartCoroutine(AttackIE());
			return true;
		}
		return false;
	}

	IEnumerator AttackIE() {
		const float windupDelay = 0.2f;
		const float attackDuration = 0.3f;
		const float coolDown = 0.0f;
		this.attackRunning = true;
		float eTime = 0.0f; // elapsed time
		while(eTime < windupDelay){
			eTime += Time.deltaTime;
			yield return null;
		}
		eTime = 0.0f;
		this.pc2d.enabled = true;
		this.pc2d.offset = new Vector2(this.pc2d.offset.x, this.pc2d.offset.y + 0.0001f);
		while(eTime < attackDuration){
			eTime += Time.deltaTime;
			yield return null;
		}
		this.pc2d.enabled = false;
		eTime = 0.0f;
		while(eTime < coolDown){
			eTime += Time.deltaTime;
			yield return null;
		}
		this.pc2d.offset = new Vector2(this.pc2d.offset.x, this.pc2d.offset.y - 0.0001f);
		this.attackRunning = false;
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
		EnemyCore ec = col.gameObject.GetComponent<EnemyCore>();
		if (ec != null){
			ec.OnHit();
		}
	}
}
