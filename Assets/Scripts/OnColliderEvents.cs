using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnColliderEvents : MonoBehaviour
{
    [field: SerializeField] private List<Collider> CollidersToCheck { get; set; } = new List<Collider>();
    [field: SerializeField] private UnityEvent OnConnectEvents { get; set; } = null;
    [field: SerializeField] private UnityEvent OnDisconnectEvents { get; set; } = null;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (CollidersToCheck.Contains(other))
        {
            OnConnectEvents.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (CollidersToCheck.Contains(other))
        {
            OnDisconnectEvents.Invoke();
        }
    }
}
