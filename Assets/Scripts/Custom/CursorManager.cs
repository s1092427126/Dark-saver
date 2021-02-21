using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    public Texture2D cursor_Normal;
    public Texture2D cursor_Npc_Talk;
    public Texture2D cursor_Attack;
    public Texture2D cursor_LockTarget;
    public Texture2D cursor_Pick;

    private Vector2 hotspot = Vector2.zero;
    private CursorMode mode = CursorMode.Auto;

    private void Start()
    {
        instance = this;
    }

    public void SetNormal()
    {
        Cursor.SetCursor(cursor_Normal, hotspot, mode);
    }

    public void SetNpcTalk()
    {
        Cursor.SetCursor(cursor_Npc_Talk, hotspot, mode);
    }

    public void SetAttack()
    {
        Cursor.SetCursor(cursor_Attack, hotspot, mode);
    }

    public void SetLockTarget()
    {
        Cursor.SetCursor(cursor_LockTarget, hotspot, mode);
    }
}
