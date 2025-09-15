using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoolerCommon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; set; } = "";
    [field: SerializeField] public CPUSocketType[] SupportSockets { get; set; } = new CPUSocketType[0];
    [field: SerializeField] public uint TDPLimit { get; set; } = 0;
}
