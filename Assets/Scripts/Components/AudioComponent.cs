//Written By Gabriel Tupy 3-9-2021

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    private ICallAudioEvents reference = null;
    [SerializeField] private List<SoundType> sounds = new List<SoundType>();

    private void Awake()
    {
        reference = GetComponent<ICallAudioEvents>();
    }

    private void OnEnable()
    {
        if (reference != null)
        {
            reference.PlaySound += Play;
            reference.PlayOneShotSound += PlayOneShot;
            reference.StopSound += Stop;
        }
    }

    private void OnDisable()
    {
        if (reference != null)
        {
            reference.PlaySound -= Play;
            reference.PlayOneShotSound -= PlayOneShot;
            reference.StopSound -= Stop;
        }
    }

    public void Play(string sound)
    {
        SoundType s = sounds.Where(x => x.name == sound).FirstOrDefault();
        if (s != null)
        {
            s.Play();
        }
    }

    public void PlayOneShot(string sound)
    {
        SoundType s = sounds.Where(x => x.name == sound).FirstOrDefault();
        if (s != null)
        {
            s.PlayOneShot();
        }
    }

    public void Stop(string sound)
    {
        SoundType s = sounds.Where(x => x.name == sound).FirstOrDefault();
        if (s != null)
        {
            s.Stop();
        }
    }
}
