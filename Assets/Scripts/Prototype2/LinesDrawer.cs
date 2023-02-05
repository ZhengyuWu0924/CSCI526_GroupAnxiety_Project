using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lines Drawer
/// </summary>
public class LinesDrawer : MonoBehaviour
{
    public GameObject chosenLinePrefab;
    public GameObject[] linePrefabs;
    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;
    private bool isRock = false;
    private bool canDraw = false;

    [Space(30)]
    //public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;

    Line currentLine;
    Camera cam;
    CanvasMove canvasMove;

    private void Start()
    {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
        chosenLinePrefab = linePrefabs[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();
        if (null != currentLine)
            Draw();
        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    // Drawing-----------------------------------------------------------------------

    // Start drawing
    void BeginDraw()
    {
        if (canDraw)
        {
            // Instantiate line prefab
            currentLine = Instantiate(chosenLinePrefab, this.transform).GetComponent<Line>();
            // Set parameters
            currentLine.UsePhysics(false);
            //currentLine.SetLineColor(lineColor);
            currentLine.SetPointsMinDistance(linePointsMinDistance);
            currentLine.SetLineWidth(lineWidth);

        }


    }

    // Drawing the line
    void Draw()
    {
        if (canDraw)
        {
            var pos = cam.ScreenToWorldPoint(Input.mousePosition);
            // Prevent crossing between lines
            RaycastHit2D hit = Physics2D.CircleCast(pos, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);
            if (hit)
                EndDraw();
            else
                currentLine.AddPoint(pos);
        }

    }

    // The end of drawing
    void EndDraw()
    {
        if (canDraw)
        {
            if (null == currentLine) return;
            if (currentLine.pointCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                if (isRock)
                {
                    currentLine.gameObject.layer = LayerMask.NameToLayer("Rock");
                }
                else
                {
                    currentLine.gameObject.layer = cantDrawOverLayerIndex;
                }
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }

    }

    public void SwitchPen(int prefabIndex)
    {
        if (canDraw && chosenLinePrefab == linePrefabs[prefabIndex])
        {
            canDraw = false;
        }
        else
        {
            canDraw = true;
        }
        GameObject DrawingBoard = GameObject.Find("DrawingBoard");
        if(DrawingBoard != null)
        {
            canvasMove = DrawingBoard.GetComponent<CanvasMove>();
            canvasMove.DisableMove();
        }
        chosenLinePrefab = linePrefabs[prefabIndex];
        if(prefabIndex == 4)
        {
            isRock = true;
        }
        else
        {
            isRock = false;
        }
    }

}
