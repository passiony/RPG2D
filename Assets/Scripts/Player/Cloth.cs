using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth : MonoBehaviour
{
    public EBodyPart m_Part;
    public GameObject[] cloths;
    protected Color m_Color;

    public int clothIndex;
    public int colorIndex;

    private Animator[] m_Animator;

    protected Animator[] Animator
    {
        get
        {
            if (m_Animator == null) m_Animator = gameObject.GetComponentsInChildren<Animator>(true);
            return m_Animator;
        }
    }

    public virtual int Prev()
    {
        cloths[clothIndex].SetActive(false);
        clothIndex--;
        if (clothIndex < 0)
        {
            clothIndex = cloths.Length - 1;
        }

        cloths[clothIndex].SetActive(true);
        return clothIndex;
    }

    public virtual int Next()
    {
        cloths[clothIndex].SetActive(false);
        clothIndex++;
        if (clothIndex > cloths.Length - 1)
        {
            clothIndex = 0;
        }

        cloths[clothIndex].SetActive(true);
        return clothIndex;
    }

    public virtual void SetColor(int color)
    {
        colorIndex = color;
        m_Color.r = color * 0.01f;
        m_Color.g = color * 0.01f;
        m_Color.b = color * 0.01f;
        m_Color.a = 1;
        cloths[clothIndex].GetComponent<SpriteRenderer>().color = m_Color;

        // colorIndex = color;
        // cloths[clothIndex].GetComponent<SpriteRenderer>().color = colors[colorIndex];
    }

    public virtual void SetClothIndex(int index)
    {
        foreach (var cloth in cloths)
        {
            cloth.SetActive(false);
        }

        clothIndex = index;
        cloths[clothIndex].SetActive(true);
    }

    public void SetLoop(bool loop)
    {
        foreach (var anim in Animator)
        {
            var clips = anim.runtimeAnimatorController.animationClips;
            foreach (var clip in clips)
            {
                clip.wrapMode = loop ? WrapMode.Loop : WrapMode.Once;
            }
        }
    }
    
    public virtual void PlayAnim(float value)
    {
        foreach (var anim in Animator)
        {
            if (anim.isActiveAndEnabled)
            {
                anim.SetFloat("Blend", value);
            }
        }
    }

    public ClothData ToData()
    {
        var data = new ClothData();
        data.Index = clothIndex;
        data.Color = colorIndex;
        return data;
    }

    public void FromData(ClothData data)
    {
        SetClothIndex(data.Index);
        SetColor(data.Color);
    }
}