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
        speed = 1;
        width = 16;
       height = 10; 

    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float X = Mathf.Sin(timeCounter) * height;
        float Y = Mathf.Cos(timeCounter) * width;
        float Z = 6;

        transform.position = new Vector3(X, Y, Z);
    }
}
