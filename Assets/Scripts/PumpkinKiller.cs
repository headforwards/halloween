using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PumpkinKiller : MonoBehaviour
{
    int count = 0;
    void OnCollisionEnter(Collision collision)
    {

        var scoreBoard =  GameObject.Find("score").GetComponent<Text>();

        // when collide destroy the pumpkin.
        if (collision.collider.gameObject.name.StartsWith("pumpkin"))
        {
            var hit = collision.collider.gameObject;
            Instantiate(Resources.Load("splat"), hit.transform.position,Quaternion.identity);
            Destroy(collision.collider.gameObject);
            count++;
            scoreBoard.text = "Pumpkins: " + count.ToString();
        }
            
    }


}
