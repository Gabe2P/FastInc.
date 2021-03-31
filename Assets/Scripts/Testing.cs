using System;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour, INeedAudio
{
    public event Action OnCallSound;
    public event Action<int, float> OnCall;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OnCallSound?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            OnCall?.Invoke(1, 1f);
        }
    }

    public object GetObject()
    {
        return this;
    }
}