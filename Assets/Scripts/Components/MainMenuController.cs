//written by Gabriel Tupy 3-13-2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Update is called once per frame
    void StartGame(string scene)
    {
        SceneChanger.ChangeScene(scene);
    }
}
