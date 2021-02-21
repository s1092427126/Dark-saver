using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMove move;
    private PlayerAttack attack;

    private  Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMove>();
        anim = GetComponent<Animation>();
        attack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.state == PlayerState.ControlWalk)
        {
            if (move.state == PlayerState.Moving)
            {
                //Debug.Log("1111");
                PlayerAnim("Walk");
            }
            else if (move.state == PlayerState.Idle)
            {
                PlayerAnim("Idle");
            }
        }
        else if (attack.state == PlayerState.NormalAttack)
        {
            if (attack.state_attack == PlayerState.Moving)
            {
                PlayerAnim("Run");
            }
        }
    }

    void PlayerAnim(string animName)
    {
        anim.CrossFade(animName);
    }
}
