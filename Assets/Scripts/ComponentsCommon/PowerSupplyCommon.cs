using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PowerSupplyCommon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; set; } = "";
    [field: SerializeField] public uint MaxPower { get; set; } = 0;
}
