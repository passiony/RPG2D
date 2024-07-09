using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    private Animator m_Animator;
    public float m_Speed = 1;
    private Vector3 movePosition;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        movePosition = transform.position;
    }

    void Update()
    {
        var horizon = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (horizon != 0 || vertical != 0)
        {
            movePosition += new Vector3(horizon, vertical, 0) * Time.deltaTime * m_Speed;
            movePosition.y = Mathf.Clamp(movePosition.y, -2.3f, -1.5f);
            transform.position = movePosition;
        }

        PlayAnim(horizon);
        if (Input.GetMouseButtonDown(0) && !Player.IsPointerOverUIObject()) // 检测鼠标左键点击
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

    public void PlayAnim(float value)
    {
        this.m_Animator.SetFloat("Blend", value);
    }
}