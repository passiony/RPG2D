using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : Cloth
{
    public override void SetColor(int color)
    {
        colorIndex = color;
        m_Color.r = color * 0.01f;
        m_Color.g = color * 0.01f;
        m_Color.b = color * 0.01f;
        m_Color.a = 1;
        gameObject.GetComponent<SpriteRenderer>().color = m_Color;
    }
    
    public override void SetClothIndex(int index)
    {
        
    }
}