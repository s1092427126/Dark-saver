using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TweenAlpha : MonoBehaviour
{
    public TitleAlpha title;
    Image image;
    Tweener dt;
    void Start()
    {
        image = GetComponent<Image>();
        dt = image.DOColor(new Color(255, 255, 255, 0), 3);
        dt.OnComplete(title.DoTitle);
    }

}
