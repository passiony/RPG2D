using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimerEvent : MonoBehaviour
{
    public int Delay = 60;
    public UnityEvent TimeEvent;

    public Transform player;
    public Transform targetPoint;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Delay);
        Debug.Log("时间到");
        TimeEvent?.Invoke();
        
        player.position = targetPoint.position;
    }
}
