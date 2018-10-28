using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgress : MonoBehaviour {

    int squashed = 0;
	void PumpkinSquashed()
    {
        squashed += 1;
        UpdateScore();
    }

    void UpdateScore()
    {
        var score = GameObject.Find("score").GetComponent<Text>();

        score.text = string.Format("Pumpkins: {0}", squashed);
    }

    private void Start()
    {
        UpdateScore();
    }

    void ResetGame()
    {
        squashed = 0;
        UpdateScore();
    }
}
