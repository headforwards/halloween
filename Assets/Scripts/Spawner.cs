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
        var pumpkinGO = Resources.Load("pumpkin01") as GameObject;
        var pumpkin = Instantiate(pumpkinGO, position, gameObject.transform.rotation);
        StartCoroutine(SpawnPumpkin());
        // wait for 5 before dissolve
        yield return new WaitForSeconds(5);
        //dissolve anim
        StartCoroutine (this.MoveOverSeconds (pumpkin as GameObject, new Vector3 (0.0f, 0f, 1f), 1f));
        // end of translate
        Destroy(pumpkin,3.5f);

        
    }

    //   StartCoroutine (MoveOverSeconds (gameObject, new Vector3 (0.0f, 10f, 0f), 5f));
        public IEnumerator MoveOverSpeed (GameObject objectToMove, Vector3 end, float speed){
            // speed should be 1 unit per second
            while (objectToMove.transform.position != end)
            {
                objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame ();
            }
        }
        public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
        {
            float elapsedTime = 0;
            Vector3 startingPos = objectToMove.transform.position;
            while (elapsedTime < seconds)
            {
                objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToMove.transform.position = end;
        }
}
