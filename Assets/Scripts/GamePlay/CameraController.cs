using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public GameObject player;
    private Vector3 moveTemp;

    [SerializeField] float xSpeed = 5f;
    [SerializeField] float ySpeed = 8f;
    [SerializeField] float xDifference;
    [SerializeField] float yDifference;

    [SerializeField] float movementThreshold = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.x > transform.position.x)
        {
            xDifference = player.transform.position.x - transform.position.x;
        } else
        {
            xDifference = transform.position.x - player.transform.position.x;
        }

        if (xDifference >= movementThreshold)
        {
            moveTemp.x = player.transform.position.x;
            moveTemp.z = -10;
            transform.position = Vector3.MoveTowards(transform.position, moveTemp, xSpeed * Time.deltaTime);
        }


        if (player.transform.position.y > transform.position.y)
        {
            yDifference = player.transform.position.y - transform.position.y;
        }
        else
        {
            yDifference = transform.position.y - player.transform.position.y;
        }
        if (yDifference >= movementThreshold)
        {
            moveTemp.y = player.transform.position.y;
            moveTemp.z = -10;
            transform.position = Vector3.MoveTowards(transform.position, moveTemp, ySpeed * Time.deltaTime);
        }

    }

}