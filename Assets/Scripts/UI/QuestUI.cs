using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestUI : MonoBehaviour
{
    public Image image;

    public AudioSource audioSource;

    public Bar_Npc bar_Npc = null;

    public Button okBtn;
    public Button cancelBtn;
    public Button acceptBtn;

    public PlayerInfo playerInfo;

   

    private void Start()
    {
        playerInfo = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).GetComponent<PlayerInfo>();
    }

    public void OpenQuestUI()
    {
        audioSource.Play();
        image.rectTransform.DOLocalMove(new Vector3(340, 0, 0), 1);
    }


    public void CloseQuestUI()
    {
        audioSource.Play();
        image.rectTransform.DOLocalMove(new Vector3(750, 0, 0), 1);
        
        //gameObject.SetActive(false);
    }

    public void OnAcceptButtonClick()
    {
        bar_Npc.ShowTaskProgress();
        okBtn.gameObject.SetActive(true);
        acceptBtn.gameObject.SetActive(false);
        cancelBtn.gameObject.SetActive(false);
    }

    public void OnCancelButtonClick()
    {
        CloseQuestUI();
    }

    public void OnOkButtonClick()
    {
        if (bar_Npc.isAccomplish)
        {
            Inventory.instance.AddCoin(1000);
            bar_Npc.killCount = 0;
            bar_Npc.ShowTaskDes();
        }
        else
        {
            CloseQuestUI();
        }
    }
}
