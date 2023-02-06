using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    Transform left, right,me;
    public Collider2D balloonCollider;
    public Rigidbody2D basketbody1;
    public Rigidbody2D basketbody2;
    public Rigidbody2D basketbody3;
    public SpriteRenderer sprite;
    Vector3 pos;
    float speed = 3;
    bool faceLeft = true;
    bool faceUp = true;
    //bool beingAttacked = fase;
    void Start()
    {
        gameObject.SetActive(true);
        me = GameObject.Find("Player").transform;
        //sprite = GetComponent<SpriteRenderer>();
       //balloonCollider = GetComponent<CircleCollider2D>();
       /* left = GameObject.Find("balloonL").transform;
        right = GameObject.Find("balloonR").transform;
        leftX = left.localPosition.x;
        rightX = right.localPosition.x;*/
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveLeftAndRight();
        // // if(Me.attacking && Mathf.Abs(transform.position.x-me.position.x)<= 3 && Mathf.Abs(transform.position.y - me.position.y) <= 3)
        // // {
        // //     gameObject.SetActive(false);
        // // }
        // Debug.Log(transform.position.x);
        moveUpAndDown();
    }

    void moveLeftAndRight()
    {
        pos = transform.position;
        if (faceLeft)
        {
            pos.x -= speed * Time.deltaTime;
            this.transform.position = pos;
            if (transform.position.x <= -60)
            {
                faceLeft = false;
            }
        }
        else
        { 
            pos.x += speed * Time.deltaTime;
            this.transform.position = pos;
            if (transform.position.x > -30)
            {
                faceLeft = true;
            }
        }
    }
    void moveUpAndDown()
    {
        pos = transform.position;
        if (faceUp)
        {
            pos.y -= speed * Time.deltaTime;
            this.transform.position = pos;
            if (transform.position.y <= 9)
            {
                faceUp = false;
            }
        }
        else
        { 
            pos.y += speed * Time.deltaTime;
            this.transform.position = pos;
            if (transform.position.y > 14)
            {
                faceUp= true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "RockPen(Clone)")
        {
        balloonCollider.enabled = false;
       sprite.color = new Color (1, 0, 0, 0); 
       basketbody1.bodyType = RigidbodyType2D.Dynamic;
       basketbody2.bodyType = RigidbodyType2D.Dynamic;
       basketbody3.bodyType = RigidbodyType2D.Dynamic;
       Debug.Log(sprite.color);
        }
        
        
    }
}
