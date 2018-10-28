using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PumpkinKiller : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        var pumpkin = collision.collider.gameObject;

        // when collide destroy the pumpkin.
        if (pumpkin.name.StartsWith("pumpkin"))
        {
            Instantiate(Resources.Load("splat"), pumpkin.transform.position,Quaternion.identity);
            Destroy(pumpkin);
            BroadcastMessage("PumpkinSquashed");
        }
    }
}
