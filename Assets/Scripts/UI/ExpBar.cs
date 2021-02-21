using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public static ExpBar _instance;

    private Text expText;
    private Slider slider;
    private PlayerInfo pi;
    private void Start()
    {
        _instance = this;
        slider = GetComponent<Slider>();
        expText = transform.Find("expText").GetComponent<Text>();
        pi = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).GetComponent<PlayerInfo>();
    }

    public void SetValue(float value)
    {
        slider.value = value;
        value *= 100;
        expText.text = string.Format("{0:0.00}%", value);
    }
}
