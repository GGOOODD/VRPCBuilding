using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

public enum ComponentType
{
    NotSelected,
    Cooler,
    CPU,
    GPU,
    RAM,
    Motherboard,
    PowerSupply,
    StorageDevice
}

public enum CPUManufacturer
{
    NotSelected,
    AMD,
    Intel
}

public enum CPUSocketType
{
    NotSelected,
    AM5,
    AM4,
    [InspectorName("AM3+")]
    AM3Plus,
    AM3,
    [InspectorName("LGA 1851")]
    LGA_1851,
    [InspectorName("LGA 1700")]
    LGA_1700,
    [InspectorName("LGA 1200")]
    LGA_1200,
    [InspectorName("LGA 2066")]
    LGA_2066,
    [InspectorName("LGA 1151v2")]
    LGA_1151v2,
    [InspectorName("LGA 1151")]
    LGA_1151,
    [InspectorName("LGA 2011-3")]
    LGA_2011_3,
    [InspectorName("LGA 1150")]
    LGA_1150,
    [InspectorName("LGA 2011")]
    LGA_2011,
    [InspectorName("LGA 1155")]
    LGA_1155,
    [InspectorName("LGA 1156")]
    LGA_1156,
    [InspectorName("LGA 1366")]
    LGA_1366
}

public enum GPUManufacturer
{
    NotSelected,
    Nvidia,
    AMD,
    Intel
}

public enum PCIEType
{
    NotSelected,
    [InspectorName("PCI-E 5.0 x16")]
    PCIE5x16,
    [InspectorName("PCI-E 5.0 x8")]
    PCIE5x8,
    [InspectorName("PCI-E 5.0 x4")]
    PCIE5x4,
    [InspectorName("PCI-E 4.0 x16")]
    PCIE4x16,
    [InspectorName("PCI-E 4.0 x8")]
    PCIE4x8,
    [InspectorName("PCI-E 4.0 x4")]
    PCIE4x4,
    [InspectorName("PCI-E 3.0 x16")]
    PCIE3x16,
    [InspectorName("PCI-E 3.0 x8")]
    PCIE3x8,
    [InspectorName("PCI-E 3.0 x4")]
    PCIE3x4,
}

public enum MemoryType
{
    NotSelected,
    DDR5,
    DDR4,
    DDR3
}

public enum StorageDeviceType
{
    NotSelected,
    HDD,
    SSD
}
// Базовый класс для всех классов компонентов
public class BaseInfo
{
    public Dictionary<string, string> ToDict()
    {
        var dict = new Dictionary<string, string>();
        var properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (var prop in properties) {
            object value = prop.GetValue(this);
            Type valueType = value.GetType();
            string valueString;
            // Если значение является массивом, то делаем каждый элемент строкой и соединяем вместе
            if (valueType.IsArray)
            {
                var items = (value as IEnumerable).Cast<object>().Select(x => x?.ToString() ?? "null");
                valueString = $"[{string.Join(", ", items)}]";
            } else 
            {
                valueString = value.ToString();
            }
            dict[prop.Name] = valueString;
        }
        return dict;
    }
}
public class CoolerInfo : BaseInfo
{
    public CPUSocketType[] SupportSockets { get; } = new CPUSocketType[0];
    public uint TDPLimit { get; } = 0;
    public CoolerInfo(CPUSocketType[] a, uint b)
    {
        SupportSockets = a;
        TDPLimit = b;
    }
}

public class CPUInfo : BaseInfo
{
    public CPUManufacturer CPUManufacturer { get; } = CPUManufacturer.NotSelected;
    public string Model { get; } = "";
    public CPUSocketType SocketType { get; } = CPUSocketType.NotSelected;
    public uint Performance { get; } = 0;
    public CPUInfo(CPUManufacturer a, string b, CPUSocketType c, uint f)
    {
        CPUManufacturer = a;
        Model = b;
        SocketType = c;
        Performance = f;
    }
}

public class GPUInfo : BaseInfo
{
    public GPUManufacturer GPUManufacturer { get; } = GPUManufacturer.NotSelected;
    public string Model { get; } = "";
    public uint MemoryAmountGB { get; } = 0;
    public PCIEType PCIESupport { get; } = PCIEType.NotSelected;
    public uint Performance { get; } = 0;
    public GPUInfo(GPUManufacturer a, string b, uint c, PCIEType d, uint e)
    {
        GPUManufacturer = a;
        Model = b;
        MemoryAmountGB = c;
        PCIESupport = d;
        Performance = e;
    }
}

public class RAMInfo : BaseInfo
{
    public MemoryType DDRType { get; } = MemoryType.NotSelected;
    public uint MemoryAmountGB { get; } = 0;
    public RAMInfo(MemoryType a, uint b)
    {
        DDRType = a;
        MemoryAmountGB = b;
    }
}

public class MotherboardInfo : BaseInfo
{
    public CPUManufacturer CPUManufacturer { get; } = CPUManufacturer.NotSelected;
    public CPUSocketType SocketType { get; } = CPUSocketType.NotSelected;
    public PCIEType PCIESupport { get; } = PCIEType.NotSelected;
    public MemoryType DDRType { get; } = MemoryType.NotSelected;
    public MotherboardInfo(CPUManufacturer a, CPUSocketType b, PCIEType c, MemoryType d)
    {
        CPUManufacturer = a;
        SocketType = b;
        PCIESupport = c;
        DDRType = d;
    }
}

