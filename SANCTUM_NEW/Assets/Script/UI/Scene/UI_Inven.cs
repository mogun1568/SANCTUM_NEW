using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    Dictionary<string, int> invenDict = new Dictionary<string, int>();

    enum GameObjects
    {
        Inventory
    }

    enum Texts
    {
        StandardTowerCountText,
        FireCountText,
        IceCountText,
        WaterCountText,
        LightingCountText,
        DirtCountText,
        DamageUpCountText,
        RangeUpCountText,
        FireRateUpCountText,

    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));

        /*GameObject inventory = Get<GameObject>((int)GameObjects.Inventory);
        foreach (Transform child in inventory.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }*/

        // 실제 인벤토리 정보를 참고해서
        for (int i = 0; i < 3; i++)
        {
            AddItem("StandardTower");

            //GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(inventory.transform).gameObject;
            //UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            //invenItem.SetInfo($"Sword{i}");
        }
    }

    public void AddItem(string itemName)
    {
        if (invenDict.ContainsKey(itemName))
        {
            // 이미 해당 아이템이 인벤토리에 있는 경우
            invenDict[itemName]++; // 아이템 개수를 1 증가시킴
            UpdateItemUI(itemName, invenDict[itemName]); // 아이템 UI 업데이트
        }
        else
        {
            // 새로운 아이템을 획득한 경우
            invenDict.Add(itemName, 1); // 아이템을 딕셔너리에 추가하고 개수를 1로 초기화함
            //CreateItemUI(item); // 아이템 UI 생성
            CreateItemUI(itemName);
        }
    }

    public void useItem(string itemName)
    {
        invenDict[itemName]--;
        if (invenDict[itemName] == 0)
        {
            invenDict.Remove(itemName);
            Managers.Select.DestroyItemUI();
        }
        else
        {
            UpdateItemUI(itemName, invenDict[itemName]);
        }
    }

    void UpdateItemUI(string itemName, int itemCount)
    {
        // 아이템 UI를 찾아서 개수를 업데이트함
       // GetText((int)Texts.ScoreText).text = $"Score : {_score}";

        Transform itemUI = transform.Find(itemName);
        TextMeshProUGUI itemText = itemUI.GetComponentInChildren<TextMeshProUGUI>();
        itemText.text = itemCount.ToString();
    }

    void CreateItemUI(string itemName)
    {
        GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(transform, itemName).gameObject;
        UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();


        Transform itemUI = transform.Find(itemName.ToString());
        itemUI.SetAsLastSibling();
        itemUI.gameObject.SetActive(true);
    }
}
