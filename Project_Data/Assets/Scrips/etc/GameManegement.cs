using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManegement : MonoBehaviour
{

    Button button1;
    Button button2;
    [SerializeField]
    int button_count1;

    [SerializeField]
    int button_count2;
    int old_button_count2;

    float time_manegement;
    GameObject burn_effect;

    public float cost_p1 { get; set; }
    public float cost_p2 { get; set; }

    //勝利者をシーン間で保管する
    public static string winner;
    GameObject result_parent;
    [SerializeField]
    GameObject result_panel;
    AudioSource game_music;

    // Start is called before the first frame update
    void Start()
    {
        //コストの値をセット
        gameObject.GetComponent<GameManegement>().cost_p1 = 30;
        gameObject.GetComponent<GameManegement>().cost_p2 = 30;
        gameObject.GetComponent<GameManegement>().button_count2 = 0;

        //ボタンの取得
        //button1 = GameObject.Find("CreateButton1").GetComponent<Button>();
        button2 = GameObject.Find("CreateButton2").GetComponent<Button>();
        burn_effect = (GameObject)Resources.Load("prefab/Battle/burn_end");
        
    }

    // Update is called once per frame
    void Update()
    {
        //登場数でボタンの管理
        button_count2 = TagCount("player2");
        if (old_button_count2 != button_count2)
        {
            ButtonStatus(button_count2);
            old_button_count2 = button_count2;
        }

        /*コスト関連処理*/
        time_manegement += Time.deltaTime;
        if (time_manegement >= 2)
        {
            time_manegement -= 2;
            cost_p1 += 1;
            cost_p2 += 1;
        }
        /*ボタン処理*/
        //field上のプレイヤーの数を保管する


        //ボタンのオンオフを切り替える
        //ButtonStatus(button1,button_count1,cost_p1);
        //ButtonStatus(button2, button_count2,cost_p2);
    }

    //butttonのオンオフを決める
    public void ButtonStatus(int tag_num)
    {
        if (button_count2 > 3)
        {
            button2.interactable = false;
        }
        else
        {
            button2.interactable = true;
        }
    }

    /*
    public void ButtonStatus(string tag_name)
    {
        int count;
        Button button;
        if (tag_name == "button2" || tag_name == "player2")
        {
            count = TagCount("player2");
            button = button2;
        }
        else
        {
            count = TagCount("player1");
            button = button1;
        }
        if (count >= 4)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
    */

    //対象のタグの数を数える
    int TagCount(string tag_name)
    {
        //Tagを含むものを全て取得
        GameObject[] tag_object = GameObject.FindGameObjectsWithTag(tag_name);

        //タグで指定されたオブジェクトの数を取得
        int count = tag_object.Length;
        return count;
    }

    //勝利者を変数に格納する
    public void judgBattle(string desTag,GameObject gameObj)
    {
        switch (desTag)
        {
            case "goal1":
            case "goal2":

                if (desTag == "goal1")
                {
                    winner = "Player1";
                }
                else
                {
                    winner = "Player2";
                }
                //リザルトの表示
                result_parent = GameObject.Find("Canvas");
                result_panel = result_parent.transform.Find("GameClear").gameObject;
                result_panel.SetActive(true);
                //画面遷移処理
                //FadeManager.Instance.LoadScene("Scene/GameClear", 4.0f);
                //音を止める
                game_music = GameObject.Find("GameManeger").GetComponent<AudioSource>();
                game_music.Stop();
                break;

            case "player1":
            case "player2":
                AddCost(desTag);
                break;
        }
    }

    /*コスト管理メソッド*/
    //コストの増加処理
    void AddCost(string playerTag)
    {
        if (playerTag == "player1")
        {
            cost_p2 += 1;
        }
        else
        {
            cost_p1 += 1;
        }
    }

    public void DegCost(string tag,float point)
    {
        if (tag == "button1")
            cost_p1 -= point;
        else
            cost_p2 -= point;
    }

    //勝利画面で勝利者を表示
    public static string getWinner()
    {
        return winner;
    }
}
