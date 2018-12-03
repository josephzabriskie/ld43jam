using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject toSpawn;

    public void Spawn(){
        Instantiate(this.toSpawn, this.transform);
    }
}