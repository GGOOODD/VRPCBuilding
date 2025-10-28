using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Hands.XRHandSubsystemDescriptor;

[RequireComponent(typeof(XRGrabInteractable))]
public class GameTaskInteractable : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public Canvas canvas;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        canvas.enabled = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        canvas.enabled = false;
    }
}
