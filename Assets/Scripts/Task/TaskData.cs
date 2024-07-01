using System;
using UnityEngine.Events;

public enum ETaskType
{
    Item,//物品
    Dialog,//对话
    Game,//小游戏
    Reward,//奖励
    Branch,//分支
    Score,//分数
}

public enum EBreakType
{
    None,//正常下一句对话
    Finish,//完成当前任务，跳过后续对话
    Repeat,//不完成，重复当前任务
    Break,//直接大端所有任务
}

/// <summary>
/// 单条对话实体类
/// </summary>
[System.Serializable]
public class Dialogue
{
    public string Content;
    public string Speaker;
    public EBreakType BreakType;
    public string Tag;
    public Choice[] Choices;
}

[System.Serializable]
public class Choice
{
    public string Content;
    public int NextDialogIndex;
    public int Score;
}

[System.Serializable]
public class Branch
{
    public string BranchValue;
    public int NextTaskIndex;
}

[Serializable]
public class TaskData
{
    public string Name;
    public ETaskType m_Type;
    public string ItemId;
    public string GameId;
    public string RewardId;
    public string Score;
    public string BranchName;
    public Branch[] Branchs;
    public Dialogue[] Dialogues;
    public UnityEvent OnFinish;
}
