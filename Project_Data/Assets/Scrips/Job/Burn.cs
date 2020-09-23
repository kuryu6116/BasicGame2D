using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    float time;

    void Update()
    {
        time += Time.deltaTime;

        if (time >= 0.2)
        {
            Destroy(gameObject);
        }
    }
}