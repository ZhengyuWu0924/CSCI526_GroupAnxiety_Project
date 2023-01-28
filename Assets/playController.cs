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
        float horizontalmove = Input.GetAxis("Horizontal");//������ -1~1;
        float facedirection = Input.GetAxisRaw("Horizontal");//���� -1 0 1

        //�����ƶ�
        if (horizontalmove != 0)
        {
            rBody.velocity = new Vector2(horizontalmove * speed, rBody.velocity.y);
            //velocity��ʾ��һ��·���˶����ٶ�
        }

        //����ת��
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
            //ת������ά�ռ�ʵ��
        }

        //������Ծ
        if (Input.GetButtonDown("Jump"))
        {
            rBody.velocity = new Vector2(rBody.velocity.x, jumpforce * Time.deltaTime);
        }
    }
}
