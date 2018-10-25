using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float rangeX, rangeZ;
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnPumpkin());
	}
	
	IEnumerator SpawnPumpkin()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));

        var position = gameObject.transform.position;

        position.x += Random.Range(rangeX * -1, rangeX);
        position.z += Random.Range(rangeZ * -1, rangeZ);

        var pumpkin = string.Empty;
        var randomise = (int)System.Math.Floor(Random.Range(1.0f, 3.0f));
        switch (randomise){
            case 1: 
                pumpkin = "pumpkin01";
                break;
            default:
                pumpkin = "pumpkin02";
                break;
        } 
        Instantiate(Resources.Load(pumpkin), position, gameObject.transform.rotation);

        StartCoroutine(SpawnPumpkin());
    }
}
