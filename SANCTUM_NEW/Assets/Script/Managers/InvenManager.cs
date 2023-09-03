using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvenManager : UI_Base
{
    Dictionary<string, int> invenDict = new Dictionary<string, int>();

    enum GameObjects
    {
        SelectItem,
        StandardTower,
        Firet,
        Ice,
        Water,
        Lighting,
        Dirt,
        DamageUp,
        RangeUp,
        FireRateUp
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
        LifeRecoveryCountText
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));

        /*GameObject inventory = Get<GameObject>((int)GameObjects.SelectItem);
        foreach (Transform child in inventory.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }*/

        // ���� �κ��丮 ������ �����ؼ�
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
            // �̹� �ش� �������� �κ��丮�� �ִ� ���
            invenDict[itemName]++; // ������ ������ 1 ������Ŵ
            UpdateItemUI(itemName, invenDict[itemName]); // ������ UI ������Ʈ
        }
        else
        {
            // ���ο� �������� ȹ���� ���
            invenDict.Add(itemName, 1); // �������� ��ųʸ��� �߰��ϰ� ������ 1�� �ʱ�ȭ��
            //CreateItemUI(item); // ������ UI ����
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
        // ������ UI�� ã�Ƽ� ������ ������Ʈ��
        if (Enum.TryParse(itemName + "CountText", out Texts enumValue))
        {
            GetText((int)enumValue).text = itemCount.ToString();
        }
        else
        {
            Debug.LogError($"Failed to parse enum value for itemName: {itemName}");
        }

        //Transform itemUI = transform.Find(itemName);
        //TextMeshProUGUI itemText = itemUI.GetComponentInChildren<TextMeshProUGUI>();
        //itemText.text = itemCount.ToString();
    }

    void CreateItemUI(string itemName)
    {
        //GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(transform, itemName).gameObject;
        //UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();

        if (Enum.TryParse(itemName, out GameObjects enumValue))
        {
            Transform itemUI = GetObject((int)enumValue).transform;
            itemUI.SetAsLastSibling();
            itemUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError($"Failed to parse enum value for itemName: {itemName}");
        }
        //Transform itemUI = transform.Find(itemName.ToString());
        //itemUI.SetAsLastSibling();
        //itemUI.gameObject.SetActive(true);
    }
}
