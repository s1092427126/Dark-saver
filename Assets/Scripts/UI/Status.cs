using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Status : MonoBehaviour
{
    public static Status instance;
    private bool isShow = false;

    public Text attackText;
    public Text defText;
    public Text speedText;
    public Text pointRemainText;
    public Text summyText;

    public GameObject attackBtn;
    public GameObject defBtn;
    public GameObject speedBtn;

    private PlayerInfo playerInfo;

    private void Start()
    {
        instance = this;
        playerInfo = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).GetComponent<PlayerInfo>();
        gameObject.SetActive(false);
    }

    #region 打开关闭
    public void TransformState()
    {
        if (isShow == false)
        {
            UpdateShow();
            transform.DOLocalMove(new Vector3(0, 0, 0), 1);
            isShow = true;
        }
        else
        {
            transform.DOLocalMove(new Vector3(1000,0, 0), 1);
            isShow = false;
        }
    }
   
    #endregion

    private void UpdateShow()
    {
        attackText.text = playerInfo.attack + "+" + playerInfo.attack_plus;
        defText.text = playerInfo.def + "+" + playerInfo.def_plus;
        speedText.text = playerInfo.speed + "+" + playerInfo.speed_plus;

        pointRemainText.text = playerInfo.point_remain.ToString();

        summyText.text = "伤害：" + (playerInfo.attack + playerInfo.attack_plus) + " " + 
            "防御：" + (playerInfo.def + playerInfo.def_plus) + " " + 
            "速度：" + (playerInfo.speed + playerInfo.speed_plus);

        if (playerInfo.point_remain > 0)
        {
            attackBtn.SetActive(true);
            defBtn.SetActive(true);
            speedBtn.SetActive(true);
        }
        else
        {
            attackBtn.SetActive(false);
            defBtn.SetActive(false);
            speedBtn.SetActive(false);
        }

    }

    public void OnAttackPlusClick()
    {
        bool success = playerInfo.GetPoint();
        if (success)
        {
            playerInfo.attack_plus++;
            UpdateShow();
        }
    }

    public void OnDefPlusClick()
    {
        bool success = playerInfo.GetPoint();
        if (success)
        {
            playerInfo.def_plus++;
            UpdateShow();
        }
    }

    public void OnSpeedPlusClick()
    {
        bool success = playerInfo.GetPoint();
        if (success)
        {
            playerInfo.speed_plus++;
            UpdateShow();
        }
    }
}
