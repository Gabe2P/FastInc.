using System;
using System.Collections.Generic;
using UnityEngine;

public class PictureGrader : MonoBehaviour
{
    public event Action<float> OnPictureGraded;

    public float baseReward;
    public AnimationCurve SpeedRewardCurve = AnimationCurve.Linear(0, 0, 1, 1);
    float timer;

    public ScreenShotHandler drawingHandler;
    public ScreenShotHandler outlineHandler;
    Tuple<Camera, Texture2D> drawingPic;
    Tuple<Camera, Texture2D> outlinePic;

    public void Update()
    {
        if (timer < SpeedRewardCurve.keys[SpeedRewardCurve.length - 1].time)
        {
            timer += Time.deltaTime;
        }
        else 
        {
            timer = SpeedRewardCurve.keys[SpeedRewardCurve.length - 1].time;
        }
    }

    private void GradePicture()
    {
        if (drawingPic != null && outlinePic != null)
        {
            float accuracy = TextureComparer.CompareTexture2Ds(drawingPic.Item2, outlinePic.Item2, TextureComparer.CompareFactor.IfAlpha);
            Debug.LogWarning(accuracy);
            OnPictureGraded?.Invoke(Mathf.FloorToInt((baseReward * accuracy) * SpeedRewardCurve.Evaluate(timer)));
            timer = 0;
            drawingPic = null;
            outlinePic = null;
        }
    }

    private void RecordImage(Camera cam, Texture2D texture)
    {
        if (cam == drawingHandler.gameObject.GetComponent<Camera>())
        {
            drawingPic = new Tuple<Camera, Texture2D>(cam, texture);
        }
        if (cam == outlineHandler.gameObject.GetComponent<Camera>())
        {
            outlinePic = new Tuple<Camera, Texture2D>(cam, texture);
        }
        GradePicture();
    }

    private void OnEnable()
    {
        if (drawingHandler != null)
        {
            drawingHandler.OnScreenshotTaken += RecordImage;
        }
        if (outlineHandler != null)
        {
            outlineHandler.OnScreenshotTaken += RecordImage;
        }
    }

    private void OnDisable()
    {
        if (drawingHandler != null)
        {
            drawingHandler.OnScreenshotTaken -= RecordImage;
        }
        if (outlineHandler != null)
        {
            outlineHandler.OnScreenshotTaken -= RecordImage;
        }
    }
}
