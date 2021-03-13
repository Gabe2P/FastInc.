using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICallAudioEvents
{
    public event Action<string> PlaySound;
    public event Action<string> PlayOneShotSound;
    public event Action<string> StopSound;
}
