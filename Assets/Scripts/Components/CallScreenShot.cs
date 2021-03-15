using System;
using System.Collections.Generic;
using UnityEngine;

public class CallScreenShot : MonoBehaviour
{
    public static Action OnTakeScreenShot;

    public void CallEvent()
    {
        OnTakeScreenShot?.Invoke();
    }
}
