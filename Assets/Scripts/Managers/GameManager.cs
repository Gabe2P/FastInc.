using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private bool canPause;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void OnEnable()
    {
        SceneChanger.OnSceneChange += UpdatePauseFlag;
    }

    private void OnDisable()
    {
        SceneChanger.OnSceneChange -= UpdatePauseFlag;
    }

    private void UpdatePauseFlag(UnityEngine.SceneManagement.Scene previous, UnityEngine.SceneManagement.Scene next)
    {
        if (next.name != "MainMenuScene")
        {
            canPause = true;
        }
        else
        {
            canPause = false;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        if (canPause)
        {
            Time.timeScale = 0;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
