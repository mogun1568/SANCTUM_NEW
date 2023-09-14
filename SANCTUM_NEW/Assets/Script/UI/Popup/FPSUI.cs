using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Data;

public class FPSUI : MonoBehaviour
{
    [SerializeField] GameObject fpsCanvas;
    [SerializeField] GameObject Inventory;

    [SerializeField] Slider hpSlider;
    [SerializeField] TextMeshProUGUI hpText;

    //static Turret tower;
    static TowerControl towerControl;

    public Image icon;

    void Start()
    {
        fpsCanvas.SetActive(false);
        //fpsCanvas.enabled = false;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        if (GameManager.instance.isFPM == true)
        {
            Inventory.SetActive(false);
            fpsCanvas.SetActive(true);
            //fpsCanvas.enabled = true;

            /*if (!tower)
            {
                Debug.Log("hi");
                return;
            }*/
            icon.sprite = Managers.Resource.Load<Sprite>($"Icon/{towerControl.itemData.itemIcon}");
        }
        else
        {
            Inventory.SetActive(true);
            fpsCanvas.SetActive(false);
            //fpsCanvas.enabled = false;
        }

        if (towerControl)
        {
            ChangeInfo();
        }
    }

    void ChangeInfo()
    {
        float curHP = towerControl._stat.HP;
        float maxHP = towerControl._stat.MaxHp;
        hpSlider.value = curHP / maxHP;

        hpText.text = towerControl._stat.HP.ToString("F0") + "/100";
    }

    public static void getTower(TowerControl _tower)
    {
        towerControl = _tower;
    }
}
