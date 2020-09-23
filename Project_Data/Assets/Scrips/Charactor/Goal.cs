using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : PlayerBase
{
    string myTag,enemyTag, goalTag, charaName;

    Vector2 pos_up, pos_down;
    float time,_at;

    // StaGt is called before the first frame update
    void Start()
    {
        Hp = 20;
        myTag = gameObject.transform.tag;

        //プロパティの値を格納
        _at = gameObject.GetComponent<PlayerBase>().At;

        var enemy_tag = Tool.EnemyTag(gameObject);
        enemyTag = enemy_tag.Item1;
        goalTag = enemy_tag.Item2;
        charaName = enemy_tag.Item4;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //自分のタグで動きを決める
        //自分に近いオブジェクトに向かっていく
        if (myTag == "goal1")
        {
            if (time <= 5)
            {
                this.gameObject.transform.Translate(0, 0.003f, 0);
            }
            else
            {
                this.gameObject.transform.Translate(0, -0.003f, 0);
                if (time >= 10) time -= 10;
            }
        }
        else if (myTag == "goal2")
        {
            if (time <= 5)
            {
                this.gameObject.transform.Translate(0, -0.003f, 0);
            }
            else
            {
                this.gameObject.transform.Translate(0, 0.003f, 0);
                if (time >= 10) time -= 10;
            }
        }
        else
        {
            Debug.Log("goalがありません");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.transform.tag;
        if (myTag == "goal1")
        {
            if (collisionTag == "player1")
            {
                collision.gameObject.GetComponent<PlayerBase>().DirectAttack(0.5f);
            }
        }

        if(myTag == "goal2")
        {
            if (collisionTag == "player2")
            {
                collision.gameObject.GetComponent<PlayerBase>().DirectAttack(0.5f);
            }
        }
    }
}


