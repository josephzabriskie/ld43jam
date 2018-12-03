using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureCore {

	public struct PlayerInput{
        public bool moving; // Is there either horizontal or vertical axis input
        public float angle; //Angle in radians of input (WASD or Joy)
        public bool attack;
        public bool block;
        public PlayerInput(bool m, float ia, bool a, bool b)
        {
            this.moving = m;
            this.angle = ia;
            this.attack = a;
            this.block = b;
        }
    }
	PlayerInput pi;
    private int health;
	public float maxSpeed;
	public bool allowMove;
	Rigidbody2D rb;
    Animator anim;
    Attack1 a1;
    private float startTime;

	void Start () {
		this.rb = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();
        this.a1 = this.GetComponentInChildren<Attack1>();

        SetHealth(4);
	}
	
	void Update () {
		this.pi = getPlayerInput();
        if(!damagebreak) InputProc();
        if (damagebreak)
        {
            if (Time.time - startTime > 0.5f)
            {
                damagebreak = false;
            }
        }
    }

	void FixedUpdate(){
		if(!damagebreak)movementCalc();
        RotatePlayer();
	}

    //Check if we want to turn on our swing check
    void InputProc(){
        if(this.pi.attack && this.a1.Attack()){
            this.anim.SetTrigger("Attack");
        }
        this.anim.SetBool("Blocking", this.pi.block);
    }

    void RotatePlayer(){
        const float rotationOffset = -90.0f;
        Vector3 mouse = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg + rotationOffset;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

	//Update char velocity
	void movementCalc(){
        if (this.pi.moving && this.allowMove){
            //Debug.Log (string.Format("x:{0}, y:{1}",Mathf.Cos (pi.angle), Mathf.Sin (pi.angle)));
            float x_mult = Mathf.Cos(pi.angle);
            x_mult = (Mathf.Abs(x_mult) > 0.001f) ? x_mult : 0;
            float y_mult = Mathf.Sin(pi.angle);
            y_mult = (Mathf.Abs(y_mult) > 0.001f) ? y_mult : 0;
            this.rb.velocity = new Vector2(this.maxSpeed * x_mult, this.maxSpeed * y_mult);
            this.anim.SetBool("Moving", true);
        }
        else { // SLOW DOWN
            this.rb.velocity = new Vector2(0, 0);
            this.anim.SetBool("Moving", false);
        }
    }

	//Input collection
	PlayerInput getPlayerInput()
    {
        PlayerInput ret_pi = new PlayerInput(false, 0, false, false);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        ret_pi.angle = Mathf.Atan2(y, x);
        ret_pi.moving = (!(x == 0 && y == 0)) ? true : false;
        // ret_pi.attack = (Input.GetAxis("Attack") != 0) ? true : false;
        // ret_pi.block = (Input.GetAxis("Block") != 0) ? true : false;
        ret_pi.attack = Input.GetMouseButtonDown(0);
        ret_pi.block = Input.GetMouseButton(1);
        return ret_pi;
    }

    public override void OnHit() {
        if (!damagebreak)
        {
            startTime = Time.time;
            damagebreak = true;
            if (GetHealth() != 0)
            {
                rb.velocity = -rb.velocity * 5;
                DecrementHealth();
                StartCoroutine("TakeDamage");
            }
            else { OnKill(); }
        } }
    public override void OnKill() { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 10) {
            Debug.Log("I'm Hit!");
            OnHit();
            }
        }
    }

