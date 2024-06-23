using System;
using UnityEngine.Events;

public enum ETaskType
{
    Item,//物品
    Dialog,//对话
    Game,//小游戏
    Reward,//奖励
}

[Serializable]
public class TaskData
{
    public ETaskType m_Type;
    public string ItemId;
    public string GameId;
    public Dialogue[] Dialogues;
    public string RewardId;
    public string Tag;
    public UnityEvent OnFinish;
}
