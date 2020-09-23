using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /*Shotクラスで値がセットされる*/
    //速度
    public float speed;
    //発射角度
    public float angel;
    //弾の威力
    public float attack;
    //SEの設定
    public AudioClip archer_damege_se;

    //Bulletの寿命
    float limitTime = 5.0f;
    //時間管理
    float time;

    //移動量
    Vector3 vec;
    //移動量格納
    float move_x, move_y;

    string enemyTag, goalTag ,bulletTag;
    GameObject damage_archer;

    // Start is called before the first frame update
    void Start()
    {   
        var enemy_tag = Tool.EnemyTag(gameObject);
        enemyTag = enemy_tag.Item1;
        goalTag = enemy_tag.Item2;
        bulletTag = enemy_tag.Item3;

        damage_archer = (GameObject)Resources.Load("prefab/Battle/burn_small");

    }
    void Update()
    {
        //速度と角度から移動量

        if (enemyTag == "player2")
        {
            float rad = angel * Mathf.Deg2Rad;
            move_x = Mathf.Cos(rad) * speed * Time.deltaTime;
            move_y = Mathf.Sin(rad) * speed * Time.deltaTime;
            vec = new Vector3(move_x, move_y, 0);
        }
        else
        {
            float rad = angel * Mathf.Deg2Rad;
            move_x = Mathf.Cos(rad) * speed * Time.deltaTime;
            move_y = Mathf.Sin(rad) * speed * Time.deltaTime;
            vec = new Vector3(move_x, move_y, 0);
        }
        
        transform.Translate(vec);

        //時間で消滅する処理
        time += Time.deltaTime;

        if (time > limitTime)
        {
            Destroy(this.gameObject);
            time -= 5;
        }
    }

    //消滅処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.transform.tag;
        if (collisionTag == enemyTag || collisionTag == bulletTag || collisionTag == goalTag)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = transform.up / 10;

            Vector2 pos = collision.ClosestPoint(gameObject.transform.position);
            GameObject effect = Instantiate(damage_archer);
            effect.transform.position = pos;
            //音声を鳴らす
            AudioSource.PlayClipAtPoint(gameObject.GetComponent<AudioSource>().clip, pos);
            //ダメージ処理
            collision.gameObject.GetComponent<PlayerBase>().DirectAttack(attack);
           
            Destroy(gameObject);
        }
    }
}