using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTest : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 direction = new Vector3(0, 0, -1);


    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = gameObject.transform.position + direction * speed * Time.fixedDeltaTime;
    }
}
