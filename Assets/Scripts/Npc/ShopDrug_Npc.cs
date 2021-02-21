using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShopDrug_Npc : Npc
{
    public AudioSource audioSource;
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            audioSource.Play();
            ShopDrug.instance.gameObject.SetActive(true);
            ShopDrug.instance.TransformState();
        }
    }

   
}
