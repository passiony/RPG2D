using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject EndPanel;
    private Animator m_Animator;
    private AudioSource m_Audio;
    
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Audio = gameObject.GetComponent<AudioSource>();
    }

    public async void PlayAnim()
    {
        m_Animator.Play("拔剑");
        m_Audio.Play();
        await System.Threading.Tasks.Task.Delay(3000);
        
        EndPanel.SetActive(true);
    }
}
