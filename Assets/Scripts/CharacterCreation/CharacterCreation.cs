using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public GameObject[] characterPrefabs;

    private GameObject[] _charactorGameObjects;

    public int selectedIndex = 0;

    public int length;

    // Start is called before the first frame update
    void Start()
    {
        length = characterPrefabs.Length;
        _charactorGameObjects = new GameObject[length];
        for(int i = 0; i < length; i++)
        {
            _charactorGameObjects[i] = GameObject.Instantiate(characterPrefabs[i], transform.position, transform.rotation) as GameObject;
        }
        UpdateCharacterShow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCharacterShow()
    {
        //Debug.Log(selectedIndex);
        _charactorGameObjects[selectedIndex].SetActive(true);
        for(int i = 0; i < length; i++)
        {
            if(i != selectedIndex)
            {
                _charactorGameObjects[i].SetActive(false);
            }
        }
    }
}
