using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	DateTime start = DateTime.Now;
	// Use this for initialization
	void Start () {
		
	}
	

private int seconds = 0;
	// Update is called once per frame
	void Update () {


		var current = (int) System.Math.Floor((DateTime.Now - start).TotalSeconds);

		if(seconds != current)
		{
					var timeLeft =  GameObject.Find("gamestate").GetComponent<Text>();

			timeLeft.text = string.Format("Time left: {0} secs",current);

			seconds = current;
		}

	}
}
