using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonContainer : MonoBehaviour
{
    public AudioSource audioSource;
    public void OnNewGame()
    {
        PlayerPrefs.SetInt("DataFromSave", 0);
        audioSource.Play();
        SceneManager.LoadScene(1);
    }

    public void OnLoadGame()
    {
        PlayerPrefs.SetInt("DataFromSave", 1);//DataFromSave数据来自保存
        audioSource.Play();
    }
}
