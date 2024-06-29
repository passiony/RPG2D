using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;



/// <summary>
/// 对话信息配置ScriptsObject
/// </summary>
[CreateAssetMenu(fileName = "DialogConfig", menuName = "ScriptableObjects/DialogConfig", order = 1)]
public class DialogConfig : ScriptableObject
{
    public string Title;
    public List<Dialogue> Dialogues;
}