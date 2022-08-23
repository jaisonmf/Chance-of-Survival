using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGenerator : MonoBehaviour
{
    public gameController gameController;
    public enemyController enemyController;
    public CSVReader csvReader;
    public CSVReader bossCsvReader;
    public ShopSpawn shopSpawn;
   

    public int amount;
    public int maxSpawn = 4;
    public int minSpawn = 1;
    public GameObject enemyType;
    public int shopCounter;
    public int bossCounter;
    

    public GameObject go;
    public GameObject Parent;

    //Enemies
    public GameObject goblin;
    public GameObject ogre;

    //Boss
    public GameObject orc;
    public GameObject goblinKing;


    //Stats
    public goblin goblinStats;
    public ogre ogreStats;
    public orc orcStats;
    public goblinKing goblinKingStats;

    //Number of enemy
    public List<GameObject> list = new List<GameObject>();
    //Enemy Type
    public List<GameObject> Type = new List<GameObject>();
    //Boss Type
    public List<GameObject> Boss = new List<GameObject>();

    private void Start()
    {
        Type[0] = goblin;
        
        Type[1] = ogre;

        Boss[0] = orc;

        Boss[1] = goblinKing;

    }

    public void EnemyGeneration()
    {
        goblinStats.Goblin();
        ogreStats.Ogre();
        orcStats.Orc();
        goblinKingStats.GoblinKing();

        for (int i = 0; i < gameController.waveCount; i++)
        {
            maxSpawn++;
        }
        amount = Random.Range(minSpawn, maxSpawn);

        if (maxSpawn > 11)
        {
            maxSpawn = 10;
        }
        if (amount >= 11)
        {
            amount = 10;
        }

        if (shopCounter != 9 || bossCounter != 20)
        {
            {
                for (int i = 0; i < amount; i++)
                {
                    enemyType = Type[Random.Range(0, Type.Count)];

                    {
                        go = Instantiate(enemyType, new Vector2((Screen.width / (amount + 1)) * (i + 1), -15), Quaternion.identity);
                        go.transform.SetParent(Parent.transform, false);
                        list.Add(go);
                        EnemyStats();

                        go.GetComponent<enemyController>().count = i;
                        enemyController.alive = true;
                    }

                }
            }
        }

        if(shopCounter == 9)
        {
            shopSpawn.ShopStart();
        }

        if (bossCounter == 20)
        {
            amount = 1;
            for (int i = 0; i < amount; i++)
            {
                enemyType = Boss[0];

                {
                    go = Instantiate(enemyType, new Vector2((Screen.width / (amount + 1)) * (i + 1), -15), Quaternion.identity);
                    go.transform.SetParent(Parent.transform, false);
                    list.Add(go);
                    EnemyStats();

                    go.GetComponent<enemyController>().count = i;
                    enemyController.alive = true;
                }

            }
        }
    }


    public void EnemyStats()
    {
        //Calculates states
        go.GetComponent<enemyController>().eMaxHealth = Random.Range(go.GetComponent<enemyController>().eMinHealth, go.GetComponent<enemyController>().eMaxHealth);
        go.GetComponent<enemyController>().eMaxDefence = 50;
        go.GetComponent<enemyController>().eHealth = go.GetComponent<enemyController>().eMaxHealth;
        go.GetComponent<enemyController>().goldDropped = Random.Range(go.GetComponent<enemyController>().minGold, go.GetComponent<enemyController>().maxGold);

    }

    
   
    public void Aggression()
    {
        if (gameController.turnCount > 5 && gameController.aggrovated)
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


    private void Update()
    {
        if(shopCounter == 9)
        {
            shopCounter = 1;
        }

        if(bossCounter == 20)
        {
            bossCounter = 0;
        }
    }

}
