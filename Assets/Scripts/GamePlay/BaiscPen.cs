using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaiscPen : MonoBehaviour
{
    // Components
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rigidBody;

    // Points
    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointCount = 0;

    // Line properties
    private float pointsMinDistance = 0.1f;
    private float circleColliderRadius;

    // Add a point
    public void AddPoint(Vector2 newPoint)
    {
        if (pointCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
            return;

        points.Add(newPoint);
        ++pointCount;

        // Add circle Collider
        var circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = circleColliderRadius;

        // Line Renderer
        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(pointCount - 1, newPoint);

        // Edge Collider
        if (pointCount > 1)
            edgeCollider.points = points.ToArray();
    }


    // Get the last point drawn
    public Vector2 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointCount - 1);
    }

    // Initialize chosenPen properties
    public virtual void InitializePen(float distance, float width)
    {
        pointsMinDistance = distance;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        circleColliderRadius = width / 2f;
        edgeCollider.edgeRadius = circleColliderRadius;
    }
}
