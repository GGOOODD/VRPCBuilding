using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



public class GPUCommon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; set; } = "";
    [field: SerializeField] public GPUManufacturer Manufacturer { get; set; } = GPUManufacturer.NotSelected;
    [field: SerializeField] public string GPUModel { get; set; } = "";
    [field: SerializeField] public uint VRAMAmountGB { get; set; } = 0;
    [field: SerializeField] public PCIEType PCIEType { get; set; } = PCIEType.NotSelected;
    [field: SerializeField] public uint Performance { get; set; } = 0;
}
