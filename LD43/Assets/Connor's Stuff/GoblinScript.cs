using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : EnemyCore {
    GameObject player;
    Rigidbody2D rb;
    public float speed = 2f;
    public float rotateAmount = 2f;
    public float rotateSpeed = 2f;
    PolygonCollider2D col;

	// Use this for initialization
	void Start () {
        SetHealth(3);
        player = GameObject.FindGameObjectWithTag("Player");
        this.rb = this.GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {

        EnemyMovement.moveTowardsTarget(player, rb, speed);
        
    }

    
    protected override void OnHit() {

        if (GetHealth() != 0) {
            DecrementHealth();
        }
        else { OnKill(); }
    }

    protected override void OnKill()
    {
    }
}
