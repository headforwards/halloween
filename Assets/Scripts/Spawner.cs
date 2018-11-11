using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Pumpkin Spawn Settings")]

    [Tooltip("Distance along x axis pumpkin will be spawned")]
    public float rangeX;
    [Tooltip("Distance along the Z axis pumpkins will be spawned")]
    public float rangeZ;
    [Space(10)]

    [Tooltip("Minimum delay before spawning a pumpkin")]
    public float delayMinimum = 0.1f;
    [Tooltip("Maximum delay before spawning a pumpkin")]
    public float delayMaximum = 1.5f;

    [Space(20)]
    [Header("Easter Egg")]

    [Tooltip("Number of times player must jump within time limit to trigger the easter egg")]
    public int JumpCountTrigger = 3;
    [Tooltip("Time limit player must complete jump count within")]
    public float JumpTimeLimit = 5.0f;

    [Tooltip("How long the pumpkins will rain for during the easter egg")]
    public float RainFor = 5.0f;
    // Use this for initialization

    IEnumerator SpawnPumpkin()
    {
        if (gameInProgress)
        {
            yield return new WaitForSeconds(Random.Range(delayMinimum, delayMaximum));

            var position = gameObject.transform.position;

            position.x += Random.Range(rangeX * -1, rangeX);
            position.z += Random.Range(rangeZ * -1, rangeZ);

            var rotation = gameObject.transform.rotation;
            // rotation.x = rotation.x += Random.Range(-30f, 30f);

            var pumpkin = string.Empty;
            var randomise = (int)System.Math.Floor(Random.Range(1.0f, 4.0f));

            switch (randomise)
            {
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
        }
        else
        {
            yield return null;
        }
    }

    private bool gameInProgress = false;

    void GameStarted()
    {
        if (gameInProgress) return;
        gameInProgress = true;
        StartCoroutine(SpawnPumpkin());
    }

    void GameOver()
    {
        gameInProgress = false;
    }

    private bool raining = false;
    private int jumpCount = 0;

    void PlayerJumped()
    {
        if (!gameInProgress)
        {
            jumpCount = 0;
            return;
        }

        if (raining)
            return;

        jumpCount++;

        if (jumpCount >= JumpCountTrigger)
        {
            StartCoroutine(StartRaining());
        }
        
        StartCoroutine(ResetJump());
    }

    IEnumerator StartRaining()
    {
        raining = true;

        float min = delayMinimum;
        float max = delayMaximum;

        delayMinimum = 0.001f;
        delayMaximum = 0.002f;

        yield return new WaitForSeconds(RainFor);

        delayMinimum = min;
        delayMaximum = max;
        jumpCount = 0;
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(JumpTimeLimit);

        jumpCount = 0;
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.GameEvents.GameStarted, GameStarted);
        EventManager.StartListening(EventManager.GameEvents.GameOver, GameOver);
        EventManager.StartListening(EventManager.GameEvents.PlayerJumped, PlayerJumped);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.GameEvents.GameStarted, GameStarted);
        EventManager.StopListening(EventManager.GameEvents.GameOver, GameOver);
        EventManager.StopListening(EventManager.GameEvents.PlayerJumped, PlayerJumped);
    }
}
