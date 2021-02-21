using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleAlpha : MonoBehaviour
{
    public PressAny pressAny;
    Image image;
    Tweener dt;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void DoTitle()
    {

        dt = image.DOColor(new Color(255, 255, 255, 1), 2);
        dt.OnComplete(pressAny.DoAny);
    }
}
