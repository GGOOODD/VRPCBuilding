using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

[RequireComponent(typeof(Rigidbody), typeof(XRGrabInteractable))]
public class AttachObjectCable : MonoBehaviour
{
    public GameObject attachPoint;
    public MeshRenderer socket;
    public Material invis;
    public Material correct;
    public Material show;

    private InputActionAsset inputActions;
    private XRInteractionManager interactionManager;
    private InputAction activateActionLeft;
    private InputAction activateActionRight;
    private IXRSelectInteractor interactor;

    private XRGrabInteractable interactable;
    private Collider checkCollider;
    private Vector3 saveAttachScale;
    private bool check = false;
    private bool attachCheck = false;
    private MeshRenderer connectorMeshRend;
    private Transform oldParent;

    [field: SerializeField] public CheckMultipleConnections MultipleConnections { get; set; } = null;
    [field: SerializeField] private UnityEvent OnConnectEvents { get; set; } = null;
    [field: SerializeField] private UnityEvent OnDisconnectEvents { get; set; } = null;

    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        saveAttachScale = attachPoint.transform.localScale;

        interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();

        // Отслеживание нажатия кнопки для подключения и отключения объекта
        inputActions = GameObject.Find("Input Action Manager").GetComponent<InputActionManager>().actionAssets[0];
        if (inputActions != null) {
            activateActionLeft = inputActions.FindActionMap("XRI LeftHand Interaction").FindAction("Activate");
            activateActionRight = inputActions.FindActionMap("XRI RightHand Interaction").FindAction("Activate");
            interactable.selectEntered.AddListener(OnGrabEnter);
            interactable.selectExited.AddListener(OnGrabExit);
        }
        
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
                attachCheck = false;
                connectorMeshRend.material = invis;
                checkCollider = null;
                connectorMeshRend = null;
                check = false;
            }
    }

    private void OnGrabEnter(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject;
        if (args.interactorObject.transform.parent.gameObject.name == "Left Controller") {
            activateActionLeft.performed += TryActivateAction;
        } else {
            activateActionRight.performed += TryActivateAction;
        }
    }

    private void OnGrabExit(SelectExitEventArgs args)
    {
        interactor = null;
        if (args.interactorObject.transform.parent.gameObject.name == "Left Controller") {
            activateActionLeft.performed -= TryActivateAction;
        } else {
            activateActionRight.performed -= TryActivateAction;
        }
    }

    // Активация кнопки подключения/отключения объекта
    private void TryActivateAction(InputAction.CallbackContext context)
    {
        if (attachCheck)
            CheckUnAttach();
        else
            CheckAttach();
    }

    private void CheckAttach()
    {
        if (socket != null)
            socket.material = invis;
        
        if (check)
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
            attachCheck = true;
            if (interactor != null)
                interactionManager.SelectExit(interactor, interactable);

            OnConnectEvents.Invoke();
        }
    }

    private void CheckUnAttach()
    {
        if (socket != null)
            socket.material = show;
        
        if (attachCheck && checkCollider != null)
        {
            checkCollider.tag = attachPoint.tag;
            Destroy(attachPoint.GetComponent<FixedJoint>());
            attachCheck = false;
            OnDisconnectEvents.Invoke();
        } else if (attachCheck)
        {
            Destroy(attachPoint.GetComponent<FixedJoint>());
            attachCheck = false;
            OnDisconnectEvents.Invoke();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!attachCheck)
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
                check = true;
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (attachCheck)
        {
            connectorMeshRend.material = invis;
        } else if (check)
        {
            connectorMeshRend.material = correct;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (checkCollider != null && !attachCheck && checkCollider.gameObject == collider.gameObject)
        {
            if (socket != null && socket == connectorMeshRend)
                socket.material = show;
            else
                connectorMeshRend.material = invis;
            checkCollider = null;
            connectorMeshRend = null;
            check = false;
        }
        FixedJoint fixJoint = attachPoint.GetComponent<FixedJoint>();
        if (fixJoint != null && fixJoint.connectedBody == null)
        {
            Destroy(attachPoint.GetComponent<FixedJoint>());
            attachCheck = false;
            OnDisconnectEvents.Invoke();
            checkCollider = null;
            connectorMeshRend = null;
            check = false;
        }
    }
}
