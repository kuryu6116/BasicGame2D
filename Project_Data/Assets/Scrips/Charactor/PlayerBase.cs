using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//interfaceの実装を時間があったらやる
public class PlayerBase : MonoBehaviour
{
    GameManegement game = new GameManegement();
    float max_hp = 100;
    //現在のHP
    public float Hp
    {
        get { return _hp; }
        set
        {
            _hp = Mathf.Min(max_hp, value);
            if (Hp<=0)
            {
                string my_tag = gameObject.transform.tag;
                game.judgBattle(my_tag,gameObject);
                Destroy(gameObject);
            }
        }
    }
    private float _hp;
    public float At { get; set; } 
    public float Df { get; set; } 
    public float Sp { get; set; } 

    // Start is called before the first frame update
    void Start()
    {
        _hp = max_hp;
    }

    public void AddDamage(float damage)
    {
        //Assert.IsFalse(damage < 0, "ダメージは 0 以上の値を設定してください");
        Hp -= Mathf.Max(0, damage - Df);
        Debug.Log(damage + " ポイントのダメージ受けました");
    }
    public void DirectAttack(float damage)
    {
        Hp -= damage;
        Debug.Log(damage + " ポイントのダメージ受けました");
    }

    public void Heal(float point)
    {
        Hp += point;
        Debug.Log(point+"回復しました");
    }

    public void ChangeAttack(float point)
    {
        At *= point;
    }
    public void ChangeDefence(float point)
    {
        Df *= point;
    }
    public void ChangeSpeed(float point)
    {
        Sp *= point;
    }
}
