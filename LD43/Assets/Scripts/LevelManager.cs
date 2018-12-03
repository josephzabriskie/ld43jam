using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		lfg, //looking for goat
		findgoat,
		taketoaltar,
		gameover
	}
	//Things the manager controls
	public List<Spawner> enemySpawners;

	//Things the level manager's state depends upon
	LevelState levelState = LevelState.init;
	public List<GoatScript> goats;

	// Use this for initialization
	void Start () {
		this.NextState();
		foreach(var goat in this.goats){ //Tell each goat who to call when caught
			goat.pc = this.GoatCaught;
		}
	}

	void GoatCaught(GoatScript goat){
		Debug.Log("LevelManager:Goat Caught");
		this.goats.Remove(goat);
		StartCoroutine(this.DelayedNext(0.1f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextState(){ //Call to progress by 1 state. Given current state and other inputs, determine next state and do setup for it
		Debug.Log("LevelManager: Moving from state: " + this.levelState.ToString());
		switch(this.levelState){ //We're currently in X. Determine what the next state is and do setup for that state
		case LevelState.init:
			//Here, we'll only ever go to spawn
			//Setup spawn conditions
			this.levelState = LevelState.spawnplayer; // then set state to spawnplayer
			StartCoroutine(this.DelayedNext(0.1f));
			break;
		case LevelState.spawnplayer:
			//Setup for look for goat state
			this.levelState = LevelState.lfg;
			StartCoroutine(this.DelayedNext(0.1f));
			break;
		case LevelState.lfg:
			this.levelState = LevelState.findgoat;
			break;
		case LevelState.findgoat:
			if(this.goats.Count > 1){//Goats will still run
				//We're on the last goat, this one won't run
				this.levelState = LevelState.lfg;
			}
			else if(this.goats.Count == 1){ //We're on the last goat, this one won't run
				this.goats[0].runOnCatch  = false;
				this.levelState = LevelState.lfg;
			}
			else{//All goats gone...
				this.SpawnAll();
				this.levelState = LevelState.taketoaltar;
			}
			break;
		case LevelState.taketoaltar:
			break;
		case LevelState.gameover:
			break;
		default:
			break;
		}
		Debug.Log("LevelManager: Ended in state: " + this.levelState.ToString());
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
