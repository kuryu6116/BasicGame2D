using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : PlayerBase
{
    GameObject[] tag_objs;

    float timeManeger,_at;
    string enemyTag, goalTag;

    GameObject damageDefender;

    // Start is called before the first frame update
    void Start()
    { 
        //タグ取得
        var enemy_tag = Tool.EnemyTag(gameObject);
        enemyTag = enemy_tag.Item1;
        goalTag = enemy_tag.Item2;

        //Job補正
        ChangeAttack(0.5f);
        ChangeDefence(1.2f);
        ChangeSpeed(0.5f);

        //プロパティの値を格納
        _at = gameObject.GetComponent<PlayerBase>().At;

        //プレハブの取得
        damageDefender = (GameObject)Resources.Load("prefab/Battle/gurd");
    }

    /*回復処理*/
    // Update is called once per frame
    void Update()
    {

    }

    /*攻撃処理*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.transform.tag;
        if (collisionTag == enemyTag || collisionTag == goalTag)
        {
            Vector2 pos;
            foreach (ContactPoint2D point in collision.contacts)
            {
                pos = point.point;
                GameObject effect = Instantiate(damageDefender);
                effect.transform.position = pos;
                collision.gameObject.GetComponent<PlayerBase>().AddDamage(_at);
                Destroy(effect, 0.3f);
            }
        }
    }
}


