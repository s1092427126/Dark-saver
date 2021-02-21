using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShopWeapon_NPC : Npc
{
    public AudioSource audioSource;
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            audioSource.Play();
            ShopWeaponUI._instance.TransformState();
        }
    }

}
