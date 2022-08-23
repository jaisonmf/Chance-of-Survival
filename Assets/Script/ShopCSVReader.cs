using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopCSVReader : MonoBehaviour
{
    public TextAsset textAssetData;


    [System.Serializable]
    public class Shop
    {
        public string name;
        public int cost;
        public string description;
    }

    [System.Serializable]
    public class ShopList
    {
        public Shop[] shop;
    }

    public ShopList myShoppingList = new ShopList();

    void Start()
    {
        ReadCSV();
    }


    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 3 - 1;
        myShoppingList.shop = new Shop[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            myShoppingList.shop[i] = new Shop();
            myShoppingList.shop[i].name = data[3 * (i + 1)];
            myShoppingList.shop[i].cost = int.Parse(data[3 * (i + 1) + 1]);
            myShoppingList.shop[i].description = data[3 * (i + 1) + 2];
        }
    }

}
