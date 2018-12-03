using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentaurScript : CreatureCore {
    GameObject player;
    Rigidbody2D rb;
    public float speed = 2f;
    public float rotateAmount = 2f;
    public float rotateSpeed = 2f;
    PolygonCollider2D col;
    private bool damagebreak = false;
    private float startTime;

    Animator anim;
    Attack1 a1;
    // Use this for initialization
    void Start () {
        this.anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        this.a1 = this.GetComponentInChildren<Attack1>();
        this.rb = this.GetComponent<Rigidbody2D>();
        SetHealth(3);
    }
	
	// Update is called once per frame
	void Update () {
        
        EnemyMovement.MoveTowardsTarget(player, rb, speed);
        this.anim.SetTrigger("Run");
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

    public override void OnKill() {
        Destroy(this);
           }

    }
