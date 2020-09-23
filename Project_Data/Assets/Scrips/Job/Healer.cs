using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : PlayerBase
{
    float timeManeger, _at;

    string enemyTag, goalTag, myTag;
    GameObject damageFiter, healEffect;

    // Start is called before the first frame update
    void Start()
    {
        //Job補正
        ChangeAttack(0.3f);
        ChangeSpeed(0.3f);

        //プロパティの値を格納
        _at = gameObject.GetComponent<PlayerBase>().At;

        //タグ取得
        var enemy_tag = Tool.EnemyTag(gameObject);
        enemyTag = enemy_tag.Item1;
        goalTag = enemy_tag.Item2;
        myTag = gameObject.transform.tag;

        //プレハブの取得
        damageFiter = (GameObject)Resources.Load("prefab/Battle/burn");
        healEffect = (GameObject)Resources.Load("prefab/Battle/eff_heal");
    }

    /*回復処理*/
    // Update is called once per frame
    void Update()
    {
        timeManeger += Time.deltaTime;
        if (timeManeger >= 4)
        {
            timeManeger -= 4;
            GameObject[] tag_objs = GameObject.FindGameObjectsWithTag(myTag);
            foreach (GameObject obj in tag_objs)
            {
                //演出を作成する
                GameObject heal_effect = Instantiate(healEffect);
                //回復の音声

                //演出を移動させる
                Vector3 pos = (Vector3)obj.transform.position;
                pos.y += 1;
                pos.x += -1;
                pos.z += 10;
                heal_effect.transform.position = pos;
                heal_effect.transform.Rotate(new Vector3(0,0,0));
                //回復処理を入れる
                obj.gameObject.GetComponent<PlayerBase>().Heal(_at);
            }
        }
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
                GameObject effect = Instantiate(damageFiter);
                effect.transform.position = pos;
                collision.gameObject.GetComponent<PlayerBase>().AddDamage(_at);
                Destroy(effect, 0.3f);
            }
        }
    }

}


