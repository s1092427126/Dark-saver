using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonCtrl : MonoBehaviour
{
    public CharacterCreation characterCreation;

    public AudioSource audioSource;

    public InputField inputField;
    public void OnNextButtonClick()
    {
        audioSource.Play();

        characterCreation.selectedIndex++;
        characterCreation.selectedIndex %= characterCreation.length;
        characterCreation.UpdateCharacterShow();
    }

    public void OnPrevButtonClick()
    {
        audioSource.Play();

        characterCreation.selectedIndex--;
        if (characterCreation.selectedIndex == -1)
        {
            characterCreation.selectedIndex = characterCreation.length - 1;
        }
            
        characterCreation.UpdateCharacterShow();
    }

    public void OnOkButtonClick()
    {
        audioSource.Play();

        PlayerPrefs.SetInt(PlayerPrefsName.SelectedCharacterIndex.ToString(), characterCreation.selectedIndex);

        PlayerPrefs.SetString(PlayerPrefsName.Name.ToString(), inputField.text);

        //跳转到下一个场景
        SceneManager.LoadScene(2);

    }
}
