//Written By Gabriel Tupy 3-7-2021

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// The first Arguement is the previous scene and the second is the new scene being changed to.
    /// </summary>
    public static Action<Scene, Scene> OnSceneChange;

    public void ChangeScene(string nextScene)
    {
        OnSceneChange?.Invoke(SceneManager.GetActiveScene(), SceneManager.GetSceneByName(nextScene));
        SceneManager.LoadScene(nextScene);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));
    }
}
