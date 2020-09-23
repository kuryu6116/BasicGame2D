using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatePlayer2 : MonoBehaviour
{
    //inpuFieldの値の取得
    public TextMeshProUGUI sum_text;
    public TextMeshProUGUI _hp,_at,_df,_sp,_sum,_cost;
    float Hp, At, Df, Sp;

    //NameManager
    public int prefab_end = 4;
    public List<int> prefab_list = new List<int>();
    public int sprite_end = 15;
    public List<int> sprite_list = new List<int>();
    public int get_num;

    //登場処理
    private GameObject parentUI;

    GameObject prefab;
    SpriteRenderer srender;
    Sprite[] sprites;
    Sprite sprite_one;

    GameObject player;
    string enemyTag, goalTag, charaName, myTag;

    //ジョブセレクト
    public ToggleGroup toggleGroup;

    //コスト管理
    public GameObject _gameObject;
    GameManegement gameManegement;
    CostManeger costManeger;

    void Start()
    {
        costManeger = _gameObject.GetComponent<CostManeger>();
        gameManegement = _gameObject.GetComponent<GameManegement>();

        parentUI = GameObject.Find("Charactor");
        var enemy_tag = Tool.EnemyTag(gameObject);
        enemyTag = enemy_tag.Item1;
        goalTag = enemy_tag.Item2;
        charaName = enemy_tag.Item4;
        myTag = gameObject.transform.tag;
    }

    void Update()
  {
        Hp =float.Parse(_hp.text);
        At = float.Parse(_at.text);
        Df = float.Parse(_df.text);
        Sp = float.Parse(_sp.text);
        /*入力値チェック
        float zero = 0;
        if (float.TryParse(inputHp.text, out zero))
            _hp = float.Parse(inputHp.text);
        if (float.TryParse(inputAt.text, out zero))
            _at = float.Parse(inputAt.text);
        if (float.TryParse(inputDf.text, out zero))
            _df = float.Parse(inputDf.text);
        if (float.TryParse(inputSp.text, out zero))
            _sp = float.Parse(inputSp.text);
            */
  }

    private string PrefabSetNumber()
    {
        get_num = Tool.RandomNumber(prefab_end, prefab_list);
        string prefab_num = get_num.ToString();
        return prefab_num;
    }

    private string SpriteSetNumber()
    {
        get_num = Tool.RandomNumber(sprite_end, sprite_list);
        string sprite_num = get_num.ToString();
        return sprite_num;
    }

    private GameObject GetPrefab(String sprite_num, String prefab_num)
    {
        prefab = (GameObject)Resources.Load("prefab/player/"+charaName+"0" + prefab_num);
        srender = prefab.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("charactor");
        sprite_one = System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(charaName+"_" + sprite_num));
        srender.sprite = sprite_one;
        return prefab;
    }

    public void OnClick()
    {
        float sum = costManeger.sum_cost;
        float cost = costManeger._now_cost;

        if (sum < cost)
        {
            player = GetPrefab(SpriteSetNumber(), PrefabSetNumber());
            GameObject Instant_player =
                Instantiate(player, player.transform.position, Quaternion.Euler(0f, 180f, 0f));

            Instant_player.transform.SetParent(parentUI.transform, false);

            //ジョブの決定
            string myjob = Tool.GetTaggleName(toggleGroup);
            Tool.JobSelect(Instant_player, myjob);

            gameManegement.DegCost(myTag, sum);

            //inputFieldの値を取得
            SetStatus(Instant_player, Hp, At, Df, Sp);
            
        }
    }

    private void SetStatus(GameObject nowObject,float hp, float at, float df, float sp)
    {
        nowObject.GetComponent<PlayerBase>().Hp = hp;
        nowObject.GetComponent<PlayerBase>().At = at;
        nowObject.GetComponent<PlayerBase>().Df = df;
        nowObject.GetComponent<PlayerBase>().Sp = sp/100;
    }
}



