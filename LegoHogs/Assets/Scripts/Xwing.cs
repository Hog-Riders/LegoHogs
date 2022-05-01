using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xwing : MonoBehaviour
{
    float timeCounter = 0;

    float speed;
    float width;
    float height;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        width = 16;
       height = 10; 

    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float X = -6;
        float Y = Mathf.Cos(timeCounter) * width;
        float Z = Mathf.Sin(timeCounter) * height;

        transform.position = new Vector3(X, Y, Z);
    }
}
