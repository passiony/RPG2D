using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

/// <summary>
/// 单条对话实体类
/// </summary>
[System.Serializable]
public class Dialogue
{
    public string content;
    public string speaker;
    public bool autoNext = true;
    public Choice[] choices;
}

[System.Serializable]
public class Choice
{
    public string content;
    public int nextDialogIndex;
}

/// <summary>
/// 对话信息配置ScriptsObject
/// </summary>
[CreateAssetMenu(fileName = "DialogConfig", menuName = "ScriptableObjects/DialogConfig", order = 1)]
public class DialogConfig : ScriptableObject
{
    public string title;
    public List<Dialogue> dialogues;
}