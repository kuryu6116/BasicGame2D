using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラの移動のみ行うクラス
public class Player : PlayerBase
{
    [SerializeField]
    GameObject nearObj;
    [SerializeField]
    GameObject nearGoalObj;

    string enemyTag,goalTag;
    public Rigidbody2D rb;

    // StaGt is called before the first frame update
    void Start()
    {

        //複数のタグを取得
        var enemy_tag = Tool.EnemyTag(gameObject);
        enemyTag = enemy_tag.Item1;
        goalTag = enemy_tag.Item2;

        //近くのオブジェクトを取得
        nearObj = Tool.SearchTag(gameObject, enemyTag);
        nearGoalObj = Tool.SearchTag(gameObject, goalTag);

        //自分のクラスを取得
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //近くのオブジェクトを取得
        nearObj = Tool.SearchTag(gameObject, enemyTag);
        nearGoalObj = Tool.SearchTag(gameObject, goalTag);

        //自分に近いオブジェクトに向かっていく
        if (nearObj == null)
        {
            //何も存在しない場合は止まる
            if (nearGoalObj == null)
            {
                rb.AddForce(new Vector2(0, 0));
            }
            else
            {
                rb.AddForce((nearGoalObj.transform.position - gameObject.transform.position) * Sp);
            }
        }
        else if (nearGoalObj == null)
        {
            rb.AddForce(new Vector2(0, 0));
        }
        else
        {
            //敵とGoalの中で近い方に向かう
            Vector3 vec = Tool.CompareDistance(gameObject, nearObj, nearGoalObj);
            rb.AddForce((vec - gameObject.transform.position) * Sp);
        }
    }
}

