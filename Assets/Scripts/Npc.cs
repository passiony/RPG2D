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

    void Start()
    {
    }

    public void OnClick()
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

    void OnItemTask()
    {
        var itemId = m_Task.ItemId;
        if (!Player.Instance.m_Pack.Items.Contains(itemId))
        {
            var dialog = m_Task.Dialogues;
            DialogPanel.Instance.ShowDialog(dialog);
        }
    }

    void OnDialogTask()
    {
        var dialog = m_Task.Dialogues;
        DialogPanel.Instance.ShowDialog(dialog);
        DialogPanel.Instance.OnComplete.RemoveAllListeners();
        DialogPanel.Instance.OnComplete.AddListener(() =>
        {
            taskIndex++;
        });
    }

    void OnGameTask()
    {
    }

    void OnRewardTask()
    {
    }
}