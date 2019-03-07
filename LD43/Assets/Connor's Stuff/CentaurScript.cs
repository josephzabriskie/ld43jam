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
    public GameObject nodes;
    public int nodeIndex;

    Animator anim;
    CentaurAttack a1;
    // Use this for initialization
    void Start () {
        this.anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        this.a1 = this.GetComponentInChildren<CentaurAttack>();
        this.rb = this.GetComponent<Rigidbody2D>();
        SetHealth(2);
        
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
        if(ProximityCheck(player.transform.position, rb, 7))
        {
            StartCoroutine("CentaurRoutine");
        }
    }

    IEnumerator CentaurRoutine() {
        while (!ProximityCheck(player.transform.position, rb, 7))
            yield return new WaitForFixedUpdate();

         this.anim.SetBool("Run", true);
        /* while (!ProximityCheck(player.transform.position, rb, 5)) {
             EnemyMovement.MoveTowardsTarget(player, rb, speed);

             yield return new WaitForFixedUpdate();
         }
         this.anim.SetBool("Run", true);

         Vector3 target = player.transform.position;
         while (!ProximityCheck(player.transform.position, rb, 4f)){

                 EnemyMovement.Dash(target, rb, speed);
             if (ProximityCheck(target, rb, 4f)){
                 target = player.transform.position;              
             }
                 yield return new WaitForFixedUpdate();
         }
         if (this.a1.Attack())
         {
             this.anim.SetTrigger("Attack");
             //rb.AddRelativeForce(new Vector2(;
         }

         yield return new WaitForSeconds(2);
         rb.velocity = rb.velocity * 0;
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
         rb.velocity = rb.velocity * 0;*/
        LocateClosestNode();
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 100; i++)
        {

            //Debug.Log(nodeIndex);
            while (!ProximityCheck(GetCurrentNode(nodeIndex).position, this.rb, .5f))
            {
                EnemyMovement.MoveTowardsTarget(GetCurrentNode(nodeIndex).gameObject, rb, speed * 3);
                yield return new WaitForSeconds(.5f);
            }
            nodeIndex++;
        }

        StartCoroutine("CentaurRoutine");
    }
    public override void OnHit() {
        rb.freezeRotation = true;
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

    Transform GetCurrentNode(int index)
    {
        
        if(index == nodes.transform.childCount)
        {
            nodeIndex = 0;
            index = 0;
        }
        return nodes.transform.GetChild(index);
    }

    void LocateClosestNode()
    {
        int closestIndex = 0;
         float distance = 0;
         float maxDistance = 10;
        // this.nodeIndex = 0;
         while(nodeIndex != nodes.transform.childCount)
         {
             distance = Vector3.Distance(this.transform.position, nodes.transform.GetChild(nodeIndex).position);
             if(distance < maxDistance)
             {
                 maxDistance = distance;
                 closestIndex = nodeIndex;
             }
             nodeIndex++;
         }

         //nodeIndex = closestIndex;
    }
}

