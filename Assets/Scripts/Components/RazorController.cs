using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class RazorController : MonoBehaviour
{
    public event Action<float> OnRazorMoving;
    public event Action OnRazorFinished;

    public GameObject RazorOnButton, RazorOffButton = null;

    private Slider slide = null;

    private void Awake()
    {
        slide = GetComponent<Slider>();
    }

    public void CheckForCompletion(float value)
    {
        OnRazorMoving?.Invoke(value);
        if (value == slide.maxValue)
        {
            slide.value = slide.minValue;
            OnRazorFinished?.Invoke();
            RazorOnButton.SetActive(true);
            RazorOffButton.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
