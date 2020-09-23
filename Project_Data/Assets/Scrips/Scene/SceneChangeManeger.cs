using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeManeger : MonoBehaviour
{
    public AudioClip audioClip;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnclickTutorial()
    {
        audioSource.PlayOneShot(audioClip);
        FadeManager.Instance.LoadScene("Scene/Tutorial",2.0f);
    }

    public void OnclickLevel1()
    {
        audioSource.PlayOneShot(audioClip);
        CreatePlayer1.setLevel(2f);
        FadeManager.Instance.LoadScene("Scene/MainDisplay", 2.0f);
    }

    public void OnclickLevel2()
    {
        audioSource.PlayOneShot(audioClip);
        CreatePlayer1.setLevel(2.5f);
        FadeManager.Instance.LoadScene("Scene/MainDisplay", 2.0f);
    }

    public void OnclickLevel3()
    {
        audioSource.PlayOneShot(audioClip);
        CreatePlayer1.setLevel(3f);
        FadeManager.Instance.LoadScene("Scene/MainDisplay", 2.0f);
    }

    public void Exit()
    {
        audioSource.PlayOneShot(audioClip);
        Application.Quit();
    }

    public void OnclicRestart()
    {
        audioSource.PlayOneShot(audioClip);
        FadeManager.Instance.LoadScene("Scene/MainDisplay", 2.0f);
    }

    public void OnclickStart()
    {
        audioSource.PlayOneShot(audioClip);
        FadeManager.Instance.LoadScene("Scene/StartScene", 2.0f);
    }
}
