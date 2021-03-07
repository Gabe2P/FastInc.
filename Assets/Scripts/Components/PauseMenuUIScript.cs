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
    }


    private void TogglePuaseScreen()
    { 
        
    }
}
