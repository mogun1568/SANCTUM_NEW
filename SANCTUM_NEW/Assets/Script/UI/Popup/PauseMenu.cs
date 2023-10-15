using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : UI_Popup
{
    //GameObject ui;

    string MainToLoad = "MainMenu";

    //public SceneFader sceneFader;

    //GameObject settingUI;

    //bool isPopup = false, isSettingUI = false;

    enum Buttons
    {
        Button_Setting,
        Button_Continue,
        Button_Retry,
        Button_GoToMenu
    }

    void Awake()
    {
        //ui = Managers.Resource.Instantiate("UI/Popup/PauseMenuUI");
        //settingUI = Managers.Resource.Instantiate("UI/Popup/SettingUI");
        base.Init();

        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.Button_Setting).gameObject, (PointerEventData data) => { Setting(); }, Define.UIEvent.Click);
        BindEvent(GetButton((int)Buttons.Button_Continue).gameObject, (PointerEventData data) => { Managers.Game.Toggle(); }, Define.UIEvent.Click);
        BindEvent(GetButton((int)Buttons.Button_Retry).gameObject, (PointerEventData data) => { Retry(); }, Define.UIEvent.Click);
        BindEvent(GetButton((int)Buttons.Button_GoToMenu).gameObject, (PointerEventData data) => { Menu(); }, Define.UIEvent.Click);
    }

    // Time.timescale = 0이면 Update문 실행안됨
    /*void Update()
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
            Managers.Game.Toggle();
        }
    }

    public void Toggle()
    {
        Managers.Sound.Play("Effects/UiClickLow", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/UiClickLow", SoundManager.Sound.Effect);

        if (isSettingUI)
        {
            Managers.UI.ClosePopupUI();
            isSettingUI = false;
            //settingUI.SetActive(false);
            return;
        }

        //ui.SetActive(!ui.activeSelf);

        if (!isPopup)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("PauseMenuUI");
            isPopup = true;
            Managers.Game.Stop();
        }
        else
        {
            Managers.UI.ClosePopupUI();
            isPopup = false;
            Managers.Game.Resume();
            //GameManager.instance.soundManager.Play("Effects/UiClickLow", SoundManager.Sound.Effect);
        }
    }*/

    public void Retry()
    {
        Managers.Game.Toggle();
        Managers.Scene.sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Managers.Game.Toggle();
        Managers.Scene.sceneFader.FadeTo(MainToLoad);
    }

    public void Setting()
    {
        Managers.Sound.Play("Effects/UiClickLow", Define.Sound.Effect);
        //GameManager.instance.soundManager.Play("Effects/UiClickLow", SoundManager.Sound.Effect);
        //settingUI.SetActive(true);
        Managers.UI.ShowPopupUI<Setting>("SettingUI");
    }
}
