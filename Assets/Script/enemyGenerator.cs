using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGenerator : MonoBehaviour
{
    public gameController gameController;
    public enemyController enemyController;

    public int amount;
    public GameObject goblin;
    public GameObject boss;
    public GameObject go;
    public GameObject Parent;


    public List<GameObject> list = new List<GameObject>();

    public void EnemyGeneration()
    {
        amount = Random.Range(1, 1);
        {
            if(gameController.waveCount != 10)
            {
                for (int i = 0; i < amount; i++)
                {
                    go = Instantiate(goblin, new Vector2((Screen.width / (amount + 1)) * (i + 1), 15), Quaternion.identity);
                    go.transform.SetParent(Parent.transform, false);
                    list.Add(go);
                    EnemyStats();
                    go.GetComponent<enemyController>().count = i;
                    enemyController.alive = true;
                }
            }
            else if (gameController.waveCount == 10)
            {
                go = Instantiate(boss, new Vector2((Screen.width / (amount + 1)) * (1), 15), Quaternion.identity);
                go.transform.SetParent(Parent.transform, false);
                list.Add(go);
                BossStats();
                go.GetComponent<enemyController>().count = 0;
                enemyController.alive = true;
            }
           

        }
    }



    public void EnemyStats()
    {
        //Calculates states
        if(gameController.waveCount <= 5)
        {
            go.GetComponent<enemyController>().eMaxHealth = Random.Range(25, 75);
            go.GetComponent<enemyController>().eMaxDamage = Random.Range(5, 10);
            go.GetComponent<enemyController>().eMinDamage = Random.Range(0, 5);
            go.GetComponent<enemyController>().eHealth = go.GetComponent<enemyController>().eMaxHealth;
            go.GetComponent<enemyController>().eMaxDefence = 50;
        }
        else if (gameController.waveCount >= 6 && gameController.waveCount != 10)
        {
            go.GetComponent<enemyController>().eMaxHealth = Random.Range(35, 85);
            go.GetComponent<enemyController>().eMaxDamage = Random.Range(7, 12);
            go.GetComponent<enemyController>().eMinDamage = Random.Range(2, 7);
            go.GetComponent<enemyController>().eHealth = go.GetComponent<enemyController>().eMaxHealth;
            go.GetComponent<enemyController>().eMaxDefence = 50;

        }
    }


    public void BossStats()
    {
        go.GetComponent<enemyController>().eMaxHealth = Random.Range(100, 200);
        go.GetComponent<enemyController>().eMaxDamage = Random.Range(10, 20);
        go.GetComponent<enemyController>().eMinDamage = Random.Range(5, 10);
        go.GetComponent<enemyController>().eHealth = go.GetComponent<enemyController>().eMaxHealth;
        go.GetComponent<enemyController>().eMaxDefence = 50;
    }


    public void Aggression()
    {
        if (gameController.turnCount > 7 && gameController.aggrovated)
        {
            for (int i = 0;i < list.Count; i++)
            {
                if (list[i].GetComponent<enemyController>().alive == true)
                {
                    list[i].GetComponent<enemyController>().eMaxHealth += 5;
                    list[i].GetComponent<enemyController>().eMaxDamage += 5;
                    list[i].GetComponent<enemyController>().eMinDamage += 5;
                    list[i].GetComponent<enemyController>().eHealth += 5;
                }
                
            }
            gameController.aggrovated = false;
        }
    }

}
