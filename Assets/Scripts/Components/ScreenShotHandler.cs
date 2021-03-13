using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenShotHandler : MonoBehaviour
{
    public event Action<Camera, Texture2D> OnScreenshotTaken;

    public int width = 500;
    public int height = 500;
    public int depth = 16;

    Camera cam;
    bool takePicturesNextFrame = false;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takePicturesNextFrame)
        {
            takePicturesNextFrame = false;
            RenderTexture render = cam.targetTexture;
            Texture2D result = new Texture2D(render.width, render.height, TextureFormat.Alpha8, false); //Come back here for when we wish to check shading/multiple colors.
            Rect rect = new Rect(0, 0, render.width, render.height);
            result.ReadPixels(rect, 0, 0);
            OnScreenshotTaken?.Invoke(cam, result);
        }
    }

    public void TakeScreenShot()
    {
        cam.targetTexture = RenderTexture.GetTemporary(width, height, depth);
        takePicturesNextFrame = true;
    }
}
