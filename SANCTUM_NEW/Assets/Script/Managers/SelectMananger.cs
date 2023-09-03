using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectMananger
{
    
    //GameObject selectItem;

    GameObject itemUI;

    [HideInInspector] public Node selectedNode;

    Data.Item itemData;

    //ItemData data;

    //public NodeUI nodeUI;

    // node 선택 함수



    public void SelectNode(Node node)
    {
        if (selectedNode == node)   // 선택한 노드를 또 선택하면
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        //itemUI = null;

        //nodeUI.SetTarget(node);
    }

    // node 선택 해제 함수
    public void DeselectNode()
    {
        selectedNode = null;
        //nodeUI.Hide();
    }

    public void SelectItemToUse(GameObject item, Data.Item _itemData)
    {
        if (itemUI == item)   // 선택한 아이템을 또 선택하면
        {
            Debug.Log("Deselect Item");
            Clear();
            return;
        }

        Clear();
        itemData = _itemData;

        Debug.Log($"{itemData.itemName} Selected");
        itemUI = item;
        DeselectNode();
    }

    public Data.Item getItemData()
    {
        return itemData;
    }

    public void itemUITextDecrease()
    {
        //Managers.Inven.useItem(itemData.itemName);
        itemUI.GetComponentInParent<SelectItem>().useItem(itemData.itemName);
        //selectItem.GetComponent<SelectItem>().useItem(data.itemId);
        Clear();
    }

    public void DestroyItemUI()
    {
        //Managers.Resource.Destroy(itemUI);
        itemUI.SetActive(false);
        Clear();
    }

    public void Clear()
    {
        itemUI = null;
        itemData = null;
    }
}
