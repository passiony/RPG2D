using System;
using System.Collections;
using System.Collections.Generic;
using Task;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public string Name;
    public TaskData[] Tasks;
    public int taskIndex;
    private TaskData m_Task;

    public void OnClick()
    {
        if (taskIndex == -1)
        {
            Debug.Log("任务已经结束");
            return;
        }

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
            DialogPanel.Instance.ShowDialog(dialog, () =>
            {
                Debug.Log("未获取Item：" + taskIndex);
            }, () =>
            {
                taskIndex = -1;
                Debug.Log("任务被永久打断");
            });
        }
        else
        {
            taskIndex++;
            Debug.Log("完成任务：" + taskIndex);
            m_Task.OnFinish?.Invoke();
        }
    }

    void OnDialogTask()
    {
        Debug.Log("Dialog任务：" + m_Task.Name);
        var dialog = m_Task.Dialogues;
        DialogPanel.Instance.ShowDialog(dialog,
            () =>
            {
                taskIndex++;
                m_Task.OnFinish?.Invoke();
            },
            () => { taskIndex = -1; });
    }

    void OnGameTask()
    {
        Debug.Log("Game任务：" + m_Task.Name);
    }

    void OnRewardTask()
    {
        Debug.Log("Reward任务：" + m_Task.Name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            var item = other.GetComponent<Item>();
            Player.Instance.m_Pack.Items.Add(item.Id);
        }
    }
}