using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawn : MonoBehaviour
{
    public int maxNum = 5;
    [SerializeField ]
    private int currNum = 0;
    public float time = 3f;
    [SerializeField]
    private float timer = 0;

    public GameObject prefab;

    private void Update()
    {
        if (currNum < maxNum)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                Vector3 pos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                pos += transform.position;
                GameObject go = GameObject.Instantiate(prefab, pos, Quaternion.identity);
                go.GetComponent<WolfBaby>().spawn = this;
                go.transform.SetParent(transform);
                timer = 0;
                currNum++;
            }
        }
    }

    public void MinusNumber()
    {
        this.currNum--;
    }
}
