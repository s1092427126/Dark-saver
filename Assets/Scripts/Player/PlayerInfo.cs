using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int level = 1;
    public int hp = 100;//hp最大值
    public int mp = 100;//mp最大值
    public float hp_remain = 100;//剩余hp
    public float mp_remain = 100;//剩余mp

    public float exp = 0f;//当前经验
    public float total_exp;//当前等需要的总经验

    public float attack = 20;
    public int attack_plus = 0;
    public float def = 20;
    public int def_plus=0;
    public int speed = 20;
    public int speed_plus = 0;

    public HeroType heroType;

    public int point_remain = 0;

    private void Start()
    {
        total_exp = 100 + level * 30;
        GetExp(exp);
    }

    #region GetDrug使用药品
    /// <summary>
    /// 使用药品
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="mp"></param>
    public void GetDrug(int hp,int mp)
    {
        hp_remain += hp;
        mp_remain += mp;
        if (hp_remain > this.hp)
        {
            hp_remain = this.hp;
        }
        if (mp_remain > this.mp)
        {
            mp_remain = this.mp;
        }
        HeadStatusUI._instance.UpdateShow();
    }
    #endregion

    #region GetPoint技能加点
    /// <summary>
    /// 技能加点
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool GetPoint(int point = 1)
    {
        if (point_remain >= point)
        {
            point_remain -= point;
            return true;
        }
        return false;
    }
    #endregion

    #region GetExp获得经验
    /// <summary>
    /// 获得经验
    /// </summary>
    /// <param name="expCount"></param>
    public void GetExp(float expCount)
    {
        this.exp += expCount;
        total_exp = 100 + level * 30;
        while (exp > total_exp)
        {
            exp -= total_exp;
            level++;
            total_exp = 100 + level * 30;
        }
        
        ExpBar._instance.SetValue(exp / total_exp);
    }
    #endregion

    #region 使用MP
    /// <summary>
    /// 使用MP
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool TakeMP(int count)
    {
        if (mp_remain >= count)
        {
            mp_remain -= count;
            HeadStatusUI._instance.UpdateShow();
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
