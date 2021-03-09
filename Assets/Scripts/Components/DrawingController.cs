using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingController : MonoBehaviour
{
    public Camera cam = null;
    public GameObject brushPrefab = null;
    public bool isClean = true;
    public int inkLength = 20;
    private int inkCounter = 0;
    private Color curColor = Color.white;
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
    }

    private void Draw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (inkCounter >= 0)
            {
                CreateBrush();
            }
        }
        if (Input.GetMouseButton(0))
        {
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
        }
    }

    private void CreateBrush()
    {
        GameObject instance = Instantiate(brushPrefab);
        brushStrokes.Add(instance);
        curRenderer = instance.GetComponent<LineRenderer>();
        curRenderer.startColor = curColor;
        curRenderer.endColor = curColor;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        curRenderer.SetPosition(0, mousePos);
        curRenderer.SetPosition(1, mousePos);
    }

    private void AddPoint(Vector2 position)
    {
        if (inkCounter > 0)
        {
            curRenderer.positionCount++;
            inkCounter--;
            curRenderer.SetPosition(curRenderer.positionCount - 1, position);
        }
    }

    public void ClearStrokes()
    {
        for (int i = 0; i < brushStrokes.Count; i++)
        {
            Destroy(brushStrokes[i]);
        }
        brushStrokes.Clear();
    }

    public void ResetNeedle()
    {
        isClean = true;
    }

    public void ResetInk()
    {
        inkCounter = inkLength;
    }

    public void ResetBrush()
    {
        if (curRenderer != null)
        {
            Destroy(curRenderer.gameObject);
            curRenderer = null;
        }
    }

    private void OnDisable()
    {
        if (curRenderer != null)
        {
            Destroy(curRenderer.gameObject);
            curRenderer = null;
        }
    }
}
