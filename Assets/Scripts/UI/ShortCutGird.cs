using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShortCutGird : MonoBehaviour
{
    public KeyCode keyCode;

    private string str = "RPG/GUI/Icon/";
    private ShortCutType type = ShortCutType.None;
    private Image icon;
    private int id;
    private SkillInfo skillInfo;
    private PlayerInfo pi;
    private PlayerAttack pa;
    private ObjectInfo objectInfo;
    private void Awake()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        icon.gameObject.SetActive(false);
    }

    private void Start()
    {
        pi = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).GetComponent<PlayerInfo>();
        pa= GameObject.FindGameObjectWithTag(Tags.Player.ToString()).GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            if(type == ShortCutType.Drug)
            {
                OnDrugUse();
            }
            else if(type == ShortCutType.Skill)
            {
                bool success = pi.TakeMP(skillInfo.mp);
                if (success == false)
                {

                }
                else
                {
                    pa.UseSkill(skillInfo);
                }
            }

        }
    }

    public void SetSkill(int id)
    {
        this.id = id;
        this.skillInfo = SkillsInfo.instance.GetSkillInfoById(id);
        icon.gameObject.SetActive(true);

        StringBuilder sb = new StringBuilder(str);
        sb.Append(skillInfo.icon_name);
        icon.sprite = Resources.Load<Sprite>(sb.ToString());
        type = ShortCutType.Skill;
    }

    public void SetInventory(int id)
    {
        this.id = id;
        objectInfo = ObjectsInfo.instance.GetObjectInfoById(id);
        icon.gameObject.SetActive(true);

        StringBuilder sb = new StringBuilder(str);
        sb.Append(objectInfo.icon_name);
        icon.sprite = Resources.Load<Sprite>(sb.ToString());
        type = ShortCutType.Drug;
    }

    public void OnDrugUse()
    {
        bool success = Inventory.instance.MinusId(id, 1);
        if(success)
        {
            pi.GetDrug(objectInfo.hp, objectInfo.mp);
        }
        else
        {
            type = ShortCutType.None;
            icon.gameObject.SetActive(false);
            id = 0;
            skillInfo = null;
            objectInfo = null;
        }
    }
}
