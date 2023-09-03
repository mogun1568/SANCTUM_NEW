using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public enum InfoType { Time, Live, Exp }
    public InfoType type;

    Slider mySlider;
    TextMeshProUGUI myText;

    void Awake()
    {
        mySlider = GetComponent<Slider>();
        myText = GetComponent<TextMeshProUGUI>();
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        switch (type)
        {
            case InfoType.Time:
                float gameTime = GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(gameTime / 60);
                int sec = Mathf.FloorToInt(gameTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Live:
                int live = GameManager.instance.Lives;
                myText.text = live.ToString();
                break;
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp;
                mySlider.value = curExp / maxExp;
                break;

        }
    }
}
