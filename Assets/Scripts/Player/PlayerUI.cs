using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TouchButton leftBtn;
    public TouchButton rightBtn;
    public Button confirmBtn;
    public InputField nameInput;
    public InputField ageInput;
    public InputField fromInput;
    public ClothUI[] clothUIs;
    
    void Start()
    {
        leftBtn.OnDown.AddListener(OnLeftClick);
        rightBtn.OnDown.AddListener(OnRightClick);
        leftBtn.OnUp.AddListener(OnUpClick);
        rightBtn.OnUp.AddListener(OnUpClick);
        confirmBtn.onClick.AddListener(OnConfirmClick);
        Init();
    }

    void Init()
    {
        if (!PlayerPrefs.HasKey("PlayerCloth"))
        {
            return;
        }
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
    
    
    private void OnUpClick()
    {
        // Player.Instance.PlayAnim(0);
    }

    public void OnLeftClick()
    {
        // Player.Instance.PlayAnim(-1);
    }

    public void OnRightClick()
    {
        // Player.Instance.PlayAnim(1);
    }

    private void OnConfirmClick()
    {
        Player.Instance.ClothData.Name = nameInput.text;
        if (!string.IsNullOrEmpty(ageInput.text))
        {
            Player.Instance.ClothData.Age = int.Parse(ageInput.text);
        }
        Player.Instance.ClothData.From = fromInput.text;
        for (int i = 0; i < Player.Instance.m_Cloths.Length; i++)
        {
            Player.Instance.ClothData.Cloths[i] = Player.Instance.m_Cloths[i].ToData();
        }

        var json = JsonUtility.ToJson(Player.Instance.ClothData);
        Debug.Log(json);
        PlayerPrefs.SetString("PlayerCloth",json);
        FadeManager.Instance.FadeIn(() =>
        {
            SceneManager.LoadScene(2);
        });
    }
    
}