using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShopDrug : MonoBehaviour
{
    public static ShopDrug instance;

    private bool isShow = false;
    private GameObject numberDialog;
    private GameObject resultImage;
    private InputField inputField;
    private Text resultText;
    private int buy_id;


    private void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
        numberDialog = this.transform.Find("NumberDialog").gameObject;
        inputField = this.transform.Find("NumberDialog/InputField").GetComponent<InputField>();
        resultImage= this.transform.Find("ResultImg").gameObject;
        resultText= this.transform.Find("ResultImg/ResultText").GetComponent<Text>();
    }

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
            transform.DOLocalMove(new Vector3(1000, 0, 0), 1);
        }
    }


    public void OnCloseButtonClick()
    {
        TransformState();
    }


    public void OnBuyId1001()
    {
        Buy(1001);
    }

    public void OnBuyId1002()
    {
        Buy(1002);
    }

    public void OnBuyId1003()
    {
        Buy(1003);
    }

    void Buy(int id)
    {
        ShowNumberDialog();
        buy_id = id;
    }

    void ShowNumberDialog()
    {
        numberDialog.SetActive(true);
        inputField.text = "0";
    }

    public void OnOkButtonClick()
    {
        int count = int.Parse(inputField.text);
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(buy_id);
        int price = info.price_buy;
        int price_total = price * count;
        bool success = Inventory.instance.GetCoin(price_total);
        Sequence se = DOTween.Sequence();
        if (success)
        {
            if (count > 0)
            {
                Inventory.instance.GetId(buy_id, count);
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

}
