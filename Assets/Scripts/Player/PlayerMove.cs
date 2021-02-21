using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;

    public PlayerDir playerDir;

    public PlayerState state = PlayerState.Idle;

    private CharacterController _characterController;
    private PlayerAttack attack;

    public bool isMoving = false;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        playerDir = GetComponent<PlayerDir>();
        attack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.state == PlayerState.Death) return;

        if (attack.state == PlayerState.ControlWalk)
        {
            float distance = Vector3.Distance(playerDir.targetPos, transform.position);
            if (distance > 0.3f)
            {
                isMoving = true;
                state = PlayerState.Moving;
                _characterController.SimpleMove(transform.forward * speed);

            }
            else
            {
                isMoving = false;
                state = PlayerState.Idle;
            }
        }

        if (!_characterController.isGrounded)
        {
            _characterController.Move(new Vector3(0, -1000, 0) * Time.deltaTime);
        }
    }

    public void SimpleMove(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        _characterController.SimpleMove(transform.forward * speed);
    }
}
