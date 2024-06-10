using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public BodyData ClothData;
    public Cloth[] m_Cloths;

    void Awake()
    {
        Instance = this;
        SetLoop(false);
    }

    void Update()
    {
        var horizon = Input.GetAxis("Horizontal");
        PlayAnim(horizon);
    }

    public void OnPrevClick(EBodyPart part)
    {
        var index = m_Cloths[(int)part].Prev();
        ClothData.Cloths[(int)part].Index = index;
    }

    public void OnNextClick(EBodyPart part)
    {
        var index = m_Cloths[(int)part].Next();
        ClothData.Cloths[(int)part].Index = index;
    }

    public void OnColorClick(EBodyPart part, int color)
    {
        m_Cloths[(int)part].SetColor(color);
        ClothData.Cloths[(int)part].Color = color;
    }

    public void PlayAnim(float value)
    {
        foreach (var cloth in m_Cloths)
        {
            cloth.PlayAnim(value);
        }
    }

    public void SetLoop(bool loop)
    {
        foreach (var cloth in m_Cloths)
        {
            cloth.SetLoop(loop);
        }
    }
    
    public void ParseData(BodyData data)
    {
        for (int i = 0; i < data.Cloths.Length; i++)
        {
            m_Cloths[i].FromData(data.Cloths[i]);
        }
    }
}