using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static System.Math;

public class AttachObject : MonoBehaviour
{
    public GameObject attachPoint;
    public Material invis;
    public Material correct;
    public Material wrong;
    private XRGrabInteractable interactable;
    private Rigidbody rb;
    private Collider checkCollider;
    private Vector3 current;
    private int check = 0;
    private int attachCheck = 0;
    private MeshRenderer connectorMeshRend;
    private float color;

    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        current = attachPoint.transform.lossyScale;
        interactable.selectExited.AddListener(CheckAttach);
        interactable.selectEntered.AddListener(CheckUnAttach);
    }

    private void CheckAttach(SelectExitEventArgs args)
    {
        if (attachCheck == 1) {
            attachPoint.transform.parent = null;
            attachPoint.transform.localScale = current;
            rb.isKinematic = false;
            attachCheck = 0;
        }
        if (check == 1 && Vector3.Distance(attachPoint.transform.position, checkCollider.gameObject.transform.position) <= 2
            && Quaternion.Angle(attachPoint.transform.rotation, checkCollider.gameObject.transform.rotation) <= 30) {
            Debug.Log("Inside");
            rb.isKinematic = true;
            attachPoint.transform.SetParent(checkCollider.gameObject.transform, true);
            attachPoint.transform.localPosition = new Vector3(0f, 0f, 0f);
            attachPoint.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            attachCheck = 1;
            connectorMeshRend.material = invis;
        }
    }
    private void CheckUnAttach(SelectEnterEventArgs args)
    {
        if (attachCheck == 1) {
            connectorMeshRend.material = correct;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("check?");
        connectorMeshRend = collider.gameObject.GetComponent<MeshRenderer>();
        if (attachPoint.tag == collider.gameObject.tag) {
            checkCollider = collider;
            check = 1;
            connectorMeshRend.material = correct;
        } else
        {
            connectorMeshRend.material = wrong;
        }
    }
    void OnTriggerExit(Collider collider) {
        if (checkCollider.name == collider.gameObject.name)
            check = 0;
        connectorMeshRend = collider.gameObject.GetComponent<MeshRenderer>();
        connectorMeshRend.material = invis;
    }
}
