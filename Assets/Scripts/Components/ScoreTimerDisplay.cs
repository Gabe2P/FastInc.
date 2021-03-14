using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTimerDisplay : MonoBehaviour
{
    public static Action OnTimerDone;

    public PictureGrader grader = null;

    public AnimationCurve curve = null;
    public float timer = 300f;
    bool countDown = false;

    private void Update()
    {
        if (countDown)
        {
            if (timer == 0)
            {
                OnTimerDone?.Invoke();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void OnEnable()
    {
        if (grader != null)
        {
            grader.OnPictureGraded += UpdateScore;
            grader.OnPictureGraded += UpdateTimer;
        }
    }

    private void OnDisable()
    {
        if (grader != null)
        {
            grader.OnPictureGraded -= UpdateScore;
            grader.OnPictureGraded -= UpdateTimer;
        }
    }

    void UpdateScore(float score)
    { 
        
    }

    void UpdateTimer(float score)
    {
        if (curve != null)
        {
            timer += curve.Evaluate(score);
        }
    }
}

public class UITextDisplayComponent : MonoBehaviour
{ 
    
}
