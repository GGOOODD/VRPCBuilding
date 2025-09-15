using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



public class MemoryCommon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; set; } = "";
    [field: SerializeField] public MemoryType DDRType { get; set; } = MemoryType.NotSelected;
    [field: SerializeField] public uint MemoryAmountGB { get; set; } = 0;
}
