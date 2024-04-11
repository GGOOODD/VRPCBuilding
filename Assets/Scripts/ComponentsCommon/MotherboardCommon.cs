using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.Rendering.DebugUI;

public class ObjectPair
{
    public GameObject Slot;
    public GameObject Connection;

    public ObjectPair(GameObject slot, GameObject connection)
    {
        this.Slot = slot;
        this.Connection = connection;
    }
}

public class MotherboardCommon : MonoBehaviour
{
    [field: SerializeField] public String SocketType { get; set; } = "";
    [field: SerializeField] public String MemoryType { get; set; } = "";

    [field: SerializeField] private List<GameObject> MemorySlots { get; set; } = new List<GameObject>();
    [field: SerializeField] private GameObject CPUSlot { get; set; } = null;
    [field: SerializeField] private GameObject CoolerSlot { get; set; } = null;
    [field: SerializeField] private GameObject GraphicsSlot { get; set; } = null;

    [field: NonSerialized] public List<ObjectPair> MemoryConnections = new List<ObjectPair>();
    [field: NonSerialized] public ObjectPair CPUConnection;
    [field: NonSerialized] public ObjectPair CoolerConnection;
    [field: NonSerialized] public ObjectPair GraphicsConnection;

    // [field: SerializeField] private GameObject TestObject { get; set; } = null;

    private void Start()
    {
        foreach (var mem in MemorySlots)
        {
            MemoryConnections.Add(new ObjectPair(mem, null));
        }
        CPUConnection = new ObjectPair(CPUSlot, null);
        CoolerConnection = new ObjectPair(CoolerSlot, null);
        GraphicsConnection = new ObjectPair(GraphicsSlot, null);

        // ConnectObject(MemorySlots[0], TestObject);
        // DetachObject(MemorySlots[0], TestObject);
    }

    private ObjectPair GetConnectedPair(GameObject slot)
    {
        int index = MemorySlots.IndexOf(slot);
        if (index != -1)
        {
            return MemoryConnections[index];
        }
        if (CPUConnection.Slot == slot)
        {
            return CPUConnection;
        }
        if (CoolerConnection.Slot == slot)
        {
            return CoolerConnection;
        }
        if (GraphicsConnection.Slot == slot)
        {
            return GraphicsConnection;
        }

        Debug.Log("Error! No such slot found");
        return null;
    }

    private ObjectPair GetConnectedPairByConnection(GameObject connection)
    {
        foreach (var pair in MemoryConnections)
        {
            if (pair.Connection == connection)
            {
                return pair;
            }
        }
        if (CPUConnection.Connection == connection)
        {
            return CPUConnection;
        }
        if (CoolerConnection.Connection == connection)
        {
            return CoolerConnection;
        }
        if (GraphicsConnection.Connection == connection)
        {
            return GraphicsConnection;
        }

        Debug.Log("Error! No such connection found");
        return null;
    }

    public void ConnectObject(GameObject slot, GameObject connection)
    {
        Debug.Log("Trying to connect connection: " + slot + " - " + connection);
        ObjectPair pair = GetConnectedPair(slot);
        if (pair.Connection != null)
        {
            Debug.Log("Error! Another component already connected");
            return;
        }

        pair.Connection = connection;
        // Debug.Log("Set connection: " + pair.Connection);
        // Debug.Log("Real connection: " + GetConnectedPair(slot).Connection);
        Debug.Log("Set connection: " + pair.Slot + " - " + pair.Connection);
        return;
    }

    public void DetachObject(GameObject slot, GameObject connection)
    {
        Debug.Log("Trying to detach connection: " + slot + " - " + connection);
        ObjectPair pair = GetConnectedPair(slot);
        if (pair.Connection == null)
        {
            Debug.Log("Error! No components attached");
            return;
        }

        pair.Connection = null;
        // Debug.Log("Set connection: " + pair.Connection);
        // Debug.Log("Real connection: " + GetConnectedPair(slot).Connection);
        Debug.Log("Set connection: " + pair.Slot + " - " + pair.Connection);
        return;
    }

    public void DetachObject(GameObject connection)
    {
        Debug.Log("Trying to detach object: " + connection);
        ObjectPair pair = GetConnectedPairByConnection(connection);
        if (pair == null)
        {
            Debug.Log("Error! No such object exists");
            return;
        }
        if (pair.Connection == null)
        {
            Debug.Log("Error! No components attached");
            return;
        }

        pair.Connection = null;
        // Debug.Log("Set connection: " + pair.Connection);
        // Debug.Log("Real connection: " + GetConnectedPair(slot).Connection);
        Debug.Log("Set connection: " + pair.Slot + " - " + pair.Connection);
        return;
    }

    public bool IsBuilt()
    {
        foreach (ObjectPair pair in MemoryConnections)
        {
            if (pair.Connection == null)
                return false;
        }
        if (CPUConnection.Connection == null)
            return false;
        if (CoolerConnection.Connection == null)
            return false;
        if (GraphicsConnection.Connection == null)
            return false;
        return true;
    }
}
