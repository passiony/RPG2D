using System;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    public Cloth[] m_Cloths;
    private Vector3 movePosition;

    void Start()
    {
        //初始化Player穿戴
        var json = PlayerPrefs.GetString("PlayerCloth");
        var data = JsonUtility.FromJson<BodyData>(json);
        ParseData(data);
    }

    public void ParseData(BodyData data)
    {
        for (int i = 0; i < data.Cloths.Length; i++)
        {
            m_Cloths[i].FromData(data.Cloths[i]);
        }
    }

}