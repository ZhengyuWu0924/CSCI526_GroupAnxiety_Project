using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playController : MonoBehaviour
{
    public Rigidbody2D rBody;
    public float speed;
    public float jumpforce;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");//浮点型 -1~1;
        float facedirection = Input.GetAxisRaw("Horizontal");//整型 -1 0 1

        //人物移动
        if (horizontalmove != 0)
        {
            rBody.velocity = new Vector2(horizontalmove * speed, rBody.velocity.y);
            //velocity表示沿一定路线运动的速度
        }

        //人物转身
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
            //转身在三维空间实现
        }

        //人物跳跃
        if (Input.GetButtonDown("Jump"))
        {
            rBody.velocity = new Vector2(rBody.velocity.x, jumpforce * Time.deltaTime);
        }
    }
}
