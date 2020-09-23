using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayer1 : MonoBehaviour
{
    //レベルを決める
    public static float level {get; set;}

    //inpuFieldの値の取得
    //public Text inputHp, inputAt, inputDf, inputSp;
    float _hp, _at, _df, _sp;
    string job_name;

    //NameManager
    int prefab_end = 4;
    List<int> prefab_list = new List<int>();
    int sprite_end = 15;
    List<int> sprite_list = new List<int>();
    int get_num;

    public string myjob;
    float time;
    bool flag;
    bool create_flag;

    //登場処理
    public GameObject parentUI;
    GameObject prefab;
    public SpriteRenderer srender;
    Sprite[] sprites;
    Sprite sprite_one;
    GameObject player;
    string charaName;

    //ジョブセレクト
    public ToggleGroup toggleGroup;

    //コスト管理処置
    public GameManegement gameManegement;

    void Start()
    {
        Debug.Log("敵のステータスは"+level+"です");
        flag = true;

        parentUI = GameObject.Find("Charactor");
        //var enemy_tag = Tool.EnemyTag(gameObject);
        charaName = "marisa";

        _hp = level;
        _at = level;
        _df = level;
        _sp = level;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (flag && time>=3)
        {
            time -= 3;
            flag = false;
            OnClick();
        }

        if (time >= 5)
        {
            time -= 5;

            if (CreateFlag("player1"))
            {
                OnClick();
            }
        }
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

   private GameObject GetPrefab(string sprite_num, string prefab_num)
    {
        sprite_num = Tool.RandomNumber(15, sprite_list).ToString();
        prefab = (GameObject)Resources.Load("prefab/player/"+charaName+"0"+prefab_num);
        srender = prefab.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("charactor");
        sprite_one = System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(charaName + "_" + sprite_num));
        srender.sprite = sprite_one;
        return prefab;
    }

    public void OnClick()
    {
        //float sum = _hp + _at + _df + _sp;
        //if(sum<gameManegement.cost_p1)
        //{
            player = GetPrefab(SpriteSetNumber(), PrefabSetNumber());
            GameObject Instant_player =
                Instantiate(player, player.transform.position, Quaternion.identity);
            Instant_player.transform.SetParent(parentUI.transform, false);

            //ジョブの決定
            //myjob = Tool.GetTaggleName(toggleGroup);
            myjob = JobNameSelect();
            Tool.JobSelect(Instant_player, myjob);

            //ランダムのコスト機能の実装
            //if(inputHp.text=="" || inputAt.text=="" || inputDf.text=="" || inputSp.text == "")
            //{
            //    RandomSetStatus(Instant_player,cost_p1);
            //}
            //else
            //{
            //inputFieldの値を取得
            SetStatus(Instant_player, _hp, _at, _df, _sp);
            //}
        //}
    }

    private void SetStatus(GameObject nowObject, float hp, float at, float df, float sp)
    {
        nowObject.GetComponent<PlayerBase>().Hp = hp;
        nowObject.GetComponent<PlayerBase>().At = at;
        nowObject.GetComponent<PlayerBase>().Df = df;
        nowObject.GetComponent<PlayerBase>().Sp = sp/120;
    }
    //コストの値を各ステータスに割り振る
    //void RandomSetStatus(GameObject game,float cost)
    //{
    //    int count=1;
    //    List<float> list = new List<float> { 0, 0, 0, 0 };
    //    for (;cost>0;cost--)
    //    {
    //        list[count] ++;
    //        count++;
    //        if (count == 5) count -= 4;
    //    }
    //    //コスト処理
    //    gameManegement.DegCost(cost);
    //    //ステータスのセット
    //    SetStatus(game, list[1], list[2], list[3],list[4]);
    //}
    string JobNameSelect()
    {
        int ran = Random.Range(1, 4);
        switch(ran)
        {
            case 1:
                job_name = "Fiter";
                break;
            case 2:
                job_name = "Defender";
                break;
            case 3:
                job_name = "Archar";
                break;
        }
        return job_name;
    }
    int TagCount(string tag_name)
    {
        //Tagを含むものを全て取得
        GameObject[] tag_object = GameObject.FindGameObjectsWithTag(tag_name);
        //タグで指定されたオブジェクトの数を取得
        int count = tag_object.Length;
        return count;
    }
    bool CreateFlag(string tag_name)
    {
        if (TagCount(tag_name)>3)
        {
            create_flag = false;
        }
        else
        {
            create_flag = true;
        }
        return create_flag;
    }
    public static void setLevel(float num)
    {
        level = num;
    } 
}