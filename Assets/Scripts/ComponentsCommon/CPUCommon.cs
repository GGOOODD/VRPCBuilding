using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CPUCommon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; set; } = "";
    [field: SerializeField] public CPUManufacturer Manufacturer { get; set; } = CPUManufacturer.NotSelected;
    [field: SerializeField] public string CPUModel { get; set; } = "";
    [field: SerializeField] public CPUSocketType SocketType { get; set; } = CPUSocketType.NotSelected;
    [field: SerializeField] public PCIEType PCIESupport { get; set; } = PCIEType.NotSelected;
    [field: SerializeField] public MemoryType[] DDRSupports { get; set; } = new MemoryType[0];
    [field: SerializeField] public uint Performance { get; set; } = 0;
}
