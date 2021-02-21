using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI instance;

    private bool isShow = false;
    private PlayerInfo pi;

    private GameObject headgear;
    private GameObject armor;
    private GameObject leftHand;
    private GameObject rightHand;
    private GameObject shoe;
    private GameObject accessory;

    public GameObject equipmentItem;

    public float attack = 0;
    public float def = 0;
    public float speed = 0;
    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
        pi = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).GetComponent<PlayerInfo>();

        headgear = transform.Find("Headgear").gameObject;
        armor = transform.Find("Armor").gameObject;
        leftHand = transform.Find("LeftHand").gameObject;
        rightHand = transform.Find("RightHand").gameObject;
        shoe = transform.Find("Shoe").gameObject;
        accessory = transform.Find("Accessory").gameObject;
    }

    #region 显示关闭
    public void TransformState()
    {
        if (isShow == false) 
        {
            isShow = true;
            transform.DOLocalMove(new Vector3(0, 0, 0), 1);
        }
        else
        {
            isShow = false;
            transform.DOLocalMove(new Vector3(1000,0, 0), 1);

        }
    }
    #endregion

    #region 穿戴功能
    /// <summary>
    /// 穿戴功能
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Dress(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        if(info.type != ObjectType.Equip)
        {
            return false;
        }
        if(pi.heroType == HeroType.Magician)
        {
            if (info.applicationType == ApplicationType.Swordman)
            {
                return false;
            }
        }
        if(pi.heroType == HeroType.Swordman)
        {
            if (info.applicationType == ApplicationType.Magician)
            {
                return false;
            }
        }

        GameObject parent = null;
        switch (info.dressType)
        {
            case DressType.Headgear:
                parent = headgear;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.RightHand:
                parent = rightHand;
                break;
            case DressType.LeftHand:
                parent = leftHand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
            case DressType.Accessory:
                parent = accessory;
                break;
            default:
                break;
        }
        EquipmentItem item = parent.GetComponentInChildren<EquipmentItem>();
        if (item != null)
        {
            Inventory.instance.GetId(item.id);
            item.SetInfo(info);
        }
        else
        {
            var tempItem = GameObject.Instantiate(equipmentItem);
            tempItem.transform.SetParent(parent.transform);
            tempItem.transform.localPosition = Vector3.zero;
            tempItem.GetComponent<EquipmentItem>().SetInfo(info);
        }

        return true;
    }
    #endregion
    
    public void TakeOff(int id,GameObject go)
    {
        Inventory.instance.GetId(id);
        GameObject.Destroy(go);
    }


    void UpdateProperty()
    {
        this.attack = 0;
        this.def = 0;
        this.speed = 0;
        EquipmentItem headgearItem = headgear.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headgearItem);
        EquipmentItem armorItem = armor.GetComponentInChildren<EquipmentItem>();
        PlusProperty(armorItem);
        EquipmentItem leftHandItem = leftHand.GetComponentInChildren<EquipmentItem>();
        PlusProperty(leftHandItem);
        EquipmentItem rightHandItem = rightHand.GetComponentInChildren<EquipmentItem>();
        PlusProperty(rightHandItem);
        EquipmentItem shoeItem = shoe.GetComponentInChildren<EquipmentItem>();
        PlusProperty(shoeItem);
        EquipmentItem accessoryItem = accessory.GetComponentInChildren<EquipmentItem>();
        PlusProperty(accessoryItem);
    }

    void PlusProperty(EquipmentItem item)
    {
        if (item != null)
        {
            ObjectInfo equipInfo = ObjectsInfo.instance.GetObjectInfoById(item.id);
            this.attack += equipInfo.attack;
            this.def += equipInfo.def;
            this.speed += equipInfo.speed;
        }
    }
}
