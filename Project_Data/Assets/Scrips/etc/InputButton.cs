using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputButton : MonoBehaviour
{
    public TextMeshProUGUI input_num;
    public float status_num;

    // Start is called before the first frame update
    void Start()
    {
        status_num = float.Parse(input_num.text);
    }

    void Update()
    {
        input_num.text = status_num.ToString() ;
    }

    public void Onclick()
    {
       status_num++;
        if (status_num > 5) status_num =1 ;
    }
}
