using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinUI : MonoBehaviour
{
    public EBodyPart m_BodyPart;

    private Button prevBtn;
    private Button nextBtn;
    private Slider colorSlider;

    void Awake()
    {
        prevBtn = transform.Find("PrevBtn").GetComponent<Button>();
        nextBtn = transform.Find("NextBtn").GetComponent<Button>();
        colorSlider = transform.Find("Slider").GetComponent<Slider>();

        prevBtn.onClick.AddListener(() => { Player.Instance.OnPrevClick(m_BodyPart); });
        nextBtn.onClick.AddListener(() => { Player.Instance.OnNextClick(m_BodyPart); });
        colorSlider.onValueChanged.AddListener((x) =>
        {
            Player.Instance.OnColorClick(m_BodyPart, (int)(100 - x * 100));
        });
    }
}