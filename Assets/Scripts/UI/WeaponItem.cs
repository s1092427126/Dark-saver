using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class WeaponItem : MonoBehaviour
{
    private int id;
    private ObjectInfo info;

    private Image image;
    private Text nameText;
    private Text effectText;
    private Text priceSellText;

    private string str = "RPG/GUI/Icon/";
    private void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
        nameText = transform.Find("NameText").GetComponent<Text>();
        effectText = transform.Find("EffectText").GetComponent<Text>();
        priceSellText = transform.Find("PriceSellText").GetComponent<Text>();
    }

    public void SetId(int id)
    {
        this.id = id;
        info = ObjectsInfo.instance.GetObjectInfoById(id);

        StringBuilder sb = new StringBuilder(str);
        sb.Append(info.icon_name);
        image.sprite = Resources.Load<Sprite>(sb.ToString());
        nameText.text = info.name;

        if (info.attack > 0)
        {
            effectText.text = "+伤害：" + info.attack;
        }else if (info.def > 0)
        {
            effectText.text = "+防御：" + info.def;
        }else if (info.speed > 0)
        {
            effectText.text = "+速度：" + info.speed;
        }

        priceSellText.text = info.price_buy.ToString();
    }

    public void OnBuyBtnClick()
    {
        ShopWeaponUI._instance.OnBuyClick(id);
    }
}
