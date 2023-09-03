using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Item
    [Serializable]
    public class Item
    {
        public string itemType;
        public string itemName;
        public string itemIcon;
        public string bulletType;
        public float upgradeAmount;
        public int returnExp;
    }

    [Serializable]
    public class ItemData : ILoader<string, Item>
    {
        public List<Item> items = new List<Item>();

        public Dictionary<string, Item> MakeDict()
        {
            Dictionary<string, Item> dict = new Dictionary<string, Item>();
            foreach (Item item in items)
            {
                dict.Add(item.itemName, item);
            }
            return dict;
        }
    }
    #endregion
}