using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region PlayerPrefs名称
/// <summary>
/// PlayerPrefs名称
/// </summary>
public enum PlayerPrefsName
{
    /// <summary>
    /// 选择的人物索引
    /// </summary>
    SelectedCharacterIndex,

    /// <summary>
    /// 玩家名字
    /// </summary>
    Name
    
}
#endregion

#region 标签
/// <summary>
/// 标签
/// </summary>
public enum Tags
{
    Player,

    Ground,

    Inventory_item_grid,

    Inventory_item,

    SkillItem,

    ShortCut,

    Minimap,

    Enemy,

    HUD

}
#endregion

#region 玩家当前状态
/// <summary>
/// 玩家当前状态
/// </summary>
public enum PlayerState
{
    Moving,
    Idle,
    Attack,
    Death,

    ControlWalk,
    NormalAttack,
    SkillAttack

}
#endregion

#region 物品类型
/// <summary>
/// 物品类型
/// </summary>
public enum ObjectType
{
    /// <summary>
    /// 药品
    /// </summary>
    Drug,

    /// <summary>
    /// 装备
    /// </summary>
    Equip,

    /// <summary>
    /// 材料
    /// </summary>
    Mat
}
#endregion

#region 装备类型

/// <summary>
/// 穿戴类型
/// </summary>
public enum DressType
{
    /// <summary>
    /// 头盔、帽子
    /// </summary>
    Headgear,

    /// <summary>
    /// 盔甲
    /// </summary>
    Armor,

    /// <summary>
    /// 右手
    /// </summary>
    RightHand,

    /// <summary>
    /// 左手
    /// </summary>
    LeftHand,

    /// <summary>
    /// 鞋子
    /// </summary>
    Shoe,

    /// <summary>
    /// 饰品
    /// </summary>
    Accessory

}

#endregion

#region 适用类型
/// <summary>
/// 适用类型
/// </summary>
public enum ApplicationType
{
    /// <summary>
    /// 剑士
    /// </summary>
    Swordman,

    /// <summary>
    /// 魔法师
    /// </summary>
    Magician,

    /// <summary>
    /// 公共
    /// </summary>
    Common
}
#endregion

#region 角色类型
/// <summary>
/// 角色类型
/// </summary>
public enum HeroType
{
    Swordman,

    Magician
}


#endregion

#region 技能作用类型
/// <summary>
/// 技能作用类型
/// </summary>
public enum ApplyType
{
    /// <summary>
    /// 增益、加HP、MP
    /// </summary>
    Passive,
    /// <summary>
    /// 增强、加攻击、防御、移速
    /// </summary>
    Buff,
    /// <summary>
    /// 单体
    /// </summary>
    SingleTarget,
    /// <summary>
    /// 群体
    /// </summary>
    MultiTarget
}
#endregion

#region 技能作用属性

/// <summary>
/// 技能作用属性
/// </summary>
public enum ApplyProperty
{
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP
}

#endregion

#region 技能释放类型
/// <summary>
/// 技能释放类型
/// </summary>
public enum ReleaseType
{
    /// <summary>
    /// 当前位置释放
    /// </summary>
    Self,

    /// <summary>
    /// 指定敌人释放
    /// </summary>
    Enemy,

    /// <summary>
    /// 指定位置释放
    /// </summary>
    Position
}

#endregion

#region 快捷类型
/// <summary>
/// 快捷类型
/// </summary>
public enum ShortCutType
{
    /// <summary>
    /// 技能
    /// </summary>
    Skill,

    /// <summary>
    /// 药品
    /// </summary>
    Drug,

    None
}
#endregion

#region 敌人状态
/// <summary>
/// 敌人状态
/// </summary>
public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Death
}
#endregion

