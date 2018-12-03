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
    private bool damagebreak = false;
    private Color Red;
    private Color White;

    private float startTime;

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

        if (damagebreak) {
            if (Time.time - startTime > 2) {
                damagebreak = false;
        }
        }
        if (!damagebreak)
        {
            startTime = Time.time;
            if (GetHealth() != 0)
            {
                DecrementHealth();
                StartCoroutine("Take Damage");
            }
            else { OnKill(); }
        }
    }

    public override void OnKill()
    {

        Destroy(this);
    }

    IEnumerator TakeDamage() {

        while (damagebreak)
        {
            GetComponent<SpriteRenderer>().color = Red;
            yield return new WaitForSeconds(0.3f);
            GetComponent<SpriteRenderer>().color = White;
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player") {
            Hit(); 
        }
    }
}
