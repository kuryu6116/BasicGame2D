using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGage : MonoBehaviour
{
    [SerializeField]
    Slider _slider;

    bool isCallOnce;
    public float _hp;
    public float hp;

    void Start()
    {
        _slider = GetComponentInChildren<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        //ジョブ判定後のHPを一回だけ取得(=maxHP)
        if (!isCallOnce)
        {
            isCallOnce = true;
            _hp = GetComponent<PlayerBase>().Hp;
        }

        hp = GetComponent<PlayerBase>().Hp;
        //現状のHPのパーセンテージを与える
        _slider.value = hp / _hp;
    }
}
