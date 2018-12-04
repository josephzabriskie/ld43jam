using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureCore : MonoBehaviour {
    
    //Holds enemy health and other important values accosiated with all enemies
    private int health;
    protected bool damagebreak = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected int DecrementHealth() {this.health--; return this.health;}

    protected int IncrementHealth() {this.health++; return this.health;}

    protected int GetHealth() { return this.health; }

    protected void SetHealth(int life) { health = life; }

    public abstract void OnHit();

    public abstract void OnKill();

    public bool ProximityCheck(GameObject player, Rigidbody2D rb, float activationDistance)
    {
    
            if (Vector3.Distance(player.transform.position, rb.transform.position) < activationDistance)
            {
                return true;
            }
       

        return false;
    }

    IEnumerator TakeDamage()
    {

        while (damagebreak)
        {

            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.05f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }

    }
}
