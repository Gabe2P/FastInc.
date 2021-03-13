using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class NeedleRepresentation : MonoBehaviour
{
    public TattooGunController controller = null;
    private Image img = null;
    public float imageScale = .5f;
    public float offset = .05f;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (controller != null)
        {
            controller.OnBrushColorChanged += UpdateColor;
            controller.OnBrushSizeChanged += UpdateSize;
        }
    }

    private void OnDisable()
    {
        if (controller != null)
        {
            controller.OnBrushColorChanged -= UpdateColor;
            controller.OnBrushSizeChanged -= UpdateSize;
        }
    }

    public void UpdateColor(Color newColor)
    {
        img.color = newColor;
    }

    public void UpdateSize(float value)
    {
        this.transform.localScale = Vector3.one * (value * imageScale + offset);
    }
}
