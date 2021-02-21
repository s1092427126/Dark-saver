using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform _player;
    private Vector3 _offsetPosition;
    private bool isRotating = false;

    public float distance;
    public float scrollSpeed = 1;
    public float rotateSpeed = 1;

    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).transform;
        transform.LookAt(_player.position);
        _offsetPosition = transform.position - _player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _offsetPosition + _player.position;
        RotateView();
        ScrollView();
    }

    private void RotateView()
    {
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            transform.RotateAround(_player.position, Vector3.up, rotateSpeed * Input.GetAxis("Mouse X"));

            Vector3 oriPos = transform.position;
            Quaternion oriRot = transform.rotation;


            transform.RotateAround(_player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));

            float x = transform.eulerAngles.x;
            if (x < 10 || x > 80)
            {
                transform.position = oriPos;
                transform.rotation = oriRot;
            }


            _offsetPosition = transform.position - _player.position;
        }

    }

    private void ScrollView()
    {
        distance = _offsetPosition.magnitude;
        distance += Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed;

        distance = Mathf.Clamp(distance, 2, 12);
        _offsetPosition = _offsetPosition.normalized * distance;
    }
}
