using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{
    public GameObject objectPrefab;
    public Vector3 gereratePosition = new Vector3(0, -5, 0);
    public float generationDelay;
    
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= generationDelay)
        {
            Instantiate(objectPrefab, gereratePosition, Quaternion.identity);
            timer = 0f;
        }
    }
}
