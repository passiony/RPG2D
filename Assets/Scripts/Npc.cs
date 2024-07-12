using System;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public string Name;
    public TaskData[] Tasks;
    public int taskIndex;
    private TaskData m_Task;
    public int Score;
    public string failedTag;

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
                case ETaskType.Branch:
                    OnBranchTask();
                    break;
                case ETaskType.Score:
                    OnScoreTask();
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
        if (!Player.Instance.Pack.HasItem(itemId))
        {
            var dialog = m_Task.Dialogues;
            DialogPanel.Instance.ShowDialog(this, dialog, () => { Debug.Log("未获取Item：" + taskIndex); }, () =>
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
        DialogPanel.Instance.ShowDialog(this, dialog,
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

    void OnBranchTask()
    {
        Debug.Log("Reward任务：" + m_Task.Name);
    }

    void OnScoreTask()
    {
        bool match = false;
        var array = m_Task.Score.Split('-');
        if (array.Length == 1)
        {
            var min = int.Parse(array[0]);
            match = Score == min;
        }
        else if (array.Length == 2)
        {
            var min = int.Parse(array[0]);
            var max = int.Parse(array[1]);

            match = Score >= min && Score <= max;
        }

        Debug.Log("Score任务：" + m_Task.Name);
        if (match)
        {
            var dialog = m_Task.Dialogues;
            DialogPanel.Instance.ShowDialog(this, dialog, () => { Debug.Log("完成score任务：" + taskIndex); }, () =>
            {
                taskIndex = -1;
                Debug.Log("任务被永久打断");
            });
        }
        else
        {
            taskIndex++;
            Debug.Log("跳过任务：" + taskIndex);
            m_Task.OnFinish?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            var item = other.GetComponent<Item>();
            Player.Instance.Pack.HasItem(item.Id);
        }
    }

    public void CheckFinish()
    {
        if (taskIndex < Tasks.Length)
        {
            Player.Instance.AddTag(failedTag);
        }
    }
}