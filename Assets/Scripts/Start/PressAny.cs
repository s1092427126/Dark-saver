using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PressAny : MonoBehaviour
{
    Image image;

    private bool isAnyKey = false;

    public GameObject buttonContainer;

    public MovieCamera movieCamera;
    void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (isAnyKey == false && movieCamera.isTarget == true) 
        {
            if (Input.anyKey)
            {
                ShowButton();
            }
        }
        
    }

    private void ShowButton()
    {
        buttonContainer.SetActive(true);
        this.gameObject.SetActive(false);
        isAnyKey = true;
    }

    public void DoAny()
    {
        image.DOColor(new Color(255, 255, 255, 1), 1);
    }
}
