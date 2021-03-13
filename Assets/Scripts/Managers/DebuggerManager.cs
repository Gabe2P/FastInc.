using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerManager : MonoBehaviour
{
    public PictureGrader grader = null;

    private void OnEnable()
    {
        if (grader != null)
        {
            grader.OnPictureGraded += PrintStatement;
        }
    }

    private void OnDisable()
    {
        if (grader != null)
        {
            grader.OnPictureGraded -= PrintStatement;
        }
    }

    private void PrintStatement(float value)
    {
        Debug.Log(value);
    }
}
