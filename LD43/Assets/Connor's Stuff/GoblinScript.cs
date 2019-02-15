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
    Animator anim;
    Collider2D col;
    private bool isAlive = true;
    private bool isActive = false;
	// Use this for initialization
	void Start () {
        SetHealth(1);
        player = GameObject.FindGameObjectWithTag("Player");
        this.rb = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();
        this.col = this.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(ProximityCheck(player.transform.position, rb, 7))
        {
            isActive = true;
        }
        if (!damagebreak && isAlive && isActive)EnemyMovement.MoveTowardsTarget(player, rb, speed);
        if (damagebreak)
        {
            if (Time.time - startTime > 0.5f)
            {
                damagebreak = false;
            }
        }
    }

    public override void OnHit() {
        if (!damagebreak && isAlive)            
        {
            startTime = Time.time;
            damagebreak = true;
            if (GetHealth() != 0)
            {  
                rb.velocity = -rb.velocity * 1;
                DecrementHealth();
                AudioManager.instance.Play("Goblin_Idle");
                StartCoroutine("TakeDamage");
            }
            else { OnKill(); }
        }
    }

    public override void OnKill()
    {
        Debug.Log("This goblin dead as hell!");
        this.anim.SetTrigger("Dead");
        isAlive = false;
        rb.velocity = rb.velocity * 0;
        rb.angularVelocity = 0;
        AudioManager.instance.Play("Goblin_Death");
        col.enabled = false;
        Destroy(this.gameObject,5f);
    }



 
}
