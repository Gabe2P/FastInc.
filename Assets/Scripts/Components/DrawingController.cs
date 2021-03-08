using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingController : MonoBehaviour
{
    public Camera cam = null;
    public GameObject brushPrefab = null;
    private LineRenderer curRenderer = null;
    private Vector2 previousPos = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
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
        curRenderer = instance.GetComponent<LineRenderer>();

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        curRenderer.SetPosition(0, mousePos);
        curRenderer.SetPosition(1, mousePos);
    }

    private void AddPoint(Vector2 position)
    {
        curRenderer.positionCount++;
        curRenderer.SetPosition(curRenderer.positionCount - 1, position);
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
