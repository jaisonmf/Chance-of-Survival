using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblin : MonoBehaviour
{
    public enemyController enemyController;
    public enemyGenerator enemyGenerator;
    public CSVReader cSVReader;

    public GameObject go;


    public void Goblin()
    {
        go.GetComponent<enemyController>().eMaxHealth = cSVReader.myEnemyList.enemy[0].maxHealth;
        go.GetComponent<enemyController>().eMinHealth = cSVReader.myEnemyList.enemy[0].maxHealth;

        go.GetComponent<enemyController>().eMaxDamage = cSVReader.myEnemyList.enemy[0].maxDamage;
        go.GetComponent<enemyController>().eMinDamage = cSVReader.myEnemyList.enemy[0].minDamage;

        
    }
}
