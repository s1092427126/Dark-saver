using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private void OnMouseEnter()
    {
        CursorManager.instance.SetNpcTalk();
    }

    private void OnMouseExit()
    {
        CursorManager.instance.SetNormal();
    }
}
