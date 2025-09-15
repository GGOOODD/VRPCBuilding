using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DetachObject : MonoBehaviour
{
    private XRGrabInteractable interactable;
    private Transform tf;


    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        tf = GetComponent<Transform>();
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        if (interactable.isSelected){
            if (Vector3.Distance(tf.position, interactable.interactorsSelecting[0].transform.position) > 0.4f)
                interactable.interactionManager.CancelInteractableSelection(interactable);
        }
    }
}
