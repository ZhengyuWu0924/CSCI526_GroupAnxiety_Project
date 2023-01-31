using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class RopeColliders : MonoBehaviour
{
    LineRenderer rope;
    EdgeCollider2D edgeCollider;
    Rigidbody2D rigidBody;

    Vector3 points;
    Vector2[] points2;

    AdvancedRope advancedRope;

    private bool init = false;
    private bool CreateCollider = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!init && CreateCollider)
        {
            rigidBody = this.gameObject.AddComponent<Rigidbody2D>();
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;


            edgeCollider = this.gameObject.AddComponent<EdgeCollider2D>();
            edgeCollider.edgeRadius = 0.1f;

            advancedRope = this.gameObject.GetComponent<AdvancedRope>();
            points2 = new Vector2[advancedRope.segmentLength];
            rope = this.gameObject.GetComponent<LineRenderer>();

            getNewPositions();
            edgeCollider.points = points2;
            init = true;
        }

        if (init)
        {
            getNewPositions();
            edgeCollider.points = points2;
            // edgeCollider.offset = new Vector2(-transform.position.x, -transform.position.y);
        }
        
    }

    void getNewPositions()
    {
        for (int i = 0; i < rope.positionCount; i++)
        {
            points = rope.GetPosition(i);
            points2[i] = new Vector2(points.x, points.y);
        }
    }

    public void SetCreateCollider(bool collider)
    {
        CreateCollider = collider;
    }
}
