using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShopWeaponUI : MonoBehaviour
{
    public static ShopWeaponUI _instance;
    public int[] weaponIdArray;
    public GameObject weaponItem;
    public Transform contect;

    private bool isShow = false;
    private GameObject numberDialog;
    private InputField inputField;
    private int buyId = 0;
    private GameObject resultImage;
    private Text resultText;
    private void Awake()
    {
        _instance = this;
        numberDialog = transform.Find("NumberDialog").gameObject;
        inputField = transform.Find("NumberDialog/InputField").GetComponent<InputField>();
        numberDialog.SetActive(false);
        resultImage = this.transform.Find("ResultImg").gameObject;
        resultText = this.transform.Find("ResultImg/ResultText").GetComponent<Text>();
    }

    private void Start()
    {
        InitShow();
    }

    public void TransformState()
    {
        if (isShow)
        {
            isShow = false;
            transform.DOLocalMove(new Vector3(1000, 0, 0),1);
        }
        else
        {
            isShow = true;
            transform.DOLocalMove(new Vector3(0, 0, 0), 1);
        }
    } 

    public void OnCloseButtonClick()
    {
        TransformState();
    }

    public void InitShow()
    {
        foreach (var id in weaponIdArray)
        {
            GameObject itemGo = GameObject.Instantiate(weaponItem,contect);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<WeaponItem>().SetId(id);
        }
    }

    public void OnOkBtnClick()
    {
        int count = int.Parse(inputField.text);
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(buyId);
        int price = info.price_buy;
        int total_price = price * count;
        bool success = Inventory.instance.GetCoin(total_price);
        Sequence se = DOTween.Sequence();
        if (success)
        {
            if (count > 0)
            {
                Inventory.instance.GetId(buyId, count);
                se.Append(resultImage.transform.DOScale(new Vector3(1, 1, 1), 1));
                resultText.text = "购买成功！";
                se.Append(resultImage.transform.DOScale(new Vector3(1, 1, 1), 1));
                se.Append(resultImage.transform.DOScale(new Vector3(0, 0, 0), 1));

            }
        }
        else
        {      
            se.Append(resultImage.transform.DOScale(new Vector3(1, 1, 1), 1));
            resultText.text = "金币不足！";
            se.Append(resultImage.transform.DOScale(new Vector3(1, 1, 1), 1));
            se.Append(resultImage.transform.DOScale(new Vector3(0, 0, 0), 1));
        }
        numberDialog.SetActive(false);
    }

    public void OnBuyClick(int id)
    {
        buyId = id;
        numberDialog.SetActive(true);
        inputField.text = "0";
    }
}
