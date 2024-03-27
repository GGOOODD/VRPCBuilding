using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPlayable : MonoBehaviour
{
    public GameObject table;
    public float teleportHeight = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 tablePos = table.transform.position;
        float height = table.GetComponent<Renderer>().bounds.size.y;

        Transform collidedObjTransform = other.gameObject.GetComponent<Transform>();
        collidedObjTransform.position = tablePos + new Vector3(0.0f, teleportHeight + height, 0.0f);
        Rigidbody collidedObjRigid = other.gameObject.GetComponent<Rigidbody>();
        collidedObjRigid.angularVelocity = Vector3.zero;
        // SpeedTest speedTest = other.gameObject.GetComponent<SpeedTest>();
        // speedTest.speed = 0.0f;
    }
}