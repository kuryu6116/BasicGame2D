using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class tourtrialSceneChange : MonoBehaviour
{ 
    public AudioClip pageSound;
    AudioSource audioSource;
    int count;
    SpriteRenderer srender;
    Sprite[] sprites;
    Sprite sprite_one;


    void Start()
    {
        count = 1;
        spriteJudgh(count);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            count++;
            audioSource.PlayOneShot(pageSound);
            spriteJudgh(count);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (count != 1)
            {
                count--;
                audioSource.PlayOneShot(pageSound);
                spriteJudgh(count);
            }
        }

    }

    void setPrefab(int num)
    {
        srender =gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("tutrial");
        sprite_one = System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals("s"+num));
        srender.sprite = sprite_one;
    }

    void spriteJudgh(int count_num)
    {
        switch (count_num)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                //spriteの変更
                setPrefab(count_num);
                break;
            case 8:
                FadeManager.Instance.LoadScene("Scene/StartScene", 2.0f);
                break;
        }
    }

    /*
     * クリックしたオブジェクトを取得する
    public GameObject getClickObject()
    {
        GameObject result = null;

        if (Input.GetMouseButtonDown(0))
        {
            //クリックされた位置の取得
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(tapPoint);
            //対象のコライダーの取得
            Collider2D collision2D = Physics2D.OverlapPoint(tapPoint);
            if (collision2D)
            {
                result = collision2D.transform.gameObject;
            }
        }

        return result;
    }
    */
}
