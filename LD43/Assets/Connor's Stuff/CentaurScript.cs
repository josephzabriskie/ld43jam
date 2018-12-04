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
    private bool isActive  = false;
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
        StartCoroutine("CentaurRoutine");
    }
	
	// Update is called once per frame
	void Update () {
      
        if (damagebreak)
        {
            if (Time.time - startTime > 0.5f)
            {
                damagebreak = false;
            }
        }
    }

    IEnumerator CentaurRoutine() {
        while (!ProximityCheck(player.transform.position, rb, 5))
            yield return null;

        this.anim.SetBool("Moving", true);
        while (!ProximityCheck(player.transform.position, rb, 4)) {
            EnemyMovement.MoveTowardsTarget(player, rb, speed);
            
            yield return null;
        }
        this.anim.SetBool("Run", true);
        
        Vector3 target = player.transform.position;
        while (!ProximityCheck(player.transform.position, rb, 1f)){
                
                EnemyMovement.Dash(target, rb, speed);
            if (ProximityCheck(target, rb, 0.3f)){
                target = player.transform.position;
                rb.velocity = rb.velocity * 0;
            }
                yield return null;
        }
        if (this.a1.Attack())
        {
            this.anim.SetTrigger("Attack");
            rb.velocity = rb.velocity * 0;
        }
       
        yield return new WaitForSeconds(1);
        this.anim.SetBool("Run", false);
        this.anim.SetBool("Moving", true);
        EnemyMovement.JumpBackwards(player, rb, speed * 2);
        yield return new WaitForSeconds(0.5f);
        rb.velocity = rb.velocity * 0;
        float startStrafe = Time.time;
        while (Time.time - startStrafe > 8) {
            EnemyMovement.Strafe(player, rb, speed, startStrafe);
            yield return null;
        }
        rb.velocity = rb.velocity * 0;
        StartCoroutine("CentaurRoutine");
    }
    public override void OnHit() {

        if (!damagebreak)
        {
            startTime = Time.time;
            damagebreak = true;
            if (GetHealth() != 0)
            {
                rb.velocity = -rb.velocity * 2;
                DecrementHealth();
                StartCoroutine("TakeDamage");
            }
            else { OnKill(); }
        }

    }

    public override void OnKill() {
        Destroy(this.gameObject);
           }

    }
