using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ogre : MonoBehaviour
{
    public enemyController enemyController;
    public enemyGenerator enemyGenerator;
    public CSVReader cSVReader;

    public GameObject go;


    public void Ogre()
    {
        go.GetComponent<enemyController>().eMaxHealth = cSVReader.myEnemyList.enemy[1].maxHealth;
        go.GetComponent<enemyController>().eMinHealth = cSVReader.myEnemyList.enemy[1].minHealth;

        go.GetComponent<enemyController>().eMaxDamage = cSVReader.myEnemyList.enemy[1].maxDamage;
        go.GetComponent<enemyController>().eMinDamage = cSVReader.myEnemyList.enemy[1].minDamage;


    }
}
