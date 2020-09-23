using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Result : MonoBehaviour
{
    //勝敗の表示
    string result;
    public GameObject win_text;
    public GameObject lose_text;
    float time;
    //フェード系
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject character;

    bool isFadeOut = true;
    float red, green, blue, alfa;
    public GameObject panel;
    Image fadeImage;
    float fadeSpeed;

    bool music_flag = true;
    public AudioClip winAudio;
    public AudioClip loseAudio;
    AudioSource audioSource;


    // Start is called before the first frame update
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //勝敗の文字表示
        result = GameManegement.getWinner();
        fadeImage = panel.GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
        fadeSpeed = 0.008f;
        panel.SetActive(true);
    }
    private void Update()
    {
        if (music_flag)
        {
            StartMusic(result);
        }   

        time += Time.deltaTime;
        if (time >= 2)
        {
            if (result == "Player2")
            {
                win_text.SetActive(true);
            }
            else
            {
                lose_text.SetActive(true);
            }
            button1.SetActive(true);
            button2.SetActive(true);
            button3.SetActive(true);
            character.SetActive(false);

        }

        if (isFadeOut)
        {
            StartFadeOut();
        }


    }
    void StartFadeOut()
    {
        //fadeImage.enabled = true;
        alfa += fadeSpeed;
        SetAlpha(); 
        if (alfa >= 0.5f)
        {             
            isFadeOut = false;
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }

    void StartMusic(string winner)
    {
        if(winner == "Player1")
        {
            audioSource.PlayOneShot(loseAudio);
        }
        else
        {
            audioSource.PlayOneShot(winAudio);
        }
        music_flag = false;
    }

}
