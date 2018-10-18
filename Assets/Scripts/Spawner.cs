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
        yield return new WaitForSeconds(1.0f);

        var position = gameObject.transform.position;

        position.x += Random.Range(rangeX * -1, rangeX);
        position.z += Random.Range(rangeZ * -1, rangeZ);

        Instantiate(Resources.Load("pumpkin01"), position, gameObject.transform.rotation);

        StartCoroutine(SpawnPumpkin());
    }
}
