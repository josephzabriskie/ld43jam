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

	void Start () {
		this.rb = this.GetComponent<Rigidbody2D>();
		this.sr = this.GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		this.pi = getPlayerInput();
	}

	void FixedUpdate(){
		movementCalc();
	}

	//Update char velocity
	void movementCalc() {
        if (this.pi.moving && this.allowMove)
        {
            //Debug.Log (string.Format("x:{0}, y:{1}",Mathf.Cos (pi.angle), Mathf.Sin (pi.angle)));
            float x_mult = Mathf.Cos(pi.angle);
            x_mult = (Mathf.Abs(x_mult) > 0.001f) ? x_mult : 0;
            float y_mult = Mathf.Sin(pi.angle);
            y_mult = (Mathf.Abs(y_mult) > 0.001f) ? y_mult : 0;
            this.rb.velocity = new Vector2(this.maxSpeed * x_mult, this.maxSpeed * y_mult);

        }
        else
        { // SLOW DOWN
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
