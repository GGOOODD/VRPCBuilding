using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateDoor : MonoBehaviour
{
    public Stage stage;
    public InputActionReference test;
    private Animator animator;
    private bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (stage)
            stage.completeTask.AddListener(onCompleteTask);
        if (test)
            test.action.performed += ActivateTest;
    }
    
    private void onCompleteTask()
    {
        if (!open && stage.AreAllTasksCompleted())
        {
            Activate();
            open = true;
        }
    }

    private void ActivateTest(InputAction.CallbackContext context) {
        animator.SetTrigger("Activate");
    }

    public void Activate() {
        animator.SetTrigger("Activate");
    }

    void OnDestroy()
    {
        if (test)
            test.action.performed -= ActivateTest;
    }
}
