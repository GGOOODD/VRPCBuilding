using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoolerCommon : MonoBehaviour
{
    [field: SerializeField] public String SocketType { get; set; } = "";
    [field: SerializeField] public uint PowerDecrease { get; set; } = 0;
}
