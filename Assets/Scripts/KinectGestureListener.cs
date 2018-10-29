using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    public int playerIndex = 0;

    public void UserDetected(long userId, int userIndex)
    {
        if (userIndex != playerIndex)
            return;
        Time.timeScale = 1;
        // as an example - detect these user specific gestures
        KinectManager manager = KinectManager.Instance;
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.Stop);
        manager.DetectGesture(userId, KinectGestures.Gestures.Wave);

        Debug.Log("Waiting to start");
        EventManager.TriggerEvent(EventManager.GameEvents.WaitingToStart);
    }

    public void UserLost(long userId, int userIndex)
    {
        if (userIndex != playerIndex)
            return;

        Time.timeScale = 0;
    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (userIndex != playerIndex)
            return false;

        switch (gesture)
        {
            case KinectGestures.Gestures.RaiseLeftHand:
            case KinectGestures.Gestures.RaiseRightHand:
                EventManager.TriggerEvent(EventManager.GameEvents.GameStarted);
                break;
            case KinectGestures.Gestures.Stop:
                BroadcastMessage("EndGame");
                break;
        }

        return true;
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
                              float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        // if (userIndex != playerIndex)
        // 	return;

        // if((gesture == KinectGestures.Gestures.ZoomOut || gesture == KinectGestures.Gestures.ZoomIn) && progress > 0.5f)
        // {
        // 	if(gestureInfo != null)
        // 	{
        // 		string sGestureText = string.Format ("{0} - {1:F0}%", gesture, screenPos.z * 100f);
        // 		gestureInfo.text = sGestureText;

        // 		progressDisplayed = true;
        // 		progressGestureTime = Time.realtimeSinceStartup;
        // 	}
        // }
        // else if((gesture == KinectGestures.Gestures.Wheel || gesture == KinectGestures.Gestures.LeanLeft || gesture == KinectGestures.Gestures.LeanRight ||
        // 	gesture == KinectGestures.Gestures.LeanForward || gesture == KinectGestures.Gestures.LeanBack) && progress > 0.5f)
        // {
        // 	if(gestureInfo != null)
        // 	{
        // 		string sGestureText = string.Format ("{0} - {1:F0} degrees", gesture, screenPos.z);
        // 		gestureInfo.text = sGestureText;

        // 		progressDisplayed = true;
        // 		progressGestureTime = Time.realtimeSinceStartup;
        // 	}
        // }
        // else if(gesture == KinectGestures.Gestures.Run && progress > 0.5f)
        // {
        // 	if(gestureInfo != null)
        // 	{
        // 		string sGestureText = string.Format ("{0} - progress: {1:F0}%", gesture, progress * 100);
        // 		gestureInfo.text = sGestureText;

        // 		progressDisplayed = true;
        // 		progressGestureTime = Time.realtimeSinceStartup;
        // 	}
        // }
    }

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint)
    {
        // if (userIndex != playerIndex)
        // 	return false;

        // if(progressDisplayed)
        // {
        // 	progressDisplayed = false;

        // 	if(gestureInfo != null)
        // 	{
        // 		gestureInfo.text = String.Empty;
        // 	}
        // }

        // return true;
        return false;
    }

}