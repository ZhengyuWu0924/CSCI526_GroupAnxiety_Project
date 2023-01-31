using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedRopesDrawer : MonoBehaviour
{
    public GameObject advancedRopePrefab;

    public LayerMask Ground;
    int groundLayerIndex;

    [Space(30)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;

    AdvancedRope advancedRope;
    Camera cam;

    private bool startDraw = false;
    private bool endDraw = false;

    private int drawChances = 8;

    private void Start()
    {
        cam = Camera.main;
        groundLayerIndex = LayerMask.NameToLayer("Ground");
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !startDraw)
        {
            startDraw = true;
            BeginDraw();
        }

        if (startDraw && null != advancedRope)
        {
            Draw();
        }


        if (Input.GetMouseButtonUp(0) && startDraw && !endDraw)
        {
            endDraw = true;
            EndDraw();
        }
    }


    void BeginDraw()
    {
        advancedRope = Instantiate(advancedRopePrefab, this.transform).GetComponent<AdvancedRope>();
        advancedRope.SetLineColor(lineColor);
        advancedRope.SetPointsMinDistance(linePointsMinDistance);
        advancedRope.SetLineWidth(lineWidth);
        advancedRope.SetStartPoint(cam.ScreenToWorldPoint(Input.mousePosition));
    }

    void Draw()
    {
        var pos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(pos, lineWidth / 3f, Vector2.zero, 1f, Ground);
        if (hit)
        {
            EndDraw();
        }
        else
        {
            advancedRope.AddPoint(pos);
        }
    }

    void EndDraw()
    {
        if (null == advancedRope) return;
        if (advancedRope.pointCount < 2)
        {
            Destroy(advancedRope.gameObject);
        }
        else
        {
            advancedRope.SetEndDraw(endDraw);
            advancedRope.gameObject.layer = groundLayerIndex;
            advancedRope = null;
            if (drawChances > 0)
            {
                drawChances--;
                startDraw = false;
                endDraw = false;
            }
        }
    }
}
