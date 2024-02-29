using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static System.Math;

public class AttachObject : MonoBehaviour
{
    public GameObject attachPoint;
    private XRGrabInteractable interactable;
    private Collider checkCollider;
    private int check = 0;

    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        interactable.selectExited.AddListener(CheckAttach);
    }
    private void CheckAttach(SelectExitEventArgs args)
    {
        if (check > 0 && Abs(attachPoint.transform.position.x - checkCollider.transform.position.x) <= 0.05 && Abs(attachPoint.transform.position.y - checkCollider.transform.position.y) <= 0.05 && Abs(attachPoint.transform.position.z - checkCollider.transform.position.z) <= 0.05) {
            Debug.Log("Inside");
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (attachPoint.name == collider.gameObject.name) {
            if (check == 0)
                checkCollider = collider;
            check++;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (attachPoint.name == collider.gameObject.name) {
            check--;
        }
    }
}
