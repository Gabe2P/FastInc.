//Written By Gabriel Tupy 3-10-2021

using System;
using System.Collections.Generic;
using UnityEngine;

public class TextureComparer
{
    public enum CompareFactor { IfAlpha, Alpha, RGB, RGBA}

    public static float CompareTexture2Ds(Texture2D texture1, Texture2D texture2, CompareFactor compareFactor)
    {
        Color[] pixels1 = texture1.GetPixels();
        Color[] pixels2 = texture2.GetPixels();

        if (pixels1.Length == pixels2.Length)
        {
            int total = 0;
            for (int i = 0; i < pixels1.Length; i++)
            {
                Debug.LogError("Pixel 1: " + pixels1 + "| Pixel 2: " + pixels2);
                switch (compareFactor)
                {
                    case CompareFactor.IfAlpha:
                        total += CompareIfAlpha(pixels1[i], pixels2[i]);
                        break;
                    case CompareFactor.Alpha:
                        total += CompareAlpha(pixels1[i], pixels2[i]);
                        break;
                    case CompareFactor.RGB:
                        total += CompareRGB(pixels1[i], pixels2[i]);
                        break;
                    case CompareFactor.RGBA:
                        total += CompareRGBA(pixels1[i], pixels2[i]);
                        break;
                }
            }
            return (float)total / pixels1.Length;
        }
        return -1f;
    }

    private static int CompareIfAlpha(Color pixel1, Color pixel2)
    {
        if (pixel1.a > 0 && pixel2.a > 0)
        {
            return 1;
        }
        if (pixel1.a == 0 && pixel2.a == 0)
        {
            return 1;
        }
        return 0;
    }

    private static int CompareAlpha(Color pixel1, Color pixel2)
    {
        if (pixel1.a == pixel2.a)
        {
            return 1;
        }
        return 0;
    }

    private static int CompareRGB(Color pixel1, Color pixel2)
    {
        if (pixel1.r > 0 && pixel2.r > 0)
        {
            return 1;
        }
        if (pixel1.a == 0 && pixel2.a == 0)
        {
            return 1;
        }
        return 0;
    }

    private static int CompareRGBA(Color pixel1, Color pixel2)
    {
        if (pixel1 == pixel2)
        {
            return 1;
        }
        return 0;
    }
}
