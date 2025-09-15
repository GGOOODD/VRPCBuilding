using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.Rendering.DebugUI;

public class MotherboardCommon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; set; } = "";
    [field: SerializeField] public CPUManufacturer SocketManufacturer { get; set; } = CPUManufacturer.NotSelected;
    [field: SerializeField] public CPUSocketType SocketType { get; set; } = CPUSocketType.NotSelected;
    [field: SerializeField] public PCIEType PCIESupport { get; set; } = PCIEType.NotSelected;
    [field: SerializeField] public MemoryType[] DDRSupports { get; set; } = new MemoryType[0];
    [HideInInspector] public int CPUid { get; set; } = -1;
}
