using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiSphere : MonoBehaviour
{
    public int attack = 0;

    private List<WolfBaby> wolfList = new List<WolfBaby>();

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.Enemy.ToString())
        {
            WolfBaby wolf = col.GetComponent<WolfBaby>();
            int index = wolfList.IndexOf(wolf);
            if (index == -1)
            {
                wolf.TakeDamage(attack);
                wolfList.Add(wolf);
            }
        }
    }
}
