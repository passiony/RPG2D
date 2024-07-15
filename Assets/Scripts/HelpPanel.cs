using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : MonoBehaviour
{
    public Button m_CloseBtn;
    
    void Start()
    {
        m_CloseBtn.onClick.AddListener(OnCloseClick);
    }

    private void OnCloseClick()
    {
        gameObject.SetActive(false);
    }

}
