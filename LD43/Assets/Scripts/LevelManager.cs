using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

	//Definition: level manager is used to control the flow of a single level
	//Spawn player, give player instructions
	//Loop
	////Player looks round until they find a goat
	////If not the last goat
		//The goat scampers off
		//repeat loop
	////Else
		//Pick up the goat, exit loop
	//Player now has to bring the goat back to the altar
	//gameover

	//Nothing here is timed, all can be done by state?

	public enum LevelState{
		init,
		spawnplayer,
		findgoat,
		taketoaltar,
		gameover
	}
	//Things the manager controls
	public List<Spawner> enemySpawners;
	public TextBox playermsg;
	public FadeScript evileye;
	public GameManager gm;
	public PlayerController pc;

	//Things the level manager's state depends upon
	LevelState levelState = LevelState.init;
	public List<GoatScript> goats;
	public AltarScript altar;

	// Use this for initialization
	void Start () {
		this.NextState();
		foreach(var goat in this.goats){ //Tell each goat who to call when caught
			goat.pc = this.GoatCaught;
		}
		this.altar.sc = this.SacrificeMade;
		this.gm = FindObjectOfType<GameManager>();
		AudioManager.instance.Play("Background_Music");
		this.pc = FindObjectOfType<PlayerController>();
		this.pc.dc = this.PlayerKilled;
	}

	void GoatCaught(GoatScript goat){
		Debug.Log("LevelManager:Goat Caught");
		this.goats.Remove(goat);
		StartCoroutine(this.DelayedNext(0.1f));
	}

	void SacrificeMade(){
		Debug.Log("LevelManager:Sacrifice Made");
		StartCoroutine(this.DelayedNext(0.1f));
	}
	
	void PlayerKilled(){
		Debug.Log("the player's dead");
		this.levelState = LevelState.taketoaltar;
		NextState();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void NextState(){ //Call to progress by 1 state. Given current state and other inputs, determine next state and do setup for it
		Debug.Log("LevelManager: Moving from state: " + this.levelState.ToString());
		switch(this.levelState){ //We're currently in X. Determine what the next state is and do setup for that state
		case LevelState.init:
			AudioManager.instance.Play("Demon_Talk");
			this.levelState = LevelState.spawnplayer; // then set state to spawnplayer
			StartCoroutine(this.DelayedNext(0.1f));
			break;
		case LevelState.spawnplayer:
			//Setup for look for goat state
			this.levelState = LevelState.findgoat;
			this.playermsg.ShowMsgTime("Goats... I need 3 Goats. Bring them to my Altar...", 5.0f);
			this.evileye.Fade(false, 3.0f);
			StartCoroutine(this.DelayedNext(0.1f));
			break;
		case LevelState.findgoat:
			if(this.goats.Count == 3){//No goats found, do nothing
				this.levelState = LevelState.findgoat;
			}
			else if(this.goats.Count > 1){//Goats will still run
				this.playermsg.ShowMsgTime("More... " + this.goats.Count + " more...", 3.0f);
				this.levelState = LevelState.findgoat;
			}
			else if(this.goats.Count == 1){ //We're on the last goat, this one won't run
				this.goats[0].runOnCatch  = false;
				this.playermsg.ShowMsgTime("Last one...", 3.0f);
				this.levelState = LevelState.findgoat;
			}
			else{//All goats gone...
				this.playermsg.ShowMsgTime("Bring the Altar what it is due...", 3.0f);
				this.SpawnAll();
				this.altar.acceptingSacrifice = true;
				this.levelState = LevelState.taketoaltar;
			}
			break;
		case LevelState.taketoaltar:
				AudioManager.instance.Play("Demon_Laugh");
				this.evileye.Fade(true, 4.5f);
				this.levelState = LevelState.gameover;
				StartCoroutine(ToMainMenu());
			break;
		case LevelState.gameover:
				StartCoroutine(ToMainMenu());
			break;
		default:
			break;
		}
		Debug.Log("LevelManager: Ended in state: " + this.levelState.ToString());
	}

	IEnumerator ToMainMenu(){
		Debug.Log("FADING");
		this.gm.FadeToBlack();
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("MainMenu");
		AudioManager.instance.Stop("Background_Music");
	}

	void SpawnAll(){
		foreach(var s in this.enemySpawners){
			s.Spawn();
		}
	}

	IEnumerator DelayedNext(float delay){
		float eTime = 0.0f;
		while(eTime < delay){
			eTime += Time.deltaTime;
			yield return null;
		}
		this.NextState();
	}
	 
}
