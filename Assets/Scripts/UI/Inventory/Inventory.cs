using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItemGrid> itemGridList = new List<InventoryItemGrid>();
    public Text coinText;

    private int coinCount = 200;

    private bool isOpen = false;

    public GameObject inventoryItem;

    private void Awake()
    {
        instance = this;
        
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetId(Random.Range(2001, 2022));
        }
    }

    #region AddCoin
    /// <summary>
    /// 增加金币
    /// </summary>
    /// <param name="count"></param>
    public void AddCoin(int count)
    {
        coinCount += count;
        coinText.text = coinCount.ToString();
    }
    #endregion

    #region 拾取到Id的物品，并添加到物品栏里
    /// <summary>
    /// 拾取到Id的物品，并添加到物品栏里
    /// </summary>
    /// <param name="id"></param>
    public void GetId(int id, int count = 1)
    {
        InventoryItemGrid grid = null;
        foreach (var item in itemGridList)
        {
            if (item.id == id)
            {
                grid = item;
                break;
            }
        }

        if (grid != null)
        {
            grid.PlusNumber(count);
        }
        else
        {
            foreach (var item in itemGridList)
            {
                if (item.id == 0) 
                {
                    grid = item;
                    break;
                }
            }

            if (grid != null) 
            {
                var tempItem = GameObject.Instantiate(inventoryItem);
                tempItem.transform.SetParent(grid.transform);
                tempItem.transform.localPosition = Vector3.zero;
                grid.SetId(id,count);
            }
        }
    }

    #endregion

    #region 打开关闭背包

    public void TransformState()
    {
        if (isOpen == false)
        {
            isOpen = true;
            transform.DOLocalMove(new Vector3(0, 0, 0), 1);
            coinText.text = coinCount.ToString();
        }
        else
        {
            isOpen = false;
            transform.DOLocalMove(new Vector3(1000, 0, 0), 1);
        }
    }
    #endregion

    #region GetCoin
    /// <summary>
    /// 判断金币是否足够
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool GetCoin(int count)
    {
        if(coinCount >= count)
        {
            coinCount -= count;
            coinText.text = coinCount.ToString();
            return true;
        }
        return false;
    }
    #endregion

    #region MinusId
    /// <summary>
    /// 使用物品
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool MinusId(int id,int count = 1)
    {
        InventoryItemGrid grid = null;
        foreach (var item in itemGridList)
        {
            if (item.id == id)
            {
                grid = item;
                break;
            }
        }
        if (grid == null)
        {
            return false;
        }
        else
        {
            bool isSuccess = grid.MinusNumber(count);
            return isSuccess;
        }
    }
    #endregion
}
