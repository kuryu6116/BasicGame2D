using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//コストの文字の表示をおこなう
public class CostManeger : MonoBehaviour
{
    //現在のコストを表示
    [SerializeField]
    TextMeshProUGUI now_cost;
    public float _now_cost;

    //インプットの値の合計を表示
    [SerializeField]
    TextMeshProUGUI sum_num;
    public TextMeshProUGUI button_hp,button_at,button_df,button_sp;
    [SerializeField]
    float input_hp, input_at, input_df, input_sp;
    [SerializeField]
    public float sum_cost;

    void SetSumCost()
    {
        _now_cost = GetComponent<GameManegement>().cost_p2;
        input_hp = float.Parse(button_hp.text);
        input_at = float.Parse(button_at.text);
        input_df = float.Parse(button_df.text);
        input_sp = float.Parse(button_sp.text);
    }

    // Update is called once per frame
    void Update()
    {
        SetSumCost();
        //今のコストの表示
        now_cost.text = _now_cost.ToString();

        //インプットの合計時の表示
        sum_cost = input_hp + input_at + input_df + input_sp;
        sum_num.text = sum_cost.ToString();
    }
}
