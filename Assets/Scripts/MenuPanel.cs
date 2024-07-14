using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public Button m_LoginBtn;
    public Button m_QuitBtn;
    
    void Start()
    {
        m_LoginBtn.onClick.AddListener(OnLoginClick);
        m_QuitBtn.onClick.AddListener(OnQuitClick);
    }

    private void OnQuitClick()
    {
        Application.Quit();
    }

    private void OnLoginClick()
    {
        SceneManager.LoadScene(1);
    }

}
