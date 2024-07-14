using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    static FadeManager _instance;

    public static FadeManager Instance
    {
        get
        {
            if (_instance == null) _instance = GameObject.FindObjectOfType<FadeManager>(true);
            return _instance;
        }
    }

    private Image m_Image;
    private Color m_CurrentColor = Color.black;
    public bool StartFadeOut;

    private void Start()
    {
        if (StartFadeOut)
        {
            FadeOut(null);
        }
    }

    public async void FadeIn(Action complete)
    {
        gameObject.SetActive(true);
        m_Image = gameObject.GetComponentInChildren<Image>();
        m_CurrentColor.a = 0;
        m_Image.color = m_CurrentColor;
        for (int i = 0; i < 10; i++)
        {
            await System.Threading.Tasks.Task.Delay(100);
            m_CurrentColor.a += 0.1f;
            m_Image.color = m_CurrentColor;
        }

        m_CurrentColor = Color.black;
        m_Image.color = Color.black;
        complete?.Invoke();
    }

    public async void FadeOut(Action complete)
    {
        gameObject.SetActive(true);
        m_Image = gameObject.GetComponentInChildren<Image>();
        m_CurrentColor = Color.black;
        m_Image.color = m_CurrentColor;
        for (int i = 0; i < 20; i++)
        {
            await System.Threading.Tasks.Task.Delay(100);
            m_CurrentColor.a -= 0.05f;
            m_Image.color = m_CurrentColor;
        }

        complete?.Invoke();
        gameObject.SetActive(false);
    }

    public async void FadeInOut(Action complete)
    {
        gameObject.SetActive(true);
        m_Image = gameObject.GetComponentInChildren<Image>();
        m_CurrentColor.a = 0;
        m_Image.color = m_CurrentColor;
        for (int i = 0; i < 10; i++)
        {
            await System.Threading.Tasks.Task.Delay(100);
            m_CurrentColor.a += 0.1f;
            m_Image.color = m_CurrentColor;
        }

        m_CurrentColor = Color.black;
        m_Image.color = Color.black;
        await System.Threading.Tasks.Task.Delay(1000);
        for (int i = 0; i < 20; i++)
        {
            await System.Threading.Tasks.Task.Delay(100);
            m_CurrentColor.a -= 0.05f;
            m_Image.color = m_CurrentColor;
        }

        complete?.Invoke();
        gameObject.SetActive(false);
    }
}