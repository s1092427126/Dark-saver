using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillItemIcon : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject icon_name;
    private int skillId;

    #region 开始拖拽
    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        skillId = this.transform.parent.GetComponent<SkillItem>().id;
        this.transform.SetParent(this.transform.root);//当开始拖拽设置当前图片icon-name的父物体为根目录，取消拖拽的边界限制作用！

        GameObject[] skillitems = GameObject.FindGameObjectsWithTag(Tags.SkillItem.ToString());   //开始拖拽时我们通过查找所有的Tag=skillitem的预制体

        foreach (var item in skillitems)
        {
            if(item.transform.childCount != 3)//当skill item子物体不足两个时
            {
                Sprite goImg = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite;//获得当前拖拽的sprite
                GameObject go = Instantiate(icon_name);//创建一个新物体

                go.GetComponent<Image>().sprite = goImg;//将sprite赋予
                go.transform.SetParent(item.transform);//设置父物体
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150, 0);//设置偏移量
            }
        }
        //transform.position = eventData.position;
        SetDraggedPosition(eventData);  //转换坐标
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }
    #endregion

    #region 拖拽中
    /// <summary>
    /// 拖拽中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = eventData.position;
        SetDraggedPosition(eventData);  //转换坐标
    }
    #endregion

    #region 拖拽结束时
    /// <summary>
    /// 拖拽结束
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.tag == Tags.ShortCut.ToString()) 
        {
            //Debug.Log(eventData.pointerCurrentRaycast.gameObject);
            eventData.pointerCurrentRaycast.gameObject.GetComponent<ShortCutGird>().SetSkill(skillId);
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(gameObject);

    }
    #endregion

    #region 转换坐标
    /// <summary>
    /// 转换坐标
    /// </summary>
    /// <param name="eventData"></param>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = this.gameObject.GetComponent<RectTransform>();//获取rectTransform

        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;//变换坐标
        }
    }
    #endregion
}
