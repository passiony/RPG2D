using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public BodyData ClothData;
    public Cloth[] m_Cloths;
    public Pack Pack;
    public bool GameMode;
    public float m_Speed;
    private Vector3 movePosition;

    public string GetName()
    {
        return ClothData.Name;
    }

    public string GetAge()
    {
        return ClothData.Age.ToString();
    }

    public string GetAllTags()
    {
        var npcs= FindObjectsOfType<Npc>();
        foreach (var npc in npcs)
        {
            npc.CheckFinish();
        }
        return Pack.GetTags();
    }

    public void AddTag(string tag)
    {
        Pack.AddTag(ParseText(tag));
    }

    public string ParseText(string text)
    {
        text = text.Replace("[name]", GetName());
        text = text.Replace("[age]", GetAge());
        return text;
    }

    void Awake()
    {
        Instance = this;
        SetLoop(false);
    }

    private void Start()
    {
        movePosition = transform.position;
    }

    void Update()
    {
        if (GameMode)
        {
            var horizon = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            if (horizon != 0 || vertical != 0)
            {
                movePosition += new Vector3(horizon, vertical, 0) * Time.deltaTime * m_Speed;
                movePosition.y = Mathf.Clamp(movePosition.y, -2f, -1.5f);
                transform.position = movePosition;
            }

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

    public static bool IsPointerOverUIObject()
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
        this.ClothData = data;
        for (int i = 0; i < data.Cloths.Length; i++)
        {
            m_Cloths[i].FromData(data.Cloths[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            var item = other.GetComponent<Item>();
            Debug.Log("拾取道具：" + item.Id);
            Pack.AddItem(item.Id);
            Destroy(item.gameObject);
            item.Trigger();
        }
    }
}