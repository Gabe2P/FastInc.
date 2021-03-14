using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    private static AudioSourceManager instance = null;
    private bool canAudio = true;
    private Queue<AudioSource> available = new Queue<AudioSource>();
    private List<AudioSource> active = new List<AudioSource>();

    public static AudioSourceManager GetInstance()
    {
        if (instance != null)
        {
            return instance;
        }
        else
        {
            return new GameObject("AudioSourceManager").AddComponent<AudioSourceManager>();
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void OnEnable()
    {
        SceneChanger.OnSceneChange += ResetSelf;
    }

    private void OnDisable()
    {
        SceneChanger.OnSceneChange -= ResetSelf;
    }

    private void Update()
    {
        for (int i = 0; i < active.Count; i++)
        {
            if (active[i] != null)
            {
                if (!active[i].isPlaying && Time.timeScale > 0)
                {
                    available.Enqueue(active[i]);
                    active.RemoveAt(i);
                }
            }
            else
            {
                active.RemoveAt(i);
            }
        }
    }

    public AudioSource GetAudioSource()
    {
        if (canAudio)
        {
            if (available.Count > 0)
            {
                AudioSource source = available.Dequeue();
                active.Add(source);
                return source;
            }
            return CreateAudioSource();
        }
        return null;
    }

    private AudioSource CreateAudioSource()
    {
        AudioSource source = new GameObject("newAudioSource").AddComponent<AudioSource>();
        active.Add(source);
        return source;
    }

    public void ReturnAudioSource(AudioSource source)
    {
        available.Enqueue(source);
    }

    private void ResetSelf(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
    {
        available.Clear();
        active.Clear();
    }
}
