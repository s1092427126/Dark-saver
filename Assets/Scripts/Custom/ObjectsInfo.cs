using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInfo : MonoBehaviour
{
    public static ObjectsInfo instance;

    public TextAsset objectsInfoListText;

    private Dictionary<int, ObjectInfo> objectInfoDic = new Dictionary<int, ObjectInfo>();

    private void Awake()
    {
        instance = this;
        ReadInfo();
    }

    #region 使用id获得物品信息 
    /// <summary>
    /// 使用id获得物品信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ObjectInfo GetObjectInfoById(int id)
    {
        ObjectInfo info = null;
        objectInfoDic.TryGetValue(id, out info);
        return info;
    }
    #endregion

    #region 读取信息
    /// <summary>
    ///读取信息
    /// </summary>
    void ReadInfo()
    {
        string text = objectsInfoListText.text;
        string[] strArray = text.Split('\n');

        foreach (var str in strArray)
        {
            string[] proArray = str.Split(',');
            ObjectInfo info = new ObjectInfo();

            //Debug.Log(proArray[0]);
            info.id= int.Parse(proArray[0]);

            info.name = proArray[1];
            info.icon_name = proArray[2];
            //Debug.Log(info.icon_name);
            string str_type = proArray[3];
            //ObjectType type = ObjectType.Drug;
            switch (str_type)
            {
                case "Drug":
                    info.type = ObjectType.Drug;
                    break;
                case "Equip":
                    info.type = ObjectType.Equip;
                    break;
                case "Mat":
                    info.type = ObjectType.Mat;
                    break;
            }

            if(info.type == ObjectType.Drug)
            {
                info.hp = int.Parse(proArray[4]);
                info.mp = int.Parse(proArray[5]);
                info.price_sell = int.Parse(proArray[6]);
                info.price_buy = int.Parse(proArray[7]);
            }
            else if (info.type == ObjectType.Equip)
            {
                info.attack = int.Parse(proArray[4]);
                info.def = int.Parse(proArray[5]);
                info.speed = int.Parse(proArray[6]);
                info.price_sell = int.Parse(proArray[9]);
                info.price_buy = int.Parse(proArray[10]);
                string str_dresstype = proArray[7];
                switch (str_dresstype)
                {
                    case "Headgear":
                        info.dressType = DressType.Headgear;
                        break;
                    case "Armor":
                        info.dressType = DressType.Armor;
                        break;
                    case "LeftHand":
                        info.dressType = DressType.LeftHand;
                        break;
                    case "RightHand":
                        info.dressType = DressType.RightHand;
                        break;
                    case "Shoe":
                        info.dressType = DressType.Shoe;
                        break;
                    case "Accessory":
                        info.dressType = DressType.Accessory;
                        break;
                    default:
                        break;
                }
                string str_apptype = proArray[8];
                switch (str_apptype)
                {
                    case "Swordman":
                        info.applicationType = ApplicationType.Swordman;
                        break;
                    case "Magician":
                        info.applicationType = ApplicationType.Magician;
                        break;
                    case "Common":
                        info.applicationType = ApplicationType.Common;
                        break;
                    default:
                        break;
                }

            }

            objectInfoDic.Add(info.id, info);
        }
    }

    #endregion
}

public class ObjectInfo
{
    public int id;
    public string name;
    public string icon_name;
    public ObjectType type;
    public int hp;
    public int mp;
    public int price_sell;
    public int price_buy;

    public int attack;
    public int def;
    public int speed;
    public DressType dressType;
    public ApplicationType applicationType;
}
