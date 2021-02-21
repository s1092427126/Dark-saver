using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDir : MonoBehaviour
{
    public GameObject effect_click_prefabs;

    private bool _isCollider;

    public Vector3 targetPos;

    private bool isMoving = false;

    PlayerMove playerMove;
    PlayerAttack attack;
    private void Start()
    {
        targetPos = transform.position;
        playerMove = GetComponent<PlayerMove>();
        attack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //    Debug.Log("鼠标处于UI上");
        //else
        //    Debug.Log("鼠标不处于UI上");
        if (attack.state == PlayerState.Death || attack.state == PlayerState.SkillAttack) return;

        if (PlayerAttack._instance.isLockingTarget == false && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            _isCollider = Physics.Raycast(ray, out hitInfo);
            if (_isCollider && hitInfo.collider.tag == Tags.Ground.ToString())
            {
                isMoving = true;
                ShowClickEffect(hitInfo.point);
                LookAtTarget(hitInfo.point);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            _isCollider = Physics.Raycast(ray, out hitInfo);
            if (_isCollider && hitInfo.collider.tag == Tags.Ground.ToString())
            {
                LookAtTarget(hitInfo.point);
            }
        }
        else if (playerMove.isMoving)
        {
            LookAtTarget(targetPos);
        }
        
    }

    private void ShowClickEffect(Vector3 point)
    {
        point = new Vector3(point.x, point.y + 0.1f, point.z);
        GameObject.Instantiate(effect_click_prefabs, point, Quaternion.identity);
    }

    public void LookAtTarget(Vector3 point)
    {
        targetPos = point;
        targetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        transform.LookAt(targetPos);
    }

}
