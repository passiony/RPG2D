using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Button leftBtn;
    public Button rightBtn;
    public Button parseBtn;
    public Button confirmBtn;
    public InputField nameInput;
    public InputField ageInput;
    public InputField fromInput;
    public ClothUI[] clothUIs;
    
    void Start()
    {
        leftBtn.onClick.AddListener(OnLeftClick);
        rightBtn.onClick.AddListener(OnRightClick);
        parseBtn.onClick.AddListener(OnParseClick);
        confirmBtn.onClick.AddListener(OnConfirmClick);
    }

    void Init()
    {
        var json = PlayerPrefs.GetString("PlayerCloth");
        var data = JsonUtility.FromJson<BodyData>(json);
        Player.Instance.ParseData(data);

        nameInput.text = data.Name;
        ageInput.text = data.Age.ToString();
        fromInput.text = data.From;
        for (int i = 0; i < clothUIs.Length; i++)
        {
            clothUIs[i].SetValue(data.Cloths[i].Color/100f);
        }
    }
    
    private void OnLeftClick()
    {
        Player.Instance.PlayAnim(-1);
    }

    private void OnRightClick()
    {
        Player.Instance.PlayAnim(1);
    }

    public void OnParseClick()
    {
        Init();
    }
    private void OnConfirmClick()
    {
        Player.Instance.ClothData.Name = nameInput.text;
        Player.Instance.ClothData.Age = int.Parse(ageInput.text);
        Player.Instance.ClothData.From = fromInput.text;
        for (int i = 0; i < Player.Instance.m_Cloths.Length; i++)
        {
            Player.Instance.ClothData.Cloths[i] = Player.Instance.m_Cloths[i].ToData();
        }

        var json = JsonUtility.ToJson(Player.Instance.ClothData);
        Debug.Log(json);
        PlayerPrefs.SetString("PlayerCloth",json);
    }
    
    
}