using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class SkillItem : MonoBehaviour
{
    public int id;
    private SkillInfo skillInfo;

    private Image iconName_Img;
    private Text name_Text;
    private Text applyType_Text;
    private Text des_Text;
    private Text mp_Text;
    private GameObject icon_mask;

    private string str = "RPG/GUI/Icon/";
    private void InitProperty()
    {
        iconName_Img = transform.Find("Icon_name").GetComponent<Image>();
        name_Text = transform.Find("Property/Name_Img/Name_Text").GetComponent<Text>();
        applyType_Text = transform.Find("Property/ApplyType_Img/ApplyType_Text").GetComponent<Text>();
        des_Text = transform.Find("Property/Des_Img/Des_Text").GetComponent<Text>();
        mp_Text = transform.Find("Property/MP_Img/MP_Text").GetComponent<Text>();
        icon_mask = transform.Find("Icon_Mask").gameObject;
        icon_mask.SetActive(false);
    }

    #region 技能是否可用
    /// <summary>
    /// 技能是否可用
    /// </summary>
    /// <param name="level"></param>
    public void UpdateShow(int level)
    {
        if(skillInfo.level <= level)
        {
            icon_mask.SetActive(false);
            iconName_Img.GetComponent<SkillItemIcon>().enabled = true;
        }
        else
        {
            icon_mask.SetActive(true);
            iconName_Img.GetComponent<SkillItemIcon>().enabled = false;
        }
    }
    #endregion

    #region SetId
    public void SetId(int id)
    {
        InitProperty();
        this.id = id;
        skillInfo = SkillsInfo.instance.GetSkillInfoById(id);
        StringBuilder sb = new StringBuilder(str);
        sb.Append(skillInfo.icon_name);
        iconName_Img.sprite= Resources.Load<Sprite>(sb.ToString());
        name_Text.text = skillInfo.name;
        switch (skillInfo.applyType)
        {
            case ApplyType.Passive:
                applyType_Text.text = "增益";
                break;
            case ApplyType.Buff:
                applyType_Text.text = "增强";
                break;
            case ApplyType.SingleTarget:
                applyType_Text.text = "单体";
                break;
            case ApplyType.MultiTarget:
                applyType_Text.text = "群体";
                break;
            default:
                break;
        }
        des_Text.text = skillInfo.des;
        mp_Text.text = skillInfo.mp + "MP";
    }
    #endregion
}
