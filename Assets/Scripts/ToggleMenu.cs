using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleMenu : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    // public InputActionReference moveReference = null;

    private Canvas canvas;

    private void Awake()
    {
        toggleReference.action.started += ToggleOn;
        toggleReference.action.canceled += ToggleOff;

        canvas = GetComponent<Canvas>();
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= ToggleOn;
        toggleReference.action.canceled -= ToggleOff;
    }

    /* private void Update()
    {
        Vector3 value = moveReference.action.ReadValue<Vector3>();
        Move(value);
    } */

    private void ToggleOn(InputAction.CallbackContext context)
    {
        // bool isActive = true;
        // gameObject.SetActive(isActive);
        canvas.enabled = !canvas.enabled;
    }

    private void ToggleOff(InputAction.CallbackContext context)
    {
        // bool isActive = false;
        // gameObject.SetActive(isActive);
        canvas.enabled = !canvas.enabled;
    }

    // private void Move(float value)
    // {
    // }
}
