using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public BodyData ClothData;
    public Cloth[] m_Cloths;
    public Pack m_Pack;
    public bool GameMode;
    public float m_Speed;

    void Awake()
    {
        Instance = this;
        SetLoop(false);
    }

    void Update()
    {
        if (GameMode)
        {
            var horizon = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            transform.position += new Vector3(horizon, vertical, 0) * Time.deltaTime * m_Speed;
            PlayAnim(horizon);
        }
        else
        {
            var horizon = Input.GetAxis("Horizontal");
            PlayAnim(horizon);
        }


        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject()) // 检测鼠标左键点击
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Click detected on: " + hit.collider.gameObject.name);
                var npc = hit.collider.GetComponent<Npc>();
                if (npc)
                {
                    npc.OnClick();
                }
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        // 使用当前的 EventSystem
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };

        // Raycast UI
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
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