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

    public static void ChangeScene(string nextScene)
    {
        Scene previous = SceneManager.GetActiveScene();
        SceneManager.LoadScene(nextScene);
        OnSceneChange?.Invoke(previous, SceneManager.GetSceneByBuildIndex(SceneUtility.GetBuildIndexByScenePath(nextScene)));
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));
    }
}
