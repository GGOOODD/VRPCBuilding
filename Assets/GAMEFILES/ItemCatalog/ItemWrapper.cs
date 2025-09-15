﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.Rendering.DebugUI;

public class ItemWrapper : MonoBehaviour
{
    public UnityEngine.UI.Button button;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    private GameObject itemObject = null;
    public Transform spawnPoint = null;
    //public Transform showcase = null;
    //public GameObject showcaseObjRef = null;

    private string ArrToStr(object value)
    {
        Type valueType = value.GetType();
        string valueString;
        // Если значение является массивом, то делаем каждый элемент строкой и соединяем вместе
        if (valueType.IsArray)
        {
            var items = (value as IEnumerable).Cast<object>().Select(x => x?.ToString() ?? "null");
            valueString = $"[{string.Join(", ", items)}]";
        }
        else
        {
            valueString = value.ToString();
        }
        return valueString;
    }

    private void Start()
    {
        if (itemObject == null)
            return;
        ItemCommon itemCommon = itemObject.GetComponent<ItemCommon>();
        if (itemCommon == null)
        {
            Debug.LogError("gameObject has no component ItemCommon which is required in Item Prefab");
            itemObject = null;
            return;
        }

        itemName.text = itemCommon.name;
        switch (itemCommon.ComponentType)
        {
            case ComponentType.CPU:
                var info = itemCommon.GetCPUInfo();
                itemDescription.text = $"Модель: {info.Model}, Сокет: {info.SocketType}";
                break;
            //case ComponentType.Cooler:
            //    break;
            //case ComponentType.GPU:
            //    break;
            //case ComponentType.RAM:
            //    break;
            //case ComponentType.Motherboard:
            //    break;
            //case ComponentType.PowerSupply:
            //    break;
            //case ComponentType.StorageDevice:
            //    break;
            default:
                itemDescription.text = "";
                foreach (var kvp in itemCommon.GetInfo().ToDict()) 
                {
                    itemDescription.text += $"{kvp.Key}: {kvp.Value}, ";
                }
                break;
        }
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            Instantiate(itemObject, spawnPoint.position, Quaternion.identity);
        }
        );
    }

    public void ApplyComponent(GameObject cmp)
    {
        itemObject = cmp;
        gameObject.SetActive(true);
        Start();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
