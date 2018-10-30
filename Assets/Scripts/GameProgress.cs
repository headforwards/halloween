﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class GameProgress : MonoBehaviour {

    int TopScore;

    int squashed = 0;

    const int gameLength = 30;

    DateTime gameStarted = DateTime.Now;

	void PumpkinSquashed()
    {
         if(!gameInProgress)
            return;
        squashed += 1;
        UpdateScore();
    }

    void UpdateScore()
    {
        var score = GameObject.Find("currentScore").GetComponent<Text>();
        score.text = string.Format("Pumpkins: {0}", squashed);

        var finalScore = GameObject.Find("finalScore").GetComponent<Text>();
        finalScore.text = string.Format("You Squashed {0} Pumpkins", squashed);
    }

    void UpdateTimefLeft()
    {
        var timeLeft = GameObject.Find("timeLeft").GetComponent<Text>();
        timeLeft.text = string.Format("Time left: {0} seconds", timeLeftInSeconds);
    }

    private int timeLeftInSeconds = -1;
    // Update is called once per frame
    void Update()
    {
        if(!gameInProgress)
            return;

        var currentCount = gameLength - (int)System.Math.Floor((DateTime.Now - gameStarted).TotalSeconds);

        if (timeLeftInSeconds != currentCount)
        {
            timeLeftInSeconds  = currentCount;
            UpdateTimefLeft();
            if(timeLeftInSeconds <= 0)
                EventManager.TriggerEvent(EventManager.GameEvents.GameOver);
        }
    }

    bool gameInProgress = false;

    void GameStarted()
    {
        if(gameInProgress) return;
        squashed = 0;
        timeLeftInSeconds = gameLength;
        gameStarted = DateTime.Now;
        gameInProgress = true;
        UpdateScore();
        UpdateTimefLeft();
    }

    void GameOver(){
        gameInProgress = false;
        TopScore = Math.Max(TopScore, squashed);
        var scoreboard = GameObject.Find("Scoreboard").GetComponent<Text>();
        scoreboard.text = String.Format("Top squasher's score: {0}", TopScore);
    }

    void OnEnable(){
        EventManager.StartListening(EventManager.GameEvents.PumpkinSquashed, PumpkinSquashed);
        EventManager.StartListening(EventManager.GameEvents.GameStarted, GameStarted);
        EventManager.StartListening(EventManager.GameEvents.GameOver, GameOver);
        var scoreboard = GameObject.Find("Scoreboard").GetComponent<Text>();
        scoreboard.text = String.Empty;
    }

    void OnDisable(){
        EventManager.StopListening(EventManager.GameEvents.PumpkinSquashed, PumpkinSquashed);
        EventManager.StopListening(EventManager.GameEvents.GameStarted, GameStarted);
        EventManager.StopListening(EventManager.GameEvents.GameOver, GameOver);

    }
}
