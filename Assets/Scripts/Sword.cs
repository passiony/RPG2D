using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject EndPanel;
    private Animator m_Animator;
    
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    public async void PlayAnim()
    {
        m_Animator.Play("拔剑");

        await System.Threading.Tasks.Task.Delay(3000);
        
        EndPanel.SetActive(true);
    }
}
