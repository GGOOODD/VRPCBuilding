using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static System.Math;

public class AttachObject : MonoBehaviour
{
    public GameObject attachPoint;
    private XRGrabInteractable interactable;
    private Rigidbody rb;
    private Collider checkCollider;
    private Vector3 current;
    private int check = 0;
    private int attachCheck = 0;

    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        current = attachPoint.transform.lossyScale;
        interactable.selectExited.AddListener(CheckAttach);
    }
    void Update(){
    }
    private void CheckAttach(SelectExitEventArgs args)
    {
        if (attachCheck == 1) {
            attachPoint.transform.parent = null;
            attachPoint.transform.localScale = current;
            rb.isKinematic = false;
            attachCheck = 0;
        }
        if (check == 1 && Vector3.Distance(attachPoint.transform.position, checkCollider.gameObject.transform.position) <= 1
            && Quaternion.Angle(attachPoint.transform.rotation, checkCollider.gameObject.transform.rotation) <= 20) {
            Debug.Log("Inside");
            rb.isKinematic = true;
            attachPoint.transform.SetParent(checkCollider.gameObject.transform, true);
            attachPoint.transform.localPosition = new Vector3(0f, 0f, 0f);
            attachPoint.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            attachCheck = 1;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (attachPoint.tag == collider.gameObject.tag) {
            checkCollider = collider;
            check = 1;
        }
    }
    void OnTriggerExit(Collider collider) {
        if (checkCollider.name == collider.gameObject.name)
            check = 0;
    }
}
