using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : CreatureCore {
    GameObject player;
    Rigidbody2D rb;
    public float speed = 2f;
    public float rotateAmount = 2f;
    public float rotateSpeed = 2f;
    private float startTime;

	// Use this for initialization
	void Start () {
        SetHealth(2);
        player = GameObject.FindGameObjectWithTag("Player");
        this.rb = this.GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {

        if(!damagebreak)EnemyMovement.MoveTowardsTarget(player, rb, speed);
        if (damagebreak)
        {
            if (Time.time - startTime > 0.5f)
            {
                damagebreak = false;
            }
        }
    }

    public override void OnHit() {
        
        if (!damagebreak)            
        {
            startTime = Time.time;
            damagebreak = true;
            if (GetHealth() != 0)
            {  
                rb.velocity = -rb.velocity * 1;
                DecrementHealth();
                StartCoroutine("TakeDamage");
            }
            else { OnKill(); }
        }
    }

    public override void OnKill()
    {
        Debug.Log("This goblin dead as hell!");
        Destroy(this.gameObject);
    }



 
}
