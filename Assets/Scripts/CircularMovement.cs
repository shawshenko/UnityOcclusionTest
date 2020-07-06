using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    float angle = 0;
    float speed = (2 * Mathf.PI) / 10; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    float radius = 0.02f;

    // Update is called once per frame
    
    void Update()
    {
        angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        Vector3 pos = new Vector3();
        pos.z = 0f;
        pos.x = Mathf.Cos(angle) * radius;
        pos.z = Mathf.Sin(angle) * radius;
        gameObject.transform.position = gameObject.transform.position+ pos;
    }
    
}
