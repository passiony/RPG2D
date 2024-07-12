using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TagsPanel : MonoBehaviour
{
    public Text nameTxt;
    public Text ageTxt;
    public Text timeTxt;
    public Text contentTxt;
    public Button restartBtn;
    public Button quitBtn;

    
    string[] HairNames = { "Twin tails", "bobo", "long", "short", "very short", "frizzy" };
    string[] TopNames = { "Shirt", "Sailor blouse", "Armor", "Capelet" };
    string[] BottomNames = { "Shorts", "Slacks", "Skirts", "Long skirt" };

    void Start()
    {
        restartBtn.onClick.AddListener(OnRestartClick);
        quitBtn.onClick.AddListener(OnQuitClick);

        nameTxt.text = Player.Instance.GetName();
        ageTxt.text = Player.Instance.GetAge();
        timeTxt.text = $"于【{DateTime.Now:d}】来到勇者的世界";

        var hair = Player.Instance.ClothData.Cloths[0].Index;
        var top = Player.Instance.ClothData.Cloths[2].Index;
        var down = Player.Instance.ClothData.Cloths[3].Index;
        var dress = $"有着一头{HairNames[hair]}发行，上身身穿{TopNames[top]}，下身{BottomNames[down]}。";

        var sb = new StringBuilder();
        sb.AppendLine(dress);
        sb.AppendLine("在来到村子后，");
        sb.AppendLine(Player.Instance.GetAllTags());
        contentTxt.text = sb.ToString();
    }

    private void OnQuitClick()
    {
        Application.Quit();
    }

    private void OnRestartClick()
    {
        SceneManager.LoadScene(2);
    }

}