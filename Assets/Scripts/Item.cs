using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string Id;

    public void Trigger()
    {
        var npc = gameObject.GetComponent<Npc>();
        if (npc != null)
        {
            npc.OnClick();
        }
    }
}
