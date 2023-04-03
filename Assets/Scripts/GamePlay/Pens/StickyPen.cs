using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPen : BasicPen
{
    //public int cornerVertics;
    //public int capVertics;
    public override void InitializePen()
    {
        base.InitializePen();
        base.penName = "StickyPen";
    }

    private FixedJoint2D joint;
    private GameObject otherObject;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mutable Object"))
        {
            otherObject = collision.gameObject;
        }

        if (otherObject != null)
        {
            joint = new FixedJoint2D();

            joint = gameObject.AddComponent<FixedJoint2D>();

            joint.connectedBody = otherObject.GetComponent<Rigidbody2D>();

            joint.breakForce = Mathf.Infinity;
        }
    }

}
