using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinKiller : MonoBehaviour
{
    int count = 0;
    void OnCollisionEnter(Collision collision)
    {
        var text = FindObjectOfType<TextMesh>();
        // when collide destroy the pumpkin.
        if (collision.collider.gameObject.name.StartsWith("pumpkin01"))
        {
            var hit = collision.collider.gameObject;
            Instantiate(Resources.Load("splat"), hit.transform.position,Quaternion.identity);
            Destroy(collision.collider.gameObject);
            count++;
            text.text = "Pumpkins: " + count.ToString();
        }
            
    }


}
