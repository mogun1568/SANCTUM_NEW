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

    static Turret tower;

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

            if (!tower)
            {
                Debug.Log("hi");
                return;
            }
            icon.sprite = Managers.Resource.Load<Sprite>($"Icon/{tower.itemData.itemIcon}");
        }
        else
        {
            Inventory.SetActive(true);
            fpsCanvas.SetActive(false);
            //fpsCanvas.enabled = false;
        }

        if (tower)
        {
            ChangeInfo();
        }
    }

    void ChangeInfo()
    {
        float curHP = tower.health;
        float maxHP = 100f;
        hpSlider.value = curHP / maxHP;

        hpText.text = tower.health.ToString("F0") + "/100";
    }

    public static void getTower(Turret _tower)
    {
        tower = _tower;
    }
}
