//Written By Gabriel Tupy 3-10-2021

using System;
using System.Collections.Generic;
using UnityEngine;

public class TattooGunController : MonoBehaviour, ICallAudioEvents
{
    public event Action<string> PlaySound;
    public event Action<string> PlayOneShotSound;
    public event Action<string> StopSound;

    public event Action<Color> OnBrushColorChanged;
    public event Action<float> OnBrushSizeChanged;

    public Camera cam = null;
    public GameObject brushPrefab = null;
    public bool isClean = true;
    public LayerMask targetLayer = default(LayerMask);
    public Texture2D cursorImage = null;
    public Texture2D DefaultcursorImage = null;
    public float cursorImageScale = .5f;
    [SerializeField] private Color curColor = Color.black;
    [SerializeField] private float curThickness = .03f;
    private LineRenderer curRenderer = null;
    private Vector2 previousPos = Vector2.zero;

    [SerializeField] private List<GameObject> brushStrokes = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    public void ChangeBrushMaterial(UnityEngine.UI.Button button)
    {
        curColor = button.image.color;
        OnBrushColorChanged?.Invoke(curColor);
    }

    public void ChangeNeedleThickness(float value)
    {
        if (value > 0)
        {
            curThickness = value / 100;
            OnBrushSizeChanged?.Invoke(curThickness);
        }
    }

    private void Draw()
    {
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics2D.Raycast(r.origin, r.direction, Mathf.Infinity, targetLayer);
        if (hit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateBrush();
                PlaySound?.Invoke("TattooGun");
            }
            if (Input.GetMouseButton(0))
            {
                if (curRenderer == null)
                {
                    CreateBrush();
                }
                Vector2 curPos = cam.ScreenToWorldPoint(Input.mousePosition);
                if (curPos != previousPos)
                {
                    AddPoint(curPos);
                    previousPos = curPos;
                }
            }
            else
            {
                curRenderer = null;
                StopSound?.Invoke("TattooGun");
            }
        }
        else
        {
            curRenderer = null;
            StopSound?.Invoke("TattooGun");
        }
    }

    private void CreateBrush()
    {
        GameObject instance = Instantiate(brushPrefab);
        brushStrokes.Add(instance);
        curRenderer = instance.GetComponent<LineRenderer>();
        curRenderer.startColor = curColor;
        curRenderer.endColor = curColor;
        curRenderer.startWidth = curThickness;
        curRenderer.endWidth = curThickness;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        curRenderer.SetPosition(0, mousePos);
        curRenderer.SetPosition(1, mousePos);
    }

    private void AddPoint(Vector2 position)
    {
        if (curRenderer != null)
        {
            curRenderer.positionCount++;
            curRenderer.SetPosition(curRenderer.positionCount - 1, position);
        }
    }

    public void SetCursor()
    {
        Cursor.SetCursor(cursorImage, new Vector2(0, cursorImage.height), CursorMode.ForceSoftware);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(DefaultcursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ClearStrokes()
    {
        for (int i = 0; i < brushStrokes.Count; i++)
        {
            Destroy(brushStrokes[i]);
        }
        brushStrokes.Clear();
    }

    public void SetIsCleanValue(bool value)
    {
        isClean = value;
    }

    public void ResetBrush()
    {
        if (curRenderer != null)
        {
            Destroy(curRenderer.gameObject);
            curRenderer = null;
            StopSound?.Invoke("TattooGun");
        }
    }

    private void OnDisable()
    {
        ResetCursor();
        ResetBrush();
        StopSound?.Invoke("TattooGun");
    }
}
