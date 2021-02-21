using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieCamera : MonoBehaviour
{
    private int _speed = 5;

    private int _endZ = -20;

    public bool isTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < _endZ)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
        else
        {
            isTarget = true;
        }
    }
}