public class PowerSupplyInfo : BaseInfo
{
    public uint PowerSupplyMaxPower { get; } = 0;
    public PowerSupplyInfo(uint a)
    {
        PowerSupplyMaxPower = a;
    }
}

public class StorageDeviceInfo : BaseInfo
{
    public StorageDeviceType StorageDeviceType { get; } = StorageDeviceType.NotSelected;
    public uint MemoryAmountGB { get; } = 0;
    public StorageDeviceInfo(StorageDeviceType a, uint b)
    {
        StorageDeviceType = a;
        MemoryAmountGB = b;
    }
}

public class ItemCommon : MonoBehaviour
{
    // Общая информация
    public string Name = "";
    public ComponentType ComponentType = ComponentType.NotSelected;

    // Характеристики
    public CPUSocketType SocketType = CPUSocketType.NotSelected;
    public CPUSocketType[] SupportSockets = new CPUSocketType[0];
    public StorageDeviceType StorageDeviceType = StorageDeviceType.NotSelected;
    public PCIEType PCIESupport = PCIEType.NotSelected;
    public MemoryType DDRType = MemoryType.NotSelected;
    public GPUManufacturer GPUManufacturer = GPUManufacturer.NotSelected;
    public CPUManufacturer CPUManufacturer = CPUManufacturer.NotSelected;
    public uint TDPLimit = 0;
    public uint Performance = 0;
    public string Model = "";
    public uint MemoryAmountGB  = 0;
    [HideInInspector] public int CPUid = -1;
    public uint PowerSupplyMaxPower = 0;
    
    public BaseInfo GetInfo()
    {
        return this.ComponentType switch
        {
            ComponentType.Cooler => GetCoolerInfo(),
            ComponentType.CPU => GetCPUInfo(),
            ComponentType.GPU => GetGPUInfo(),
            ComponentType.RAM => GetRAMInfo(),
            ComponentType.Motherboard => GetMotherboardInfo(),
            ComponentType.PowerSupply => GetPowerSupplyInfo(),
            ComponentType.StorageDevice => GetStorageDeviceInfo(),
            _ => null,
        };
    }
    public CoolerInfo GetCoolerInfo()
    {
        CoolerInfo data = new(SupportSockets, TDPLimit);
        return data;
    }
    public CPUInfo GetCPUInfo()
    {
        CPUInfo data = new(CPUManufacturer, Model, SocketType, Performance);
        return data;
    }
    public GPUInfo GetGPUInfo()
    {
        GPUInfo data = new(GPUManufacturer, Model, MemoryAmountGB, PCIESupport, Performance);
        return data;
    }
    public RAMInfo GetRAMInfo()
    {
        RAMInfo data = new(DDRType, MemoryAmountGB);
        return data;
    }
    public MotherboardInfo GetMotherboardInfo()
    {
        MotherboardInfo data = new(CPUManufacturer, SocketType, PCIESupport, DDRType);
        return data;
    }
    public PowerSupplyInfo GetPowerSupplyInfo()
    {
        PowerSupplyInfo data = new(PowerSupplyMaxPower);
        return data;
    }
    public StorageDeviceInfo GetStorageDeviceInfo()
    {
        StorageDeviceInfo data = new(StorageDeviceType, MemoryAmountGB);
        return data;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ItemCommon))]
public class ItemCommonEditor : Editor
{
    private SerializedProperty componentType, nameProp;
    private void OnEnable()
    {
        nameProp = serializedObject.FindProperty("Name");
        componentType = serializedObject.FindProperty("ComponentType");
    }

    private void DrawProperty(string propName)
    {
        var prop = serializedObject.FindProperty(propName);
        if (prop != null) EditorGUILayout.PropertyField(prop);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        // показывает ссылку на сам скрипт
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();
        
        EditorGUILayout.PropertyField(nameProp);
        EditorGUILayout.PropertyField(componentType);

        var type = (ComponentType)componentType.enumValueIndex;

        switch (type)
        {
            case ComponentType.NotSelected:
                break;
            
            case ComponentType.Cooler:
                DrawProperty("SupportSockets");
                DrawProperty("TDPLimit");
                break;
                
            case ComponentType.CPU:
                DrawProperty("CPUManufacturer");
                DrawProperty("Model");
                DrawProperty("SocketType");
                DrawProperty("Performance");
                break;

            case ComponentType.GPU:
                DrawProperty("GPUManufacturer");
                DrawProperty("Model");
                DrawProperty("MemoryAmountGB");
                DrawProperty("PCIESupport");
                DrawProperty("Performance");
                break;

            case ComponentType.RAM:
                DrawProperty("DDRType");
                DrawProperty("MemoryAmountGB");
                break;
            
            case ComponentType.Motherboard:
                DrawProperty("CPUManufacturer");
                DrawProperty("SocketType");
                DrawProperty("PCIESupport");
                DrawProperty("DDRType");
                break;
            
            case ComponentType.PowerSupply:
                DrawProperty("PowerSupplyMaxPower");
                break;
            
            case ComponentType.StorageDevice:
                DrawProperty("StorageDeviceType");
                DrawProperty("MemoryAmountGB");
                break;
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
