using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Data;

public class FPSUI : UI_Popup
{
    //[SerializeFieldGameObject fpsCanvas;
    //[SerializeField] GameObject Inventory;

    //[SerializeField] Slider hpSlider;
    //[SerializeField] TextMeshProUGUI hpText;

    //static Turret tower;
    static public TowerControl towerControl;

    //public Image icon;

    //bool invenExecuted = true;
    //bool fpsExecuted = false;

    enum GameObjects
    {
        HpBar
    }
    enum Texts
    {
        HP
    }
    enum Images
    {
        Icon
    }

    void OnEnable()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
 
        GetImage((int)Images.Icon).sprite = Managers.Resource.Load<Sprite>($"Icon/{towerControl.itemData.itemIcon}");
    }

    void Update()
    {
        if (!Managers.Game.isLive)
        {
            return;
        }

        //if (Managers.Game.isFPM == true)
        //{
        //    if (fpsExecuted) {
        //        return;
        //    }
        //    invenExecuted = false;

        //    Bind<GameObject>(typeof(GameObjects));
        //    Bind<TextMeshProUGUI>(typeof(Texts));
        //    Bind<Image>(typeof(Images));
        //    //Inventory.SetActive(false);
        //    //fpsCanvas.SetActive(true);
        //    //fpsCanvas.enabled = true;

        //    /*if (!tower)
        //    {
        //        Debug.Log("hi");
        //        return;
        //    }*/
        //    GetImage((int)Images.Icon).sprite = Managers.Resource.Load<Sprite>($"Icon/{towerControl.itemData.itemIcon}");
        //    //icon.sprite = Managers.Resource.Load<Sprite>($"Icon/{towerControl.itemData.itemIcon}");

        //    fpsExecuted = true;
        //}
        //else
        //{
        //    if (invenExecuted)
        //    {
        //        return;
        //    }
        //    fpsExecuted = false;

        //    //Inventory.SetActive(true);
        //    //fpsCanvas.SetActive(false);
        //    //fpsCanvas.enabled = false;

        //    invenExecuted = true;
        //}

        if (towerControl)
        {
            ChangeInfo();
        }
    }

    void ChangeInfo()
    {
        float curHP = towerControl._stat.HP;
        float maxHP = towerControl._stat.MaxHp;

        GetObject((int)GameObjects.HpBar).GetComponent<Slider>().value = curHP / maxHP;
        GetText((int)Texts.HP).text = towerControl._stat.HP.ToString("F0") + "/100";
        //hpSlider.value = curHP / maxHP;
        //hpText.text = towerControl._stat.HP.ToString("F0") + "/100";
    }

    static public void GetTower(TowerControl _tower)
    {
        towerControl = _tower;
    }
}
