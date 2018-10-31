using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float rangeX, rangeZ;

    public float delayMinimum = 0.1f;
    public float delayMaximum = 1.5f;
	// Use this for initialization
	
	IEnumerator SpawnPumpkin()
    {
        if(gameInProgress)
        {
            yield return new WaitForSeconds(Random.Range(delayMinimum, delayMaximum));

            var position = gameObject.transform.position;

            position.x += Random.Range(rangeX * -1, rangeX);
            position.z += Random.Range(rangeZ * -1, rangeZ);
            
            var rotation = gameObject.transform.rotation;
            //rotation.z = Random.Range(-30f, 30f);

            var pumpkin = string.Empty;
            var randomise = (int)System.Math.Floor(Random.Range(1.0f, 4.0f));

            switch (randomise){
                case 1: 
                    pumpkin = "pumpkin01";
                    break;
                case 2:
                    pumpkin = "pumpkin02";
                    break;
                default:
                    pumpkin = "pumpkin03";
                    break;
            } 

            Instantiate(Resources.Load(pumpkin), position, rotation);

            StartCoroutine(SpawnPumpkin());
        } else 
        {
            yield return null;
        }
    }

    private bool gameInProgress = false;

    void GameStarted(){
        if(gameInProgress) return;
        gameInProgress = true;
        StartCoroutine(SpawnPumpkin());
    }

    void GameOver(){
        gameInProgress = false;
    }

       void OnEnable(){
        EventManager.StartListening(EventManager.GameEvents.GameStarted, GameStarted);
        EventManager.StartListening(EventManager.GameEvents.GameOver, GameOver);
    }

    void OnDisable(){
        EventManager.StopListening(EventManager.GameEvents.GameStarted, GameStarted);
        EventManager.StopListening(EventManager.GameEvents.GameOver, GameOver);
    }
}
