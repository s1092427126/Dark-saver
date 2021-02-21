using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    private Camera minimapCamera;

    private void Start()
    {
        minimapCamera = GameObject.FindGameObjectWithTag(Tags.Minimap.ToString()).GetComponent<Camera>();
    }

    /// <summary>
    /// 小地图视野放大
    /// </summary>
    public void OnZoomInClick()
    {
        minimapCamera.orthographicSize--;
    }

    /// <summary>
    /// 小地图视野缩小
    /// </summary>
    public void OnZoomOutClick()
    {
        minimapCamera.orthographicSize++;
    }
}
