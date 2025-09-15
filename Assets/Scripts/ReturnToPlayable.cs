using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReturnToPlayable : MonoBehaviour
{
    public GameObject table;
    public float teleportHeight = 1.0f;

    private void OnTriggerEnter(Collider other)
    {

        Vector3 tablePos = table.transform.position;
        float height = table.GetComponent<Renderer>().bounds.size.y;

        Transform parent = other.gameObject.transform;
        while (parent.parent != null) {
            parent = parent.parent;
        }
        GameObject obj = parent.gameObject;
        GrabbableCommon objCommon = obj.GetComponent<GrabbableCommon>();
        if (objCommon == null)
        {
            Debug.Log("Doesn't have grabble common");
        } else if (objCommon.isGrabbed)
        {
            // Debug.Log("Object is grabbed");
            Debug.Log("Object entered, " + objCommon.isGrabbed);
            return;
        }
        Debug.Log("Object entered, is grabbed: " + objCommon.isGrabbed);

        // Transform objTransform = obj.GetComponent<Transform>();
        // objTransform.position = tablePos + new Vector3(0.0f, teleportHeight + height, 0.0f);
        Rigidbody objRigid = obj.GetComponent<Rigidbody>();
        objRigid.position = tablePos + new Vector3(0.0f, teleportHeight + height, 0.0f);
        objRigid.velocity = Vector3.zero;
        // SpeedTest speedTest = other.gameObject.GetComponent<SpeedTest>();
        // speedTest.speed = 0.0f;
    }
}