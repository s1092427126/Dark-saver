using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.EventSystems;

public class EquipmentItem : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    public int id;

    private bool isEnter = false;
    private string str = "RPG/GUI/Icon/";

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (isEnter)
        {
            if (Input.GetMouseButtonDown(1))
            {
                EquipmentUI.instance.TakeOff(id,this.gameObject);
                
            }
        }
    }

    #region SetId

    public void SetId(int id)
    {
        this.id = id;
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        SetInfo(info);
    }
    #endregion

    #region SetInfo
    public void SetInfo(ObjectInfo info)
    {
        this.id = info.id;

        StringBuilder sb = new StringBuilder(str);
        sb.Append(info.icon_name);
        //Debug.Log(sb);
        image.sprite = Resources.Load<Sprite>(sb.ToString());
    }
    #endregion

    #region 鼠标移入移出
    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
    }
    #endregion
}
