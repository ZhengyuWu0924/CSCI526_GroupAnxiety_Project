using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectNodeFollowGameObject : MonoBehaviour
{
    public GameObject gameobject;
    private Vector3 originalGameObjectPosition;
    private Vector3 positionOffset;
    // Start is called before the first frame update
    void Start()
    {
        originalGameObjectPosition = gameobject.transform.position;
        positionOffset = originalGameObjectPosition - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = gameobject.transform.position - positionOffset;
    }
}
