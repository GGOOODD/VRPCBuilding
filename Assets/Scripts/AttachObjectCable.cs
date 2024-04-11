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
    private MeshRenderer connectorMeshRend;
    private Transform oldParent;

    //Debug.Log("...");
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        current = attachPoint.transform.lossyScale;
        interactable.selectExited.AddListener(CheckAttach);
        interactable.selectEntered.AddListener(CheckUnAttach);
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        if (interactable.isSelected){
            //Debug.Log(Vector3.Distance(attachPoint.transform.position, interactable.interactorsSelecting[0].transform.position));
            if (Vector3.Distance(attachPoint.transform.position, interactable.interactorsSelecting[0].transform.position) > 0.4f)
                interactable.interactionManager.CancelInteractableSelection(interactable);
        }
        if (attachPoint.GetComponent<FixedJoint>() != null)
            if (Vector3.Distance(attachPoint.transform.position, attachPoint.GetComponent<FixedJoint>().connectedBody.transform.position) > 0.4f)
            {
                checkCollider.tag = attachPoint.tag;
                Destroy(attachPoint.GetComponent<FixedJoint>());
                attachCheck = 0;
                connectorMeshRend.material = invis;
                checkCollider = null;
                connectorMeshRend = null;
                check = 0;
            }
    }

    private void CheckAttach(SelectExitEventArgs args)
    {
        if (check == 1)
        {
            oldParent = attachPoint.transform.parent;
            attachPoint.transform.parent = null;
            attachPoint.transform.localScale = current;
            attachPoint.transform.SetParent(checkCollider.gameObject.transform, true);
            attachPoint.transform.localPosition = new Vector3(0f, 0f, 0f);
            attachPoint.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            attachPoint.transform.parent = oldParent;
            attachPoint.AddComponent<FixedJoint>();
            attachPoint.GetComponent<FixedJoint>().connectedBody = checkCollider.GetComponentInParent<Rigidbody>();
            attachPoint.GetComponent<FixedJoint>().enablePreprocessing = false;
            checkCollider.tag = "Unavailable";
            attachCheck = 1;
        }
    }

    private void CheckUnAttach(SelectEnterEventArgs args)
    {
        if (attachCheck == 1)
        {
            checkCollider.tag = attachPoint.tag;
            Destroy(attachPoint.GetComponent<FixedJoint>());
            attachCheck = 0;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (attachCheck == 0)
        {
            if (checkCollider != null)
            {
                connectorMeshRend.material = invis;
            }
            if (attachPoint != collider.gameObject && attachPoint.tag == collider.gameObject.tag)
            {
                checkCollider = collider;
                connectorMeshRend = checkCollider.gameObject.GetComponent<MeshRenderer>();
                check = 1;
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (check == 1 && attachCheck != 1)
        {
            connectorMeshRend.material = correct;
        } else if (check == 1)
        {
            connectorMeshRend.material = invis;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (checkCollider != null && attachCheck == 0 && checkCollider.gameObject == collider.gameObject)
        {
            connectorMeshRend.material = invis;
            checkCollider = null;
            connectorMeshRend = null;
            check = 0;
        }
    }
}
