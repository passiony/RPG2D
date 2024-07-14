using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public Button m_LoginBtn;
    
    void Start()
    {
        m_LoginBtn.onClick.AddListener(OnLoginClick);
    }

    private void OnLoginClick()
    {
        SceneManager.LoadScene(1);
    }

}
