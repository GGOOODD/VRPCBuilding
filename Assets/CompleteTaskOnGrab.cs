using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CompleteTaskOnGrab : MonoBehaviour
{
    public string taskId;
    public TaskManager taskManager;
    private XRGrabInteractable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(TaskComplete);
    }

    private void TaskComplete(SelectEnterEventArgs args)
    {
        taskManager.CompleteTask(taskId);
    }
}
