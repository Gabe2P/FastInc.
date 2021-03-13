//Written By Gabriel Tupy 3-10-2021

using System;
using System.Collections.Generic;
using UnityEngine;

public class TextureComparer
{
    public enum CompareFactor { IfAlpha, Alpha, RGB, RGBA}
    private enum Status { None, Passed, Failed}

    public static float CompareTexture2Ds(Texture2D texture1, Texture2D texture2, CompareFactor compareFactor, int offset = 0, int startingPoint = 0)
    {
        Color[] pixels1 = texture1.GetPixels();
        Color[] pixels2 = texture2.GetPixels();

        if (pixels1.Length == pixels2.Length)
        {
            int numberOfChecks = 0;
            int total = 0;
            Status checkStatus = Status.None;
            for (int i = startingPoint; i < pixels1.Length; i++)
            {
                i += offset;
                switch (compareFactor)
                {
                    case CompareFactor.IfAlpha:
                        checkStatus = CompareIfAlpha(pixels1[i], pixels2[i]);
                        break;
                    case CompareFactor.Alpha:
                        checkStatus = CompareAlpha(pixels1[i], pixels2[i]);
                        break;
                    case CompareFactor.RGB:
                        checkStatus = CompareRGB(pixels1[i], pixels2[i]);
                        break;
                    case CompareFactor.RGBA:
                        checkStatus = CompareRGBA(pixels1[i], pixels2[i]);
                        break;
                }
                switch (checkStatus)
                {
                    case Status.None:
                        break;
                    case Status.Passed:
                        total++;
                        numberOfChecks++;
                        break;
                    case Status.Failed:
                        numberOfChecks++;
                        break;
                }
            }
            if (numberOfChecks == 0)
            {
                return 0;
            }
            Debug.Log("Total Number of checks done: " + numberOfChecks);
            return (float)total / numberOfChecks;
        }
        return -1f;
    }

    private static Status CompareIfAlpha(Color pixel1, Color pixel2)
    {
        if (pixel1.a > 0 && pixel2.a > 0)
        {
            Debug.Log("Check Passed = Pixel 1 Alpha: " + pixel1.a + " | Pixel 2 Alpha: " + pixel2.a);
            return Status.Passed;
        }
        if ((pixel1.a == 0 && pixel2.a != 0) || (pixel1.a != 0 && pixel2.a == 0))
        {
            Debug.Log("Check Failed = Pixel 1 Alpha: " + pixel1.a + " | Pixel 2 Alpha: " + pixel2.a);
            return Status.Failed;
        }
        Debug.Log("Pixel 1 Alpha: " + pixel1.a + " | Pixel 2 Alpha: " + pixel2.a);
        return Status.None;
    }

    private static Status CompareAlpha(Color pixel1, Color pixel2)
    {
        if (pixel1.a == pixel2.a)
        {
            Debug.Log("Check Passed = Pixel 1 Alpha: " + pixel1.a + " | Pixel 2 Alpha: " + pixel2.a);
            return Status.Passed;
        }
        Debug.Log("Pixel 1 Alpha: " + pixel1.a + " | Pixel 2 Alpha: " + pixel2.a);
        return Status.Failed;
    }

    private static Status CompareRGB(Color pixel1, Color pixel2)
    {
        if (pixel1.r == pixel2.r && pixel1.g == pixel2.g && pixel1.b == pixel2.b)
        {
            Debug.Log("Check Passed = Pixel 1 RGB: " + pixel1.r + "/" + pixel1.g + "/" + pixel1.b + " | Pixel 2 RGB: " + pixel2.r + "/" + pixel2.g + "/" + pixel2.b);
            return Status.Passed;
        }
        Debug.Log("Check Failed = Pixel 1 RGB: " + pixel1.r + "/" + pixel1.g + "/" + pixel1.b + " | Pixel 2 RGB: " + pixel2.r + "/" + pixel2.g + "/" + pixel2.b);
        return Status.Failed; ;
    }

    private static Status CompareRGBA(Color pixel1, Color pixel2)
    {
        if (pixel1 == pixel2)
        {
            Debug.Log("Check Succeeded = Pixel 1 RGBA: " + pixel1 + " | Pixel 2 RGBA: " + pixel2);
            return Status.Passed;
        }
        Debug.Log("Check Failed = Pixel 1 RGBA: " + pixel1 + " | Pixel 2 RGBA: " + pixel2);
        return Status.Failed;
    }
}
