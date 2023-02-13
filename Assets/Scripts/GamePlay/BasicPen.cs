using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Baisc class of pen
/// All general functions and variables of brush should be in this basic class
/// </summary>
public abstract class BasicPen : MonoBehaviour
{
    // Components
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public EdgeCollider2D edgeCollider2D;
    [HideInInspector] public Rigidbody2D rigidBody2D;
    [SerializeField] public string penName;

    // Points
    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointCount = 0;

    // Line properties (change pen color in LineRenderer Component)
    public float linePointsMinStep;
    public float lineWidth;
    public float massRatio;
    public float massRatioOffset;
    public bool isStraight = false;
    // mass should be propontional to line length
    

    // initialize pen properties before usage
    public virtual void InitializePen()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        edgeCollider2D = gameObject.GetComponent<EdgeCollider2D>();
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();

        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    // Add a point, control how to draw line
    public virtual void AddPoint(Vector2 newPoint)
    {
        if (pointCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < linePointsMinStep)
            return;
     
        points.Add(newPoint);
        ++pointCount;

        // Add circle Collider
        var circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = lineWidth/2.0f;

        // Line Renderer
        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(pointCount - 1, newPoint);

        // Edge Collider
        if (pointCount > 1)
            edgeCollider2D.points = points.ToArray();
    }


    // Get the last point drawn
    public Vector2 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointCount - 1);
    }

    // use physics
    public void UsePhysics(bool usePhysics)
    {
        rigidBody2D.isKinematic = !usePhysics;
    }
}
