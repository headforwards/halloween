using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {


    public enum GameEvents
    {
        WaitingForPlayers,
        WaitingToStart,
        GameStarted,
        PumpkinSquashed   ,
        GameOver     
    }

    private Dictionary <string, UnityEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
					Debug.Log("Event Manager Created");
                    eventManager.Init (); 
                }
            }

            return eventManager;
        }
    }

    void Init ()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening (GameEvents eventName, UnityAction listener)
    {
		Debug.Log("start listening: "+eventName);
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName.ToString(), out thisEvent))
        {
            thisEvent.AddListener (listener);
        } 
        else
        {
            thisEvent = new UnityEvent ();
            thisEvent.AddListener (listener);
            instance.eventDictionary.Add (eventName.ToString(), thisEvent);
        }
    }

    public static void StopListening (GameEvents eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName.ToString(), out thisEvent))
        {
            thisEvent.RemoveListener (listener);
        }
    }

    public static void TriggerEvent (GameEvents eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName.ToString(), out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}