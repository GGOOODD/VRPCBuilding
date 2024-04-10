using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MemoryCommon : MonoBehaviour
{
    [field: SerializeField] public String MemoryType { get; set; } = "";
    [field: SerializeField] public uint MemoryAmountGb { get; set; } = 0;
}
