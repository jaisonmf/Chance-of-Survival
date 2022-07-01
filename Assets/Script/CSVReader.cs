using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    public TextAsset textAssetData;


    [System.Serializable]
    public class Enemy
    {
        public string name;
        public int maxHealth;
        public int minHealth;
        public int maxDamage;
        public int minDamage;
    }

    [System.Serializable]
    public class EnemyList
    {
        public Enemy[] enemy;
    }

    public EnemyList myEnemyList = new EnemyList();

    void Start()
    {
        ReadCSV();
    }


    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 5 - 1;
        myEnemyList.enemy = new Enemy[tableSize];

        for(int i = 0; i < tableSize; i++)
        {
            myEnemyList.enemy[i] = new Enemy();
            myEnemyList.enemy[i].name = data[5 * (i + 1)];
            myEnemyList.enemy[i].maxHealth = int.Parse(data[5 * (i + 1) + 1]);
            myEnemyList.enemy[i].minHealth = int.Parse(data[5 * (i + 1) + 2]);
            myEnemyList.enemy[i].maxDamage = int.Parse(data[5 * (i + 1) + 3]);
            myEnemyList.enemy[i].minDamage = int.Parse(data[5 * (i + 1) + 4]);
        }
    }

}
