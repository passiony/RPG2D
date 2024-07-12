using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 背包
/// </summary>
[Serializable]
public class Pack
{
    private List<string> Items = new List<string>();
     private List<string> Tags = new List<string>();
     
    public void AddItem(string item)
    {
        Items.Add(item);
    }

    public bool HasItem(string item)
    {
        return Items.Contains(item);
    }
    
    public void AddTag(string item)
    {
        Tags.Add(item);
    }

    public string GetTags()
    {
        var sb = new StringBuilder();
        foreach (var tag in Tags)
        {
            sb.AppendLine(tag);
        }

        return sb.ToString();
    }
}
