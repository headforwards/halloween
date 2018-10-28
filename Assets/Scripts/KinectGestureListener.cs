using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectGestureListener : MonoBehaviour
{
    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    public int playerIndex = 0;

    public void UserDetected(long userId, int userIndex)
    {
        if (userIndex != playerIndex)
            return;

        // as an example - detect these user specific gestures
        KinectManager manager = KinectManager.Instance;
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.Stop);
        manager.DetectGesture(userId, KinectGestures.Gestures.Wave);

        BroadcastMessage("PlayerFound");
    }

    public void UserLost(long userId, int userIndex)
    {
        if (userIndex != playerIndex)
            return;

        BroadcastMessage("PlayerLost");
    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (userIndex != playerIndex)
            return false;

        switch(gesture)
        {
            case KinectGestures.Gestures.RaiseLeftHand:
            case KinectGestures.Gestures.RaiseRightHand:
                BroadcastMessage("NewGame");
                break;
            case KinectGestures.Gestures.Stop:
                BroadcastMessage("EndGame");
                break;
        }

        return true;
    }
}