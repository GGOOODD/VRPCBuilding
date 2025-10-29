using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineManager : MonoBehaviour
{
    private XRBaseInteractor baseInteractor;
    private XRInteractionManager interactionManager;
    private Outline outline;
    private bool hover = false;
    private bool lineRendererWasDisabled = false;

    void Start()
    {
        baseInteractor = GetComponent<XRBaseInteractor>();
        interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
        baseInteractor.hoverEntered.AddListener(HoverEnter);
        baseInteractor.selectEntered.AddListener(SelectEnter);
        baseInteractor.selectExited.AddListener(SelectExit);
    }

    private void HoverEnter(HoverEnterEventArgs args)
    {
        if (!hover)
        {
            StartCoroutine(UpdateOutline());
            hover = true;
        }
    }

    private IEnumerator UpdateOutline()
    {
        List<IXRInteractable> targets = new();
        interactionManager.GetValidTargets(baseInteractor, targets);
        XRBaseInteractable target = targets[0] as XRBaseInteractable;
        outline = target.transform.GetOrAddComponent<Outline>();
        TryGetComponent(out LineRenderer line);
        if (!target.isSelected)
            outline.enabled = true;
        
        while (true)
        {
            yield return null;
            if (baseInteractor.isSelectActive)
                continue;
            if (baseInteractor is XRRayInteractor)
            {
                if (!line.enabled && !lineRendererWasDisabled)
                {
                    outline.enabled = false;
                    lineRendererWasDisabled = true;
                    continue;
                }
                if (line.enabled && lineRendererWasDisabled)
                {
                    if (!target.isSelected)
                        outline.enabled = true;
                    lineRendererWasDisabled = false;
                    continue;
                }
                if (!line.enabled)
                    continue;
            }
            interactionManager.GetValidTargets(baseInteractor, targets);
            if (targets.Count == 0)
            {
                outline.enabled = false;
                break;
            }
            if (target.transform.gameObject != targets[0].transform.gameObject)
            {
                outline.enabled = false;
                target = targets[0] as XRBaseInteractable;
                outline = target.transform.GetOrAddComponent<Outline>();
            }
            if (!target.isSelected)
                outline.enabled = true;
        }
        hover = false;
    }

    private void SelectEnter(SelectEnterEventArgs args)
    {
        outline.enabled = false;
    }

    private void SelectExit(SelectExitEventArgs args)
    {
        IXRSelectInteractable target = args.interactableObject;
        if (!target.isSelected)
                outline.enabled = true;
    }
}
