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

        EnemyMovement.MoveTowardsTarget(player, rb, speed);
        
    }
    public void Hit() {
        OnHit();
    }
    

    public override void OnHit() {

        if (GetHealth() != 0) {
            DecrementHealth();
        }
        else { OnKill(); }
    }

    public override void OnKill()
    {

        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player") {
            Hit(); 
        }
    }
}
