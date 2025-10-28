using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Hands.XRHandSubsystemDescriptor;

[RequireComponent(typeof(LineRenderer)), RequireComponent(typeof(XRGrabInteractable))]
public class ComponentRaycast : MonoBehaviour
{
    public float raycastDistance = 10f; // The maximum distance the ray will travel
    public LayerMask interactableLayer; // Optional: Filter which layers the ray can hit
    private LineRenderer lineRenderer;
    public Color rayColor = Color.green;

    public GameObject descriptionPanel;
    public TextMeshProUGUI text;
    public TextMeshProUGUI title;

    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;

    public InputActionReference fireReference = null;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = rayColor;
        lineRenderer.endColor = rayColor;
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.005f;
        lineRenderer.enabled = false;

        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }

        fireReference.action.started += FireRay;
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
        isGrabbed = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }


    private void FireRay(InputAction.CallbackContext context)
    {
        if (isGrabbed)
        {
            Vector3 origin = transform.position;
            Vector3 direction = transform.forward;

            RaycastHit[] hits = Physics.RaycastAll(origin, direction, raycastDistance, interactableLayer);
            foreach (var hit in hits)
            {
                ItemCommon itemCommon = hit.collider.GetComponent<ItemCommon>();
                if (itemCommon != null)
                {
                    var info = itemCommon.GetInfo().ToDict();
                    text.SetText("{" + string.Join(", ", info.Select(kv => $"{kv.Key}: {kv.Value}")) + "}");
                    return;
                }
            }

            text.SetText("");
            title.SetText("");
        }
    }

    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (isGrabbed)
        {
            descriptionPanel.SetActive(true);
            lineRenderer.enabled = true;
            Vector3 startPoint = origin;
            Vector3 endPoint = origin + direction * raycastDistance;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
        }
        else
        {
            descriptionPanel.SetActive(false);
            lineRenderer.enabled = false;
        }

    }

}
