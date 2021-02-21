using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryItemGrid : MonoBehaviour
{
    public int id = 0;
    private ObjectInfo info = null;

    public int num = 0;

    [SerializeField]
    private Text numText;

    private void Start()
    {
    }

    public void SetId(int id,int num = 1)
    {
        this.id = id;
        info = ObjectsInfo.instance.GetObjectInfoById(id);
        InventoryItem item = GetComponentInChildren<InventoryItem>();
        //Debug.Log(info);
        item.SetIconName(info.id,info.icon_name);

        this.num = num;

        item.ChangeText(this.num);
    }

    /// <summary>
    /// 清空格子存的物品信息
    /// </summary>
    public void ClearInfo()
    {
        id = 0;
        info = null;
        num = 0;
    }

    public void PlusNumber(int num = 1)
    {
        this.num += num;
        InventoryItem item = GetComponentInChildren<InventoryItem>();
        if(item != null)
        {
            item.ChangeText(this.num);
        }
    }

    public bool MinusNumber(int num = 1)
    {
        if (this.num >= num)
        {
            this.num -= num;
            InventoryItem item = GetComponentInChildren<InventoryItem>();
            if (this.num == 0)
            {
                ClearInfo();
                item.DestroyItem();
            }
            item.ChangeText(this.num);
            return true;
        }
        return false;
    }
}
