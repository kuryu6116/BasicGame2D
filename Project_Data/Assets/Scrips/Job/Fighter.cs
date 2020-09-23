using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fiterのクラス機能
public class Fighter : PlayerBase
{

    float _hp, _at, _df, _sp;
    string enemyTag, goalTag, charaName;
    
    GameObject damageFiter;

    // Start is called before the first frame update
    void Start()
    {
        //Job補正
        ChangeAttack(1.5f);
        ChangeDefence(0.5f);
        ChangeSpeed(1.2f);

        //プロパティの値を格
        _at = gameObject.GetComponent<PlayerBase>().At;

        var enemy_tag = Tool.EnemyTag(gameObject);
        enemyTag = enemy_tag.Item1;
        goalTag = enemy_tag.Item2;
        charaName = enemy_tag.Item4;

        //プレハブの取得
        damageFiter = (GameObject)Resources.Load("prefab/Battle/burn");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.transform.tag;
        if (collisionTag == enemyTag  || collisionTag == goalTag)
        {
            Vector2 pos;
            foreach (ContactPoint2D point in collision.contacts)
            {
                pos = point.point;
                GameObject effect = Instantiate(damageFiter);
                effect.transform.position = pos;
                collision.gameObject.GetComponent<PlayerBase>().AddDamage(_at);
                Destroy(effect,0.3f);
            }
        }
    }
}

    

