using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBar : MonoBehaviour
{
    
    public void OnStatusBtnClick()
    {
        Status.instance.gameObject.SetActive(true);
        Status.instance.TransformState();
    }

    public void OnBagBtnClick()
    {
        Inventory.instance.gameObject.SetActive(true);
        Inventory.instance.TransformState(); 
    }

    public void OnEquipBtnClick()
    {
        EquipmentUI.instance.gameObject.SetActive(true);
        EquipmentUI.instance.TransformState();
    }

    public void OnSkillBtnClick()
    {
        SkillUI.instance.TransformState();
    }
}
