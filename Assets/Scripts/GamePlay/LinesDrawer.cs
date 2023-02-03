using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesDrawer : MonoBehaviour
{
    public GameObject[] penPrefabs;
    public int chosenPenIndex;
    public float linePointsMinDistance;
    public float lineWidth;

    private GameObject chosenPenPrefab;
    private BaiscPen chosenPen;
    private Camera camera;
    private bool isDrawing;

    private void Start()
    {
        camera = Camera.main;
        chosenPenPrefab = penPrefabs[chosenPenIndex];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeginDraw();
            isDrawing = true;
        }
        if (chosenPen != null && isDrawing)
        {
            Draw();
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndDraw();
            isDrawing = false;
        }
    }

    // Drawing-----------------------------------------------------------------------
    // Start drawing
    void BeginDraw()
    {
        // Instantiate line prefab
        chosenPen = Instantiate(chosenPenPrefab, this.transform).GetComponent<BaiscPen>();
        chosenPen.InitializePen(linePointsMinDistance, lineWidth);
    }

    // Drawing the line
    void Draw()
    {
        var pos = camera.ScreenToWorldPoint(Input.mousePosition);
        chosenPen.AddPoint(pos);
    }

    // The end of drawing
    void EndDraw()
    {
        if (null == chosenPen) return;
        if (chosenPen.pointCount < 2)
        {
            Destroy(chosenPen.gameObject);
        }
        else
        {
            chosenPen = null;
        }
    }
}
