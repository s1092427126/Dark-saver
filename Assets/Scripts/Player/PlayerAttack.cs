using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack _instance;

    public PlayerState state = PlayerState.ControlWalk;
    public PlayerState state_attack = PlayerState.Idle;

    public string aniname_normalattack;//普通攻击动画
    public string aniname_idle;
    public string aniname_now;
    public float time_normalattack;//普通攻击时间
    public float rate_normalattack = 1;
    private float timer = 0;
    public float min_distance = 5;//最小攻击距离
    //public float normalattack = 34;//攻击力
    private Transform target_normalattack;
    private PlayerMove move;

    private Animation anim;
    private PlayerInfo pi;

    public GameObject effect;
    private bool showEffect = false;

    public float miss_rate = 0.3f;//闪避概率

    private bl_HUDText HUDRoot;

    public AudioClip miss_clip;

    public GameObject mate;
    private Color oriColor;

    public string aniname_death;//死亡动画

    public GameObject[] efxArray;//技能特效
    private Dictionary<string, GameObject> efxDic = new Dictionary<string, GameObject>();//技能特效字典

    public bool isLockingTarget = false;//是否正在选择目标
    SkillInfo info = null;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        move = GetComponent<PlayerMove>();
        anim = GetComponent<Animation>();
        pi = GetComponent<PlayerInfo>();
        HUDRoot = GameObject.FindGameObjectWithTag(Tags.HUD.ToString()).GetComponent<bl_HUDText>();
        oriColor = mate.GetComponent<Renderer>().material.color;

        foreach (var item in efxArray)
        {
            efxDic.Add(item.name, item);
        }
    }

    private void Update()
    {
        if (isLockingTarget==false && Input.GetMouseButtonDown(0) && state != PlayerState.Death && state != PlayerState.SkillAttack) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider & hitInfo.collider.tag == Tags.Enemy.ToString())
            {
                target_normalattack = hitInfo.collider.transform;
                state = PlayerState.NormalAttack;
                timer = 0;
                showEffect = false;
            
            }
            else
            {
                state = PlayerState.ControlWalk;
                target_normalattack = null;
            }
        }
        if(state == PlayerState.NormalAttack)
        {
            if (target_normalattack == null)
            {
                state = PlayerState.ControlWalk;
            }
            else
            {
                float distance = Vector3.Distance(transform.position, target_normalattack.position);
                if (distance <= min_distance)
                {
                    transform.LookAt(target_normalattack);
                    state_attack = PlayerState.Attack;
                    timer += Time.deltaTime;
                    anim.CrossFade(aniname_now);
                    if (timer >= time_normalattack)
                    {
                        aniname_now = aniname_idle;
                        if (showEffect == false)
                        {
                            showEffect = true;
                            GameObject.Instantiate(effect, target_normalattack.position, Quaternion.identity);
                            target_normalattack.GetComponent<WolfBaby>().TakeDamage(GetAttack());
                        }
                    }
                    if (timer >= (1 / rate_normalattack))
                    {
                        showEffect = false;
                        timer = 0;
                        aniname_now = aniname_normalattack;
                    }
                    if (target_normalattack.GetComponent<WolfBaby>().hp <= 0)
                    {
                        target_normalattack = null;
                    }
                }
                else
                {
                    state_attack = PlayerState.Moving;
                    move.SimpleMove(target_normalattack.position);
                }
            }
        }
        else if(state ==PlayerState.Death)
        {
            anim.CrossFade(aniname_death);
        }

        if(isLockingTarget&& Input.GetMouseButtonDown(0))
        {
            OnlockTarget();
        }
    }

   

    #region 获得攻击力GetAttack
    /// <summary>
    /// 获得攻击力
    /// </summary>
    /// <returns></returns>
    public int  GetAttack()
    {
        int attackValue = (int)(EquipmentUI.instance.attack + pi.attack + pi.attack_plus);
        return attackValue;
    }
    #endregion

    #region 受到伤害TakeDamage
    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="attack"></param>
    public void TakeDamage(int attack)
    {
        if (state == PlayerState.Death) return;
        float def = EquipmentUI.instance.def + pi.def + pi.def_plus;
        float temp = attack * ((200.0f - def) / 200.0f);

        if (temp < 1) temp = 1;

        float value = UnityEngine.Random.Range(0f, 1f);
        if(value < miss_rate)
        {
            AudioSource.PlayClipAtPoint(miss_clip, transform.position);
            HUDRoot.NewText("Miss", transform, Color.green, 8, 20f, -1f, 2.2f, bl_Guidance.Up);
        }
        else
        {
            HUDRoot.NewText("-" + attack, transform, Color.red, 8, 20f, -1f, 2.2f, bl_Guidance.Up);
            pi.hp_remain -= temp;

            StartCoroutine(ShowBodyRed());

            if (pi.hp_remain <= 0)
            {
                state = PlayerState.Death;
            } 
        }
        HeadStatusUI._instance.UpdateShow();
    }

    /// <summary>
    /// 身体变红
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowBodyRed()
    {
        mate.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mate.GetComponent<Renderer>().material.color = oriColor;
    }
    #endregion

    public void UseSkill(SkillInfo info)
    {
        if(pi.heroType == HeroType.Magician)
        {
            if(info.applicationType == ApplicationType.Swordman)
            {
                return;
            }
        }
        if (pi.heroType == HeroType.Swordman)
        {
            if (info.applicationType == ApplicationType.Magician)
            {
                return;
            }
        }

        switch (info.applyType)
        {
            case ApplyType.Passive:
                StartCoroutine(OnPassiveSkillUse(info));
                break;
            case ApplyType.Buff:
                StartCoroutine(OnBuffSkillUse(info));
                break;
            case ApplyType.SingleTarget:
                OnSingleTargetSkillUse(info);
                break;
            case ApplyType.MultiTarget:
                OnMultiTargetSkillUse(info);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 处理增益技能
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    IEnumerator OnPassiveSkillUse(SkillInfo info)
    {
        state = PlayerState.SkillAttack;
        anim.CrossFade(info.aniname);
        yield return new WaitForSeconds(info.anitime);
        state = PlayerState.ControlWalk;
        int hp = 0, mp = 0;
        if (info.applyProperty == ApplyProperty.HP)
        {
            hp = info.applyValue;
        }else if (info.applyProperty == ApplyProperty.MP)
        {
            mp = info.applyValue;
        }
        pi.GetDrug(hp, mp);
        GameObject prefab = null;
        efxDic.TryGetValue(info.efx_name, out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);

       
    }

    IEnumerator OnBuffSkillUse(SkillInfo info)
    {
        state = PlayerState.SkillAttack;
        anim.CrossFade(info.aniname);
        yield return new WaitForSeconds(info.anitime);
        state = PlayerState.ControlWalk;

        GameObject prefab = null;
        efxDic.TryGetValue(info.efx_name, out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);


        switch (info.applyProperty)
        {
            case ApplyProperty.Attack:
                pi.attack *= info.applyValue / 100f;
                break;
            case ApplyProperty.AttackSpeed:
                rate_normalattack *= (info.applyValue / 100f);
                break;
            case ApplyProperty.Def:
                pi.def *= (info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                move.speed *= (info.applyValue / 100f);
                break;
        }
        yield return new WaitForSeconds(info.applyTime);
        switch (info.applyProperty)
        {
            case ApplyProperty.Attack:
                pi.attack /= (info.applyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                rate_normalattack /= (info.applyValue / 100f);
                break;
            case ApplyProperty.Def:
                pi.def /= (info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                move.speed /= (info.applyValue / 100f);
                break;
        }
    }

   
    /// <summary>
    /// 选择目标
    /// </summary>
    /// <param name="info"></param>
    void OnSingleTargetSkillUse(SkillInfo info)
    {
        state = PlayerState.SkillAttack;
        CursorManager.instance.SetLockTarget();
        isLockingTarget = true;
        this.info = info;
    }

    /// <summary>
    /// 技能释放
    /// </summary>
    private void OnlockTarget()
    {
        isLockingTarget = false;
        switch (info.applyType)
        {
            case ApplyType.SingleTarget:
                StartCoroutine(OnlockSingleTaget());
                break;
            case ApplyType.MultiTarget:
                StartCoroutine(OnlockMultiTaget());
                break;
          
        }
    }

   

    IEnumerator OnlockSingleTaget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isCollider = Physics.Raycast(ray, out hitInfo);
        if(isCollider && hitInfo.collider.tag == Tags.Enemy.ToString())
        {
            anim.CrossFade(info.name);
            yield return new WaitForSeconds(info.anitime);
            state = PlayerState.ControlWalk;
            GameObject prefab = null;
            efxDic.TryGetValue(info.efx_name, out prefab);
            GameObject.Instantiate(prefab, hitInfo.collider.transform.position, Quaternion.identity);

            hitInfo.collider.GetComponent<WolfBaby>().TakeDamage((int )(GetAttack() * (info.applyValue / 100f)));
        }
        else
        {
            state = PlayerState.NormalAttack;
        }
        CursorManager.instance.SetNormal();
    }
  


    private void OnMultiTargetSkillUse(SkillInfo info)
    {
        state = PlayerState.SkillAttack;
        CursorManager.instance.SetLockTarget();
        isLockingTarget = true;
        this.info = info;
    }

    IEnumerator OnlockMultiTaget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isCollider = Physics.Raycast(ray, out hitInfo,11);
        if (isCollider)
        {
            anim.CrossFade(info.name);
            yield return new WaitForSeconds(info.anitime);
            state = PlayerState.ControlWalk;
            GameObject prefab = null;
            efxDic.TryGetValue(info.efx_name, out prefab);
            GameObject go = GameObject.Instantiate(prefab, hitInfo.point + Vector3.up * 0.5f, Quaternion.identity);
            go.GetComponent<MagiSphere>().attack = (int)(GetAttack() * (info.applyValue / 100f));
        }
        else
        {
            state = PlayerState.ControlWalk;
        }
        CursorManager.instance.SetNormal();
    }

}
