using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : UI_Popup
{
    public GameObject ui;

    public string MainToLoad = "MainMenu";

    public SceneFader sceneFader;

    public GameObject settingUI;

    // Time.timescale = 0이면 Update문 실행안됨
    void Update()
    {
        //if (!GameManager.instance.isLive)
        //{
        //    return;
        //}

        if (SceneFader.isFading)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        Managers.Sound.Play("Effects/UiClickLow", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/UiClickLow", SoundManager.Sound.Effect);

        if (settingUI.activeSelf)
        {
            settingUI.SetActive(false);
            return;
        }

        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            GameManager.instance.Stop();
        }
        else
        {
            GameManager.instance.Resume();
            //GameManager.instance.soundManager.Play("Effects/UiClickLow", SoundManager.Sound.Effect);
        }
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(MainToLoad);
    }

    public void Setting()
    {
        Managers.Sound.Play("Effects/UiClickLow", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/UiClickLow", SoundManager.Sound.Effect);
        settingUI.SetActive(true);
    }
}
