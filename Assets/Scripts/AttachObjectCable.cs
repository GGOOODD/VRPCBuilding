using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachObjectCable : MonoBehaviour
{
    public GameObject attachPoint;
    public Material invis;
    public Material correct;
    private XRGrabInteractable interactable;
    private Rigidbody rb;
    private Collider checkCollider;
    private Vector3 current;
    private int check = 0;
    private int attachCheck = 0;
    private int attachHelp = 0;
    private MeshRenderer connectorMeshRend;
    private Transform oldParent;
    //private Transform newChild;

    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        current = attachPoint.transform.lossyScale;
        interactable.selectExited.AddListener(CheckAttach);
        interactable.selectEntered.AddListener(CheckUnAttach);
        //Debug.Log("...");
    }
    //void FixedUpdate()
    //{
        //if(attachHelp == 1)
        //{
            //attachPoint.transform.localPosition = new Vector3(0f, 0f, 0f);
            //attachPoint.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        //}
    //}

    private void CheckAttach(SelectExitEventArgs args)
    {
        if (attachCheck == 1) {
            //rb.isKinematic = false;
            attachCheck = 0;
        }
        // Vector3.Distance(attachPoint.transform.position, checkCollider.gameObject.transform.position) <= 2
        if (check == 1)
        {
            //rb.isKinematic = true;
            oldParent = attachPoint.transform.parent;
            //newChild = attachPoint.transform;
            //while (newChild.transform.parent != null)
            //    newChild = newChild.transform.parent;
            //Vector3 currentNewChild = newChild.transform.lossyScale;
            attachPoint.transform.parent = null;
            attachPoint.transform.localScale = current;
            //newChild.transform.SetParent(attachPoint.transform, true);
            attachPoint.transform.SetParent(checkCollider.gameObject.transform, true);
            attachPoint.transform.localPosition = new Vector3(0f, 0f, 0f);
            attachPoint.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            //newChild.transform.parent = null;
            //newChild.transform.localScale = currentNewChild;
            attachPoint.transform.parent = oldParent;
            attachPoint.AddComponent<FixedJoint>();
            attachPoint.GetComponent<FixedJoint>().connectedBody = checkCollider.GetComponentInParent<Rigidbody>();
            checkCollider.tag = "Unavailable";
            attachCheck = 1;
            attachHelp = 1;
        }
    }
    private void CheckUnAttach(SelectEnterEventArgs args)
    {
        if (attachHelp == 1)
        {
            checkCollider.tag = attachPoint.tag;
            //attachPoint.transform.parent = null;
            //attachPoint.transform.localScale = current;
            Destroy(attachPoint.GetComponent<FixedJoint>());
            attachHelp = 0;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (attachHelp == 0)
        {
            if (checkCollider != null)
            {
                //Debug.Log("3");
                connectorMeshRend.material = invis;
            }
            if (attachPoint != collider.gameObject && attachPoint.tag == collider.gameObject.tag)
            {
                checkCollider = collider;
                connectorMeshRend = checkCollider.gameObject.GetComponent<MeshRenderer>();
                //connectorMeshRend.material = correct;
                check = 1;
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (check == 1 && attachHelp != 1)
        {
            connectorMeshRend.material = correct;
        } else if (check == 1)
        {
            connectorMeshRend.material = invis;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (checkCollider != null && attachHelp == 0 && checkCollider.gameObject == collider.gameObject)
        {
            //Debug.Log("4");
            connectorMeshRend.material = invis;
            checkCollider = null;
            connectorMeshRend = null;
            check = 0;
        }
    }
}
