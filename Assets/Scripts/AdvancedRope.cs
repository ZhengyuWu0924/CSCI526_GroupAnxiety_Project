using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSeg;

public class AdvancedRope : MonoBehaviour
{
    public Vector3 StartPoint;
    public Vector3 EndPoint;

    public LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen = 0.1f;
    [HideInInspector] public int segmentLength = 0;
    private float lineWidth = 0.1f;

    private bool init = false;
    private bool endDraw = false;

    // line
    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    // [HideInInspector] public List<Vector2> newVerticies = new List<Vector2>();
    [HideInInspector] public int pointCount = 0;
    float pointsMinDistance = 0.1f;
    // EdgeCollider2D edgeCollider;
    RopeColliders ropeColliders;

    // Sling shot
    private bool moveToMouse = false;
    private Vector3 mousePositionWorld;
    private int indexMousePos;

    // Update is called once per frame
    void Update()
    {
        if (!init && !StartPoint.Equals(new Vector3(0,0,0)) && endDraw)
        {
            //edgeCollider = this.gameObject.AddComponent<EdgeCollider2D>();
            //Vector3 ropeStartPoint = StartPoint;
            StartPoint = lineRenderer.GetPosition(0);
            for (int i = 0; i < pointCount; i++)
            {
                this.ropeSegments.Add(new RopeSegment(points[i]));
                // newVerticies.Add(new Vector2(points[2].x, points[2].y));
                // ropeStartPoint.y -= ropeSegLen;
                segmentLength++;
            }
            init = true;
            EndPoint = GetLastPoint();
            // edgeCollider.points = newVerticies.ToArray();

            ropeColliders = this.gameObject.GetComponent<RopeColliders>();
            ropeColliders.SetCreateCollider(true);
        }

        if (init)
        {
            this.DrawRope();
            if (Input.GetMouseButtonDown(1))
            {
                this.moveToMouse = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                this.moveToMouse = false;
            }

            Vector3 screenMousePos = Input.mousePosition;
            float xStart = StartPoint.x;
            float xEnd = EndPoint.x;
            this.mousePositionWorld = Camera.main.ScreenToWorldPoint(new Vector3(screenMousePos.x, screenMousePos.y, 10));
            float currX = this.mousePositionWorld.x;

            float ratio = (currX - xStart) / (xEnd - xStart);

            if (ratio > 0)
            {
                this.indexMousePos = (int)(this.segmentLength * ratio);
            }
        }

    }

    private void FixedUpdate()
    {
        if (init)
        {
            this.Simulate();
        }
    }

    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1f);

        for (int i = 1; i < this.segmentLength; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to First Point 
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = this.StartPoint;
        this.ropeSegments[0] = firstSegment;


        //Constrant to Second Point 
        RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];
        endSegment.posNow = this.EndPoint;
        this.ropeSegments[this.ropeSegments.Count - 1] = endSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];
            // newVerticies[i] = this.ropeSegments[i].posNow;

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }

            if (this.moveToMouse && indexMousePos > 0 && indexMousePos < segmentLength && i == indexMousePos)
            {
                RopeSegment seg1 = this.ropeSegments[i];
                RopeSegment seg2 = this.ropeSegments[i + 1];
                seg1.posNow = new Vector2(this.mousePositionWorld.x, this.mousePositionWorld.y);
                seg2.posNow = new Vector2(this.mousePositionWorld.x, this.mousePositionWorld.y);
                this.ropeSegments[i] = seg1;
                this.ropeSegments[i + 1] = seg2;

            }
        }
        // edgeCollider.points = newVerticies.ToArray();
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
            // newVerticies[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
        // edgeCollider.points = newVerticies.ToArray();
    }


    public void SetStartPoint(Vector3 start)
    {
        StartPoint = start;
    }

    public void SetEndDraw(bool endDraw)
    {
        this.endDraw= endDraw;
    }

    public void AddPoint(Vector2 newPoint)
    {
        if (pointCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
            return;

        points.Add(newPoint);
        ++pointCount;

        // Line Renderer
        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(pointCount - 1, newPoint);
    }

    public Vector2 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointCount - 1);
    }

    public void SetLineColor(Gradient colorGradient)
    {
        lineRenderer.colorGradient = colorGradient;
    }

    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
        ropeSegLen = distance;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}
