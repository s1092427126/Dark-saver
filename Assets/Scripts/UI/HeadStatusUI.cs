using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadStatusUI : MonoBehaviour
{
    public static HeadStatusUI _instance;

    private Text nickName;

    private Slider hpBar;
    private Slider mpBar;

    private Text hpText;
    private Text mpText;
    private PlayerInfo pi;
    private void Start()
    {
        _instance = this;
        nickName = transform.Find("Name_BG/NameText").GetComponent<Text>();
        hpBar = transform.Find("HP").GetComponent<Slider>();
        mpBar = transform.Find("MP").GetComponent<Slider>();
        hpText= transform.Find("HP/HPText").GetComponent<Text>();
        mpText = transform.Find("MP/MPText").GetComponent<Text>();
        pi = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).GetComponent<PlayerInfo>();
        UpdateShow();
    } 

    public void UpdateShow()
    {
        nickName.text = "Lv." + pi.level + " " + pi.name;
        hpBar.value = pi.hp_remain / pi.hp;
        
        mpBar.value = pi.mp_remain / pi.mp;
       
        hpText.text = pi.hp_remain + "/" + pi.hp;
        mpText.text = pi.mp_remain + "/" + pi.mp;
    }
}
