using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    bool isGameInProgress = false;
    void HideUi(string name)
    {
        var canvasGroup = GameObject.Find(name).GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    void ShowUi(string name)
    {
        var canvasGroup = GameObject.Find(name).GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public CanvasGroup CanvasGroupUI { get; set; }

    void GameStarted()
    {
        if(isGameInProgress) return;
        isGameInProgress =  true;
        HideUi("GameOver");
        HideUi("WaitingForPlayers");
        HideUi("WaitingToStart");
        ShowUi("GameInProgress");
    }

    void GameOver()
    {
        isGameInProgress = false;
        HideUi("WaitingForPlayers");
        HideUi("WaitingToStart");
        HideUi("GameInProgress");
        ShowUi("GameOver");

        StartCoroutine(TriggerWaitingForPlayers());
    }

    void WaitingToStart()
    {
        if(isGameInProgress) return;
        
        HideUi("GameOver");
        HideUi("GameInProgress");
        HideUi("WaitingForPlayers");
        ShowUi("WaitingToStart");
    
        // StartCoroutine(TriggerGameStarted());
    }

    void WaitingForPlayers()
    {
        // initial game state
        HideUi("GameOver");
        HideUi("GameInProgress");
        HideUi("WaitingToStart");
        ShowUi("WaitingForPlayers");

        //StartCoroutine(TriggerWaitToStart());
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.GameEvents.GameStarted, GameStarted);
        EventManager.StartListening(EventManager.GameEvents.GameOver, GameOver);
        EventManager.StartListening(EventManager.GameEvents.WaitingForPlayers, WaitingForPlayers);
        EventManager.StartListening(EventManager.GameEvents.WaitingToStart, WaitingToStart);

        EventManager.TriggerEvent(EventManager.GameEvents.WaitingForPlayers);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.GameEvents.GameStarted, GameStarted);
        EventManager.StopListening(EventManager.GameEvents.GameOver, GameOver);
        EventManager.StopListening(EventManager.GameEvents.WaitingForPlayers, WaitingForPlayers);
        EventManager.StopListening(EventManager.GameEvents.WaitingToStart, WaitingToStart);
    }

    IEnumerator TriggerWaitingForPlayers()
    {
        yield return new WaitForSeconds(5f);

        EventManager.TriggerEvent(EventManager.GameEvents.WaitingForPlayers);
    }

    // debug help
    IEnumerator TriggerWaitToStart(){
        yield return new WaitForSeconds(1f);

        EventManager.TriggerEvent(EventManager.GameEvents.WaitingToStart);
    }
    
    IEnumerator TriggerGameStarted(){
        yield return new WaitForSeconds(1f);

        EventManager.TriggerEvent(EventManager.GameEvents.GameStarted);
    }
}
