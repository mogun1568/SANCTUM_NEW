using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string GameToLoad = "GamePlay";

    public SceneFader sceneFader;

    public GameObject settingUI;

    void Start()
    {
        Managers.Sound.Play("Bgms/bgm2", Define.Sound.Bgm);
        //GameManager.instance.soundManager.Play("Bgms/bgm2", SoundManager.Sound.Bgm);
        SceneFader.isFading = false;
    }

    public void Play()
    {
        Managers.Sound.Play("Effects/UiClickLow", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/UiClickLow", SoundManager.Sound.Effect);
        sceneFader.FadeTo(GameToLoad);
    }

    public void Setting()
    {
        Managers.Sound.Play("Effects/UiClickLow", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/UiClickLow", SoundManager.Sound.Effect);
        settingUI.SetActive(true);
    }

    public void Quit()
    {
        Managers.Sound.Play("Effects/UiClickLow", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/UiClickLow", SoundManager.Sound.Effect);
        Debug.Log("Exciting...");
        Application.Quit();
    }
}
