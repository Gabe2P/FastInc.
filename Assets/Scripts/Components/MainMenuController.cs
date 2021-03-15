//written by Gabriel Tupy 3-13-2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Animator anim = null;

    public void PlayAnimation()
    {
        anim.SetTrigger("Slide");
        Invoke("NextScene", 1f);
    }

    // Update is called once per frame
    public void NextScene(string scene)
    {
        SceneChanger.ChangeScene(scene);
    }
}
