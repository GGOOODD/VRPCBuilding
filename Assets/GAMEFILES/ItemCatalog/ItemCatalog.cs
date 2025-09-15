﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Windows;
using TMPro;
using NaughtyAttributes;
using System.Linq;


public class ItemCatalog : MonoBehaviour
{
    public TMP_Dropdown dropdown = null;
    public ItemWrapper[] itemWrappers = null;
    private GameObject[] components = null;
    private int currentPage = 1;
    public TextMeshProUGUI pageText = null;
    private ComponentType componentType = ComponentType.NotSelected;
    readonly private Dictionary<string, ComponentType> componentTypes = new Dictionary<string, ComponentType>()
    {
        { "Видеокарты", ComponentType.GPU },
        { "Процессоры", ComponentType.CPU },
        { "Жесткие диски", ComponentType.StorageDevice },
        { "Кулеры", ComponentType.Cooler },
        { "Материнские платы", ComponentType.Motherboard },
        { "Блоки питания", ComponentType.PowerSupply },
        { "Оперативная память", ComponentType.RAM },
    };

    void Start()
    {
        componentType = componentTypes[componentTypes.Keys.ToArray()[0]];
        var dropdownList = new List<TMP_Dropdown.OptionData>();
        foreach (var cmptk in componentTypes.Keys)
        {
            dropdownList.Add(new TMP_Dropdown.OptionData(cmptk));
        }
        Debug.Log(dropdownList.Count);
        dropdown.AddOptions(dropdownList);
        dropdown.onValueChanged.AddListener((i) =>
        {
            currentPage = 1;
            componentType = componentTypes[dropdown.options[i].text];
            UpdatePage();
        }
        );
        components = ComponentsLoader.GetComponentsPrefabs();
        UpdatePage();
    }

    private void UpdatePage()
    {
        List<GameObject> _cmpFilt = new List<GameObject>();

        foreach (GameObject c in components)
        {
            if (c.GetComponent<ItemCommon>().ComponentType == componentType)
                _cmpFilt.Add(c);
        }

        var cmpFilt = _cmpFilt.ToArray();
        int maxPages = (int)Math.Ceiling((float)cmpFilt.Length / (float)itemWrappers.Length);

        if (currentPage > maxPages)
            currentPage = maxPages;
        if (currentPage < 1)
            currentPage = 1;
        pageText.text = "Страница " + currentPage + " из " + maxPages;

        int cmpIndStart = (currentPage - 1) * itemWrappers.Length;
        int cmpIndEnd = Math.Min(cmpIndStart + itemWrappers.Length, cmpFilt.Length) - 1;
        for (int i = cmpIndStart; i <= cmpIndEnd; i++ )
        {
            itemWrappers[cmpIndEnd - i].ApplyComponent(cmpFilt[i]);
        }
        for (int i = cmpIndEnd - cmpIndStart + 1; i < itemWrappers.Length; i++)
        {
            itemWrappers[i].Hide();
        }
    }

    public void DropdownChange(String s)
    {
        Debug.Log(s);
    }

    public void NextPage()
    {
        Debug.Log("Catalog: next page");
        currentPage++;
        UpdatePage();
    }
    public void PrevPage()
    {
        Debug.Log("Catalog: previous page");
        currentPage--;
        UpdatePage();
    }
}