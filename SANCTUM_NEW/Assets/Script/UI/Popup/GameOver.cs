using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : UI_Popup
{
    public string MainToLoad = "MainMenu";

    public SceneFader sceneFader;

    enum Texts
    {
        Rounds
    }

    void Awake()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));

        Managers.Sound.Play("Bgms/cinematic-melody-main-9785", Define.Sound.Bgm);
        GetText((int)Texts.Rounds).text = Managers.Game.Rounds.ToString(); ;
    }

    public void Retry()
    {
        Managers.Sound.Play("Effects/UiClickLow", Define.Sound.Effect);
        Managers.Game.Resume();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Managers.Sound.Play("Effects/UiClickLow", Define.Sound.Effect);
        Managers.Game.Resume();
        sceneFader.FadeTo(MainToLoad);
    }
}
