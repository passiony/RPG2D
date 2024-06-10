using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinCell : MonoBehaviour
{
    public EBodyPart m_BodyPart;
    
    public Button prevBtn;
    public Button nextBtn;
    public Button color1;
    public Button color2;
    public Button color3;

    void Start()
    {
        prevBtn.onClick.AddListener(() =>
        {
            Player.Instance.OnPrevClick(m_BodyPart);

        });
    }
    
    void Update()
    {
    }
}