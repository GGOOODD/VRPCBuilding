using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CPUCommon : MonoBehaviour
{
    [field: SerializeField] public String SocketType { get; set; } = "";
    [field: SerializeField] public uint Frequency { get; set; } = 0;
}
