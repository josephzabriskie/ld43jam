using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public struct PlayerInput{
        public bool moving; // Is there either horizontal or vertical axis input
        public float angle; //Angle in radians of input (WASD or Joy)
        public bool attack;
        public PlayerInput(bool m, float ia, bool a)
        {
            this.moving = m;
            this.angle = ia;
            this.attack = a;
        }
    }
	PlayerInput pi;
    private int health;
	public float maxSpeed;
	public bool allowMove;
	Rigidbody2D rb;
	SpriteRenderer sr;
    Animator anim;

	void Start () {
		this.rb = this.GetComponent<Rigidbody2D>();
		this.sr = this.GetComponent<SpriteRenderer>();
        this.anim = this.GetComponent<Animator>();
	}
	
	void Update () {
		this.pi = getPlayerInput();
        AttackUpdate();
	}

	void FixedUpdate(){
		movementCalc();
        RotatePlayer();
	}

    //Check if we want to turn on our swing check
    void AttackUpdate(){
        if(this.pi.attack){

        }
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
        }
        else { // SLOW DOWN
            this.rb.velocity = new Vector2(0, 0);
        }
    }

	//Input collection
	PlayerInput getPlayerInput()
    {
        PlayerInput ret_pi = new PlayerInput(false, 0, false);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        ret_pi.angle = Mathf.Atan2(y, x);
        ret_pi.moving = (!(x == 0 && y == 0)) ? true : false;
        ret_pi.attack = (Input.GetAxis("Attack") != 0) ? true : false;
        return ret_pi;
    }

    private int DecrementHealth() { this.health--; return this.health; }

    private int IncrementHealth() { this.health++; return this.health; }

    public int GetHealth() { return this.health; }

    public void SetHealth(int life) { health = life; }
}
