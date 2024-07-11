using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerCloth"))
        {
            return;
        }
        
        //初始化Player穿戴
        var json = PlayerPrefs.GetString("PlayerCloth");
        Debug.Log(json);
        var data = JsonUtility.FromJson<BodyData>(json);
        Player.Instance.ParseData(data);
    }

}
