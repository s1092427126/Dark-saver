using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SkillUI : MonoBehaviour
{
    public static SkillUI instance;

    private bool isShow = false;
    private PlayerInfo pi;

    public int[] magicianSkillList;
    public int[] swordmanSkillList;
    public GameObject content;
    public GameObject skillItem;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pi = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).GetComponent<PlayerInfo>();
        int[] idList = null;
        switch (pi.heroType)
        {
            case HeroType.Swordman:
                idList = swordmanSkillList;
                break;
            case HeroType.Magician:
                idList = magicianSkillList;
                break;
            default:
                break;
        }

        foreach (var id in idList)
        {
            GameObject go = GameObject.Instantiate(skillItem, content.transform);
            go.transform.localPosition = Vector3.zero;

            go.GetComponent<SkillItem>().SetId(id);

        }
    }


    #region TransformState
    public void TransformState()
    {
        if (isShow == false)
        {
            isShow = true;
            transform.DOLocalMove(new Vector3(0, 0, 0), 1);
            UpdateShow();
        }
        else
        {
            isShow = false;
            transform.DOLocalMove(new Vector3(1000, 0, 0), 1);

        }
    }

    private void UpdateShow()
    {
        SkillItem[] items = GetComponentsInChildren<SkillItem>();
        foreach (var item in items)
        {
            item.UpdateShow(pi.level);
        }
    }
    #endregion


}
