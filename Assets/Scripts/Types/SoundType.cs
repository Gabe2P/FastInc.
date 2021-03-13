//Written By Gabriel Tupy3-8-2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSound", menuName = "Type/Sound")]
public class SoundType : ScriptableObject
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private float priority;
    [SerializeField] private float volume;
    [SerializeField] private float pitch;
    [SerializeField] private bool loop;

    private AudioSource activeSource = null;

    public AudioClip GetAudioClip()
    {
        return clip;
    }

    private void UpdateAudioSource(AudioSource source)
    {
        source.gameObject.name = this.name;
        source.clip = this.clip;
        source.volume = this.volume;
        source.pitch = this.pitch;
        source.loop = this.loop;
        source.spatialBlend = 0;
    }

    public void Play()
    {
        if (activeSource == null)
        {
            AudioSource source = AudioSourceManager.GetInstance().GetAudioSource();
            if (source != null)
            {
                UpdateAudioSource(source);
                source.Play();
                activeSource = source;
            }
        }
    }

    public void PlayOneShot()
    {
        AudioSource source = AudioSourceManager.GetInstance().GetAudioSource();
        if (source != null)
        {
            UpdateAudioSource(source);
            source.PlayOneShot(this.clip);
            activeSource = source;
        }
    }

    public void Stop()
    {
        if (activeSource != null)
        {
            activeSource.Stop();
            AudioSourceManager.GetInstance().ReturnAudioSource(activeSource);
            activeSource = null;
        }
    }
}
