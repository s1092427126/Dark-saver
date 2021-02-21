using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.EventSystems;
using System;

public class InventoryItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    private Transform originalParent;

    private Image image;

    string str = "RPG/GUI/Icon/";

    public Text numText;

    private int id;
    private bool isEnter = false;

    private void Awake()
    {
        image = GetComponent<Image>();
        numText = GetComponentInChildren<Text>();
    }
    private void Update()
    {
        if (isEnter)
        {
            InventoryDes.instance.Show(id);

            if (Input.GetMouseButtonDown(1))
            {
                bool success = EquipmentUI.instance.Dress(id);
                if (success)
                {
                    transform.parent.GetComponent<InventoryItemGrid>().MinusNumber();
                }
            }
        }
    }

    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        StringBuilder sb = new StringBuilder(str);

        sb.Append(id);
        image.sprite = Resources.Load<Sprite>(sb.ToString());
    }

    public void SetIconName(int id,string icon_name)
    {
        StringBuilder sb = new StringBuilder(str);
        this.id = id;

        sb.Append(icon_name);

        image.sprite = Resources.Load<Sprite>(sb.ToString());
    }

    #region 鼠标拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.tag == Tags.Inventory_item_grid.ToString())//当拖放到空格子中
            {
                if (eventData.pointerCurrentRaycast.gameObject == originalParent.gameObject)//拖放到自己原本的格子中
                {

                }
                else
                {
                    InventoryItemGrid oldParent = originalParent.GetComponent<InventoryItemGrid>();

                    this.transform.parent = eventData.pointerCurrentRaycast.gameObject.transform;
                    InventoryItemGrid newParent = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventoryItemGrid>();
                    newParent.SetId(oldParent.id, oldParent.num);

                    transform.localPosition = Vector3.zero;

                    oldParent.ClearInfo();
                }
            }
            else if (eventData.pointerCurrentRaycast.gameObject.tag == Tags.Inventory_item.ToString())//拖放到一个有物品的格子里
            {
                InventoryItemGrid gird1 = originalParent.GetComponent<InventoryItemGrid>();
                InventoryItemGrid gird2 = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventoryItemGrid>();

                int id = gird1.id, num = gird1.num;

                gird1.SetId(gird2.id, gird2.num);
                gird2.SetId(id, num);
            }
            else if(eventData.pointerCurrentRaycast.gameObject.tag == Tags.ShortCut.ToString())
            {
                eventData.pointerCurrentRaycast.gameObject.GetComponent<ShortCutGird>().SetInventory(id);
            }
        }

        ResetPosition();

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    #endregion

    private void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
    }

    public void ChangeText(int num)
    {
        numText.text = num.ToString();
    }

    #region 鼠标移入移出

    /// <summary>
    /// 鼠标移入
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
        //InventoryDes.instance.Show(id);
    }

    /// <summary>
    /// 鼠标移出
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
        InventoryDes.instance.Close();
    }
    #endregion

    public void DestroyItem()
    {
        InventoryDes.instance.Close();
        Destroy(gameObject);
    }
}
