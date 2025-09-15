using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    [field: SerializeField] private List<TaskViewer> TaskViewerObj { get; set; } = new List<TaskViewer>();
    [field: SerializeField] private TextMeshProUGUI TextToChange { get; set; } = null;
    [field: SerializeField] private string NewText { get; set; } = "Все задачи выполнены!";

    public void TryToWin()
    {
        if (TextToChange == null)
        {
            return;
        }

        foreach (var taskViewer in TaskViewerObj)
        {
            Debug.Log(taskViewer.ToString());
            if (!taskViewer.AreAllTasksCompleted())
            {
                return;
            }
        }

        // foreach (var taskViewer in TaskViewerObj)
        // {
        //     foreach (var task in taskViewer.GameTasks)
        //     {
        //         task.gameObject.SetActive(false);
        //     }
        // }

        TextToChange.text = NewText;
    }
}
