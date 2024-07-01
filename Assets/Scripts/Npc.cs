using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public string Name;
    public TaskData[] Tasks;
    private int taskIndex;
    private TaskData m_Task;

    private void Awake()
    {
        DialogPanel.Instance.OnComplete.AddListener(() => { taskIndex++; });
        DialogPanel.Instance.OnBreak.AddListener(() => { taskIndex = -1; });
    }

    public void OnClick()
    {
        if (taskIndex < Tasks.Length)
        {
            m_Task = Tasks[taskIndex];
            switch (m_Task.m_Type)
            {
                case ETaskType.Item:
                    OnItemTask();
                    break;
                case ETaskType.Dialog:
                    OnDialogTask();
                    break;
                case ETaskType.Game:
                    OnGameTask();
                    break;
                case ETaskType.Reward:
                    OnRewardTask();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    void OnItemTask()
    {
        var itemId = m_Task.ItemId;
        Debug.Log("Item任务：" + m_Task.Name);
        if (!Player.Instance.m_Pack.Items.Contains(itemId))
        {
            var dialog = m_Task.Dialogues;
            DialogPanel.Instance.ShowDialog(dialog);
        }
    }

    void OnDialogTask()
    {
        Debug.Log("Dialog任务：" + m_Task.Name);
        var dialog = m_Task.Dialogues;
        DialogPanel.Instance.ShowDialog(dialog);
    }

    void OnGameTask()
    {
        Debug.Log("Game任务：" + m_Task.Name);
    }

    void OnRewardTask()
    {
        Debug.Log("Reward任务：" + m_Task.Name);
    }
}