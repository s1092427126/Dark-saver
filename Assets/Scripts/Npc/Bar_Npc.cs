using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Bar_Npc : Npc
{
    public static Bar_Npc _instance;

    public QuestUI quest;
    public bool isInTask = false;
    public int killCount = 0;
    public Text des;
    public bool isAccomplish = false;
    public AudioSource audioSource;
    public int questRewards = 1000;

    private void Awake()
    {
        _instance = this;
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            AccomplishTask();
            if (isInTask)
            {
                audioSource.Play();
                ShowTaskProgress();
            }
            else
            {
                ShowTaskDes();
            }
            ShowQuest();
        }
    }

    private void ShowQuest()
    {
        quest.bar_Npc = this;
        quest.gameObject.SetActive(true);
        quest.OpenQuestUI();

    }

    public void ShowTaskProgress()
    {
        isInTask = true;
        des.text = "任务：\n你已经杀死了" + killCount + "/10只狼\n\n奖励：\n1000金币";
    }

    public void ShowTaskDes()
    {
        des.text = "任务：\n杀死10只狼\n\n奖励：\n1000金币";
    }

    public void AccomplishTask()
    {
        if (killCount >= 10)
        {
            isAccomplish = true;
        }
        else
        {
            isAccomplish = false;
        }
    }

    public void OnKillWolf()
    {
        if (isInTask)
        {
            killCount++;
        }
    }
}
