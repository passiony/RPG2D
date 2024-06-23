using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothUI : MonoBehaviour
{
    public EBodyPart m_BodyPart;

    private Button prevBtn;
    private Button nextBtn;
    private Slider colorSlider;

    // private Button color1;
    // private Button color2;
    // private Button color3;

    void Awake()
    {
        prevBtn = transform.Find("PrevBtn").GetComponent<Button>();
        nextBtn = transform.Find("NextBtn").GetComponent<Button>();

        // color1 = transform.Find("Color1").GetComponent<Button>();
        // color2 = transform.Find("Color2").GetComponent<Button>();
        // color3 = transform.Find("Color3").GetComponent<Button>();

        prevBtn.onClick.AddListener(() => { Player.Instance.OnPrevClick(m_BodyPart); });
        nextBtn.onClick.AddListener(() => { Player.Instance.OnNextClick(m_BodyPart); });

        colorSlider = transform.Find("Slider").GetComponent<Slider>();
        colorSlider.onValueChanged.AddListener((x) =>
        {
            var value = 1 - x;
            Player.Instance.OnColorClick(m_BodyPart, (int)(value * 100));
        });

        // color1.onClick.AddListener(() =>
        // {
        //     Player.Instance.OnColorClick(m_BodyPart,0);
        // });
        // color2.onClick.AddListener(() =>
        // {
        //     Player.Instance.OnColorClick(m_BodyPart,1);
        // });
        // color3.onClick.AddListener(() =>
        // {
        //     Player.Instance.OnColorClick(m_BodyPart,2);
        // });
    }

    public void SetValue(float value)
    {
        this.colorSlider.value = 1 - value;
    }
}