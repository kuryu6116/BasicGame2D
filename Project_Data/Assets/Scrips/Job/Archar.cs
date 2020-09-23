using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ショットの機能
public class Archar: PlayerBase
{
    [SerializeField]
    float bullet_at;
    [SerializeField]
    float bullet_sp;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject nearObj;
    [SerializeField]
    GameObject nearGoalObj;

    float timeManeger;

    string enemyTag, goalTag, charaName;

    // Start is called before the first frame update
    void Start()
    {
        //Job補正
        ChangeSpeed(0.01f);

        bullet_at = gameObject.GetComponent<PlayerBase>().At / 4;
        bullet_sp = gameObject.GetComponent<PlayerBase>().Sp * 250f;

        var enemy_tag = Tool.EnemyTag(gameObject);
        enemyTag = enemy_tag.Item1;
        goalTag = enemy_tag.Item2;
        charaName = enemy_tag.Item4;

        //プレハブの取得
        bulletPrefab = (GameObject)Resources.Load("prefab/Battle/BulletPrefab_" + charaName);
    }

    // Update is called once per frame
    void Update()
    {
        nearObj = Tool.SearchTag(gameObject, enemyTag);
        nearGoalObj = Tool.SearchTag(gameObject, goalTag);

        //攻撃の時間管理
        timeManeger += Time.deltaTime;
        if (timeManeger >= 5.0f)
        {
            timeManeger -= 5;
            if (nearGoalObj != null)
            {    
                ShotBullet(bullet_at, bullet_sp, GetAngleToEnemy(nearObj,nearGoalObj));
            }
            else
            {
                ShotBullet(bullet_at, bullet_sp, GetAngleToEnemy(nearGoalObj,nearGoalObj));
            }
        }
    }

    //敵のラジアンを算出
    private float GetAngleToEnemy(GameObject enemy_obj,GameObject goal_obj)
    {
        // プレイヤーの相対座標
        float target_x;
        float target_y; 
        float rad;

        // 相対座標を計算
        if (nearGoalObj != null || nearObj != null)
        {
            if (nearObj == null)
            {
                target_x = goal_obj.transform.position.x - transform.position.x;
                target_y = goal_obj.transform.position.y - transform.position.y;
            }
            else
            {
                target_x = enemy_obj.transform.position.x - transform.position.x;
                target_y = enemy_obj.transform.position.y - transform.position.y;
            }

            rad = Mathf.Atan2(target_x, target_y);
            return rad;
        }
        return 0;
    }

    //敵の角度に攻撃をだす
    private void ShotBullet(float bulletAt, float bulletSp, float bulletAngle)
    {
        //敵が全滅してるかで処理を分岐させる
        if (nearGoalObj != null || nearObj != null)
        {
            GameObject shot_obj = Instantiate(bulletPrefab);

            //shotを本体から発射する
            shot_obj.transform.position = gameObject.transform.position;

            //近くの敵かゴールの傾きを計算して発射物の傾きを変更する
            if (nearObj == null)
            {
                shot_obj.transform.Rotate(new Vector3(0, 0, Tool.GetVector2(nearGoalObj, shot_obj)));
            }
            else
            {
                shot_obj.transform.Rotate(new Vector3(0, 0, Tool.GetVector2(nearObj, shot_obj)));
            }

            //PlayerのステータスをBulletに与える
            shot_obj.GetComponent<Bullet>().attack = bulletAt;
            shot_obj.GetComponent<Bullet>().speed = bulletSp;
            shot_obj.GetComponent<Bullet>().angel = bulletAngle;
        }
        else
        {
            //何もしない
        }
    }
}