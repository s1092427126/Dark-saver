using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour
{
    public GameObject magicianPrefab;
    public GameObject swordmanPrefab;

    public Transform characterSpawn;

    private void Awake()
    {
        int selectIndex = PlayerPrefs.GetInt(PlayerPrefsName.SelectedCharacterIndex.ToString());
        string name = PlayerPrefs.GetString(PlayerPrefsName.Name.ToString());

        GameObject go = null;
        if (selectIndex == 0)
        {
            go = GameObject.Instantiate(magicianPrefab, characterSpawn.position, Quaternion.identity);
            go.GetComponent<PlayerInfo>().name = name;
        }
        else if(selectIndex == 1)
        {
            go = GameObject.Instantiate(swordmanPrefab, characterSpawn.position, Quaternion.identity);
            go.GetComponent<PlayerInfo>().name = name;
        }
    }
    
}
