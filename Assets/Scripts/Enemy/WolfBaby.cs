using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBaby : MonoBehaviour
{
    public EnemyState state = EnemyState.Idle;

    public string aniName_death;
    public string aniName_idle;
    public string aniName_walk;
    public string aniName_attack;
    public string aniName_now;
    public float speed = 3;
    public float hp = 100;
    public float  exp = 20f;
    public int attack = 20;

    public GameObject mate;
    public bl_HUDText HUDRoot;

    private Animation anim;
    private float timer;
    public float time = 1;
    private CharacterController cc;
    public float miss_rate = 0.1f;

    private Color oriColor;

    public string aniname_normalattack;
    public float time_normalattack;

    public string aniname_crazyattack;
    public float time_crazyattack;
    public float crazyattack_rate;

    public string aniname_attak_now;
    public int attack_rate = 1;
    private float attack_timer = 0;

    public Transform target;

    public float minDistance = 2;
    public float maxDistance = 5;

    public WolfSpawn spawn;

    private PlayerInfo pi;

    public AudioClip miss_clip;

    private void Start()
    {
        anim = GetComponent<Animation>();
        cc = GetComponent<CharacterController>();
        pi = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).GetComponent<PlayerInfo>();

        oriColor = mate.GetComponent<Renderer>().material.color;
        HUDRoot = GameObject.FindGameObjectWithTag(Tags.HUD.ToString()).GetComponent<bl_HUDText>();
    }


    void Update()
    {
        if (state == EnemyState.Death)
        {
            anim.CrossFade(aniName_death);
        }
        else if(state == EnemyState.Attack)
        {
            AutoAttack();
        }
        else
        {
            anim.CrossFade(aniName_now);
            if (aniName_now == aniName_walk)
            {
                cc.SimpleMove(transform.forward * speed);
            }

            timer += Time.deltaTime;
            if(timer >= time)
            {
                timer = 0;
                RandomState();
            }
        }

        if (!cc.isGrounded)
        {
            cc.Move(new Vector3(0, -1000, 0) * Time.deltaTime);
        }

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Debug.Log("AAA");
        //    TakeDamage(1);
        //}
           
     
        
    }

    private void AutoAttack()
    {
        if (target != null)
        {
            if (target.GetComponent<PlayerAttack>().state == PlayerState.Death)
            {
                target = null;
                state = EnemyState.Idle;
                return;
            }
            float distance = Vector3.Distance(target.position, transform.position);
            if(distance > maxDistance)
            {
                target = null;
                state = EnemyState.Idle;
            }
            else if (distance <= minDistance)
            {
                attack_timer += Time.deltaTime;
                anim.CrossFade(aniname_attak_now);
                if (aniname_attak_now == aniname_normalattack)
                {
                   if(attack_timer > time_normalattack)
                   {
                        target.GetComponent<PlayerAttack>().TakeDamage(attack);
                        aniname_attak_now = aniName_idle;
                   }
                }
                else if(aniname_attak_now == aniname_crazyattack)
                {
                    if (attack_timer > time_crazyattack)
                    {
                        target.GetComponent<PlayerAttack>().TakeDamage(attack);
                        aniname_attak_now = aniName_idle;
                    }
                }
                if (attack_timer > (1f / attack_rate))
                {
                    RandomAttack();
                    attack_timer = 0;
                }
            }
            else
            {
                transform.LookAt(target);
                cc.SimpleMove(transform.forward * speed);
                anim.CrossFade(aniName_walk);
            }
        }
        else
        {
            state = EnemyState.Idle;
        }
    }

    private void RandomAttack()
    {
        float value = UnityEngine.Random.Range(0, 1f);
        if(value < crazyattack_rate)
        {
            aniname_attak_now = aniname_crazyattack;
        }
        else
        {
            aniname_attak_now = aniname_normalattack;
        }
    }

    private void RandomState()
    {
        int value = UnityEngine.Random.Range(0, 2);
        if(value == 0)
        {
            aniName_now = aniName_idle;
        }
        else
        {
            if (aniName_now != aniName_walk)
            {
                transform.Rotate(Vector3.up * UnityEngine.Random.Range(0, 361));
            }
            aniName_now = aniName_walk;

        }
    }

    public void TakeDamage(int attack)
    {
        if (state == EnemyState.Death) return;
        target = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).transform;
        state = EnemyState.Attack;
        float value = UnityEngine.Random.Range(0f, 1f);
        if(value < miss_rate)
        {
            if (state != EnemyState.Death)
            {
                AudioSource.PlayClipAtPoint(miss_clip, transform.position);
                HUDRoot.NewText("Miss", transform, Color.green, 8, 20f, -1f, 2.2f, bl_Guidance.Up);
            }  
        }
        else
        {
            HUDRoot.NewText("-" + attack, transform, Color.red, 8, 20f, -1f, 2.2f, bl_Guidance.Up);
            this.hp -= attack;
            StartCoroutine(ShowBodyRed());
            
            if (this.hp <= 0)
            {
                state = EnemyState.Death;
                spawn.MinusNumber();
                pi.GetExp(exp);
                Bar_Npc._instance.OnKillWolf();
                Destroy(this.gameObject, 2f);
            }
 
        }
    }

    IEnumerator ShowBodyRed()
    {
        mate.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mate.GetComponent<Renderer>().material.color = oriColor;
    }

    #region 鼠标图标
    private void OnMouseEnter()
    {
        if (PlayerAttack._instance.isLockingTarget == false) 
            CursorManager.instance.SetAttack();
    }

    private void OnMouseExit()
    {
        if (PlayerAttack._instance.isLockingTarget == false)
            CursorManager.instance.SetNormal();
    }
    #endregion
}
