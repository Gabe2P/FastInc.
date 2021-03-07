//Written By Gabriel Tupy 3-7-2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUIScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;


    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePuaseScreen();
        }

        if (pauseScreen.activeSelf)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }


    private void TogglePuaseScreen()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

    public void ResumeGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeGame();
        }
    }

    public void PauseGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PauseGame();
        }
    }

    public void QuitGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
    }
}
