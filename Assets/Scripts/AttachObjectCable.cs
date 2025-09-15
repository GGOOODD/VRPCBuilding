using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody), typeof(XRGrabInteractable))]
public class AttachObjectCable : MonoBehaviour
{
    public GameObject attachPoint;
    public MeshRenderer socket;
    public Material invis;
    public Material correct;
    public Material show;
    private XRGrabInteractable interactable;
    private Rigidbody rb;
    private Collider checkCollider;
    private Vector3 saveAttachScale;
    private int check = 0;
    private int attachCheck = 0;
    private MeshRenderer connectorMeshRend;
    private Transform oldParent;

    [field: SerializeField] public CheckMultipleConnections MultipleConnections { get; set; } = null;
    [field: SerializeField] private UnityEvent OnConnectEvents { get; set; } = null;
    [field: SerializeField] private UnityEvent OnDisconnectEvents { get; set; } = null;

    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        saveAttachScale = attachPoint.transform.localScale;
        interactable.selectExited.AddListener(CheckAttach);
        interactable.selectEntered.AddListener(CheckUnAttach);
        if (MultipleConnections)
        {
            OnConnectEvents.AddListener(MultipleConOnConnect);
            OnDisconnectEvents.AddListener(MultipleConOnDisconnect);
        }
    }

    private void MultipleConOnConnect()
    {
        MultipleConnections.ConnectObject(gameObject);
    }

    private void MultipleConOnDisconnect()
    {
        MultipleConnections.DisconnectObject(gameObject);
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        if (interactable.isSelected){
            if (Vector3.Distance(attachPoint.transform.position, interactable.interactorsSelecting[0].transform.position) > 0.4f)
                interactable.interactionManager.CancelInteractableSelection(interactable);
        }
        if (attachPoint.GetComponent<FixedJoint>() != null && attachPoint.GetComponent<FixedJoint>().connectedBody != null)
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
        if (socket != null)
            socket.material = invis;
        
        if (check == 1)
        {
            oldParent = attachPoint.transform.parent;
            attachPoint.transform.SetParent(checkCollider.gameObject.transform, true);

            attachPoint.transform.localPosition = new Vector3(0f, 0f, 0f);
            attachPoint.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);

            attachPoint.transform.parent = oldParent;
            attachPoint.transform.localScale = saveAttachScale;
            
            attachPoint.AddComponent<FixedJoint>();
            attachPoint.GetComponent<FixedJoint>().connectedBody = checkCollider.GetComponentInParent<Rigidbody>();
            attachPoint.GetComponent<FixedJoint>().enablePreprocessing = false;
            checkCollider.tag = "Unavailable";
            attachCheck = 1;

            OnConnectEvents.Invoke();
        }
    }

    private void CheckUnAttach(SelectEnterEventArgs args)
    {
        if (socket != null)
            socket.material = show;
        
        if (attachCheck == 1 && checkCollider != null)
        {
            checkCollider.tag = attachPoint.tag;
            Destroy(attachPoint.GetComponent<FixedJoint>());
            attachCheck = 0;
            OnDisconnectEvents.Invoke();
        } else if (attachCheck == 1)
        {
            Destroy(attachPoint.GetComponent<FixedJoint>());
            attachCheck = 0;
            OnDisconnectEvents.Invoke();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (attachCheck == 0)
        {
            if (checkCollider != null)
            {
                if (socket != null && socket == connectorMeshRend)
                    socket.material = show;
                else
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
        if (attachCheck == 1)
        {
            connectorMeshRend.material = invis;
        } else if (check == 1)
        {
            connectorMeshRend.material = correct;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (checkCollider != null && attachCheck == 0 && checkCollider.gameObject == collider.gameObject)
        {
            if (socket != null && socket == connectorMeshRend)
                socket.material = show;
            else
                connectorMeshRend.material = invis;
            checkCollider = null;
            connectorMeshRend = null;
            check = 0;
        }
        FixedJoint fixJoint = attachPoint.GetComponent<FixedJoint>();
        if (fixJoint != null && fixJoint.connectedBody == null)
        {
            Destroy(attachPoint.GetComponent<FixedJoint>());
            attachCheck = 0;
            OnDisconnectEvents.Invoke();
            checkCollider = null;
            connectorMeshRend = null;
            check = 0;
        }
    }
}
