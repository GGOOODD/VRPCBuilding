using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



public class StorageDeviceCommon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; set; } = "";
    [field: SerializeField] public StorageDeviceType Type { get; set; } = StorageDeviceType.NotSelected;
    [field: SerializeField] public uint CapacityInGB { get; set; } = 0;
}
