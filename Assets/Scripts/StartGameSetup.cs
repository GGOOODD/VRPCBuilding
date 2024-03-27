using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StartGameSetup : MonoBehaviour
{
    [SerializeField] private GameObject table;
    [SerializeField] private List<GameObject> ports;

    private List<GameObject> connections = null;
    private float tableHeight; 

    private void Start()
    {
        tableHeight = table.GetComponent<Renderer>().bounds.size.y;

        int length = ports.Count;
        connections = new List<GameObject>();
        for (int i = 0; i < length; i++) {
            connections.Add(null);
        }
    }

    private GameObject GetConnectedObject(GameObject port) 
    { 
        int index = ports.IndexOf(port);
        if (index == -1)
        {
            Debug.Log("Error! No connected components");
            return null;
        }
        return connections[index];
    }

    public void ConnectObject(GameObject port, GameObject connection)
    {
        if (GetConnectedObject(port) != null)
        {
            Debug.Log("Error! Another component already connected");
            return;
        }

        int index = ports.IndexOf(port);
        connections[index] = connection;

        return;
    }

    public void DetachObject(GameObject port)
    {
        if (GetConnectedObject(port) == null)
        {
            Debug.Log("Error! Nothing to detach");
            return;
        }

        int index = ports.IndexOf(port);
        connections[index] = null;

        return;
    }
}
