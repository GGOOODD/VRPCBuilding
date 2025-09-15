using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MotherboardSteps : MonoBehaviour
{
    [field: SerializeField] private TextMeshProUGUI MemoryObject { get; set; } = null;
    [field: SerializeField] private TextMeshProUGUI ProcessorObject { get; set; } = null;
    [field: SerializeField] private TextMeshProUGUI CoolerObject { get; set; } = null;
    [field: SerializeField] private TextMeshProUGUI GraphicsObject { get; set; } = null;

    public void UpdateCompletions(bool memoryState, bool processorState, bool coolerState, bool graphicsState)
    {
        Debug.Log(
            "memoryState: " + memoryState.ToString() + ", " +
            "coolerState: " + coolerState.ToString() + ", " +
            "processorState: " + processorState.ToString() + ", " +
            "graphicsState: " + graphicsState.ToString()
        );
        if (MemoryObject != null)
        {
            UpdateState(MemoryObject, memoryState);
        }
        if (ProcessorObject != null)
        {
            UpdateState(ProcessorObject, processorState);
        }
        if (CoolerObject != null)
        {
            UpdateState(CoolerObject, coolerState);
        }
        if (GraphicsObject != null)
        {
            UpdateState(GraphicsObject, graphicsState);
        }
    }

    private void UpdateState(TextMeshProUGUI obj, bool isComplete)
    {
        obj.fontStyle = isComplete ? FontStyles.Strikethrough : FontStyles.Normal;
        //obj.text = isComplete.ToString();
    }
}
