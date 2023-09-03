using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Item : UI_Base
{
    public ItemData data;
    Data.Item itemData;

    Image icon;
    TextMeshProUGUI textName;

    public GameObject selectItem;

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        itemData = Managers.Data.ItemDict[gameObject.name];
        icon = GetComponentsInChildren<Image>()[2];
        icon.sprite = Managers.Resource.Load<Sprite>($"Icon/{itemData.itemIcon}");

        BindEvent(gameObject, (PointerEventData data) => { ItemClick(); });
    }

    void LateUpdate()
    {

    }

    public void ItemClick()
    {
        selectItem.GetComponent<SelectItem>().AddItem(itemData.itemName);
        GetComponentInParent<LevelUp>().Hide();
    }

    public void Onclick()
    {
        //selectItem.GetComponent<SelectItem>().AddItem(itemData.itemName);
        //Managers.Inven.AddItem(itemData.itemName);



        //// 아래 수정해야 함 위로 변경함
        //Transform panel = GameObject.Find("SelectItem").transform;

        //// 버튼 프리팹으로부터 새로운 버튼 오브젝트 생성
        //GameObject newButton = Instantiate(UIPrefab);

        //// 생성된 버튼 오브젝트를 패널의 자식으로 추가
        //newButton.transform.SetParent(panel, false);

        //Image newButtonIcon = newButton.GetComponentsInChildren<Image>()[1];
        //newButtonIcon.sprite = icon.sprite;

        //RItem script = newButton.GetComponentInParent<RItem>();
        //// script.data = data;로 해도 됨 (보류)
        //script.LoadData(data);

        //newButton.GetComponent<Button>().onClick.RemoveAllListeners();

        //newButton.GetComponent<Button>().onClick.AddListener(script.Onclick);
    }
}