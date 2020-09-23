using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public static class Tool
{
    //該当のタグの中で一番近いGameObjectを返す
    public static GameObject SearchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;
        float nearDis = 0;

        GameObject targetObj = null;
        //Tagを含むものを全て取得
        GameObject[] tag_object = GameObject.FindGameObjectsWithTag(tagName);
        if (tag_object == null)
        {
            return null;
        }
        //タグで指定されたオブジェクトを配列で取得
        foreach (GameObject obs in tag_object)
        {
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            if (nearDis <= 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                targetObj = obs;
            }
        }
        return targetObj;
    }


    //敵との角度(float)を取得　shotで使用
    public static float GetVector2(GameObject enemy_obj, GameObject my_obj)
    {
        // プレイヤーの相対座標
        float target_x;
        float target_y;
        // 相対座標を計算
        target_x = enemy_obj.transform.position.x - my_obj.transform.position.x;
        target_y = enemy_obj.transform.position.y - my_obj.transform.position.y;
        float rad = Mathf.Atan2(target_y, target_x);
        return rad * Mathf.Rad2Deg;
    }

    //自分に対して引数の二つの内、近いオブジェクトのVeceor2を返す
    public static Vector3 CompareDistance(GameObject myObj, GameObject enemyObj, GameObject goalObj)
    {
        float dis_enemy = Vector3.Distance(myObj.transform.position, enemyObj.transform.position);
        float dis_goal = Vector3.Distance(myObj.transform.position, goalObj.transform.position);
        if (dis_enemy < dis_goal)
        {
            return enemyObj.transform.position;
        }
        else
        {
            return goalObj.transform.position;
        }
    }

    //自分のタグ名から必要な情報を複数取得
    public static Tuple<string, string, string, string> EnemyTag(GameObject player)
    {
        string enemy_tag = player.transform.tag;
        string goal_tag, bullet_name, chara_name;

        switch (enemy_tag)
        {
            case "player1":
            case "player1_Bullet":
            case "player1_SE":
            case "button1":
            case "goal1":
                enemy_tag = "player2";
                goal_tag = "goal1";
                bullet_name = "player2_bullet";
                chara_name = "marisa";
                break;

            case "player2":
            case "player2_Bullet":
            case "player2_SE":
            case "button2":
            case "goal2":
                enemy_tag = "player1";
                goal_tag = "goal2";
                bullet_name = "player1_bullet";
                chara_name = "miku";
                break;

            default:
                Debug.Log("タグの名前がわかりません");
                goal_tag = null;
                bullet_name = null;
                chara_name = null;
                break;
        }
        return Tuple.Create(enemy_tag, goal_tag, bullet_name, chara_name);
    }

    //ラジオボタンから選択した名前を取得
    public static string GetTaggleName(ToggleGroup toggle_group)
    {
        string myToggle = "";
        foreach (Toggle toggle in toggle_group.ActiveToggles())
        {
            if (toggle.isOn)
            {
                myToggle = toggle.name;
            }
        }
        return myToggle;
    }

    //jobの有効化処理
    public static void JobSelect(GameObject Obj, string jobName)
    {
        switch (jobName)
        {
            case "Fighter":
                Obj.GetComponent<Fighter>().enabled = true;
                break;
            case "Defender":
                Obj.GetComponent<Defender>().enabled = true;
                break;
            case "Archar":
                Obj.GetComponent<Archar>().enabled = true;
                break;
            case "Healer":
                Obj.GetComponent<Healer>().enabled = true;
                break;
            default:
                Debug.Log("job選択でエラーが発生いたしました");
                break;
        }
    }
    //1から引数(end)までの重複しない数字を出力し続ける
    public static int RandomNumber(int end, List<int> list)
    {
        int num = UnityEngine.Random.Range(1, end + 1);
        if (list.Count == end) list.RemoveAt(1);
        do
        {
            num = UnityEngine.Random.Range(1, end + 1);
        }
        while (list.Contains(num));
        list.Add(num);
        return num;   
    }
}