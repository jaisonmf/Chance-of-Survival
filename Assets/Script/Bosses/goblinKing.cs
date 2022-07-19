using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinKing : MonoBehaviour
{
    public enemyController enemyController;
    public enemyGenerator enemyGenerator;
    public CSVReader BosscSVReader;
    public playerController playerController;

    public GameObject go;


    public void GoblinKing()
    {
        go.GetComponent<enemyController>().eMaxHealth = BosscSVReader.myEnemyList.enemy[1].maxHealth;
        go.GetComponent<enemyController>().eMinHealth = BosscSVReader.myEnemyList.enemy[1].minHealth;

        go.GetComponent<enemyController>().eMaxDamage = BosscSVReader.myEnemyList.enemy[1].maxDamage;
        go.GetComponent<enemyController>().eMinDamage = BosscSVReader.myEnemyList.enemy[1].minDamage;


    }

    private void Update()
    {
        if (go.GetComponent<enemyController>().attacking == true)
        {
            Attack();
        }
        if (go.GetComponent<enemyController>().defending == true)
        {
            Defend();
        }
        if (go.GetComponent<enemyController>().special == true)
        {
            Special();
        }
    }
    public void Attack()
    {
        enemyController.enemy.GetComponent<enemyController>().Damage();
        //not enough to break defend
        if (enemyController.enemy.GetComponent<enemyController>().eDamage - playerController.pDefence < playerController.pDefence)
        {
            playerController.pDefence -= enemyController.enemy.GetComponent<enemyController>().eDamage;
            playerController.defenceMeter.UpdateMeter(playerController.pDefence, playerController.pMaxDefence);
            playerController.defenceNum.text = playerController.pDefence.ToString() + "/" + playerController.pMaxDefence.ToString();
        }
        //breaks defence + goes into health. Also if player has no defence
        else
        {
            playerController.pHealth = playerController.pHealth - enemyController.enemy.GetComponent<enemyController>().eDamage + playerController.pDefence;
            playerController.pDefence = 0;
            playerController.defenceMeter.UpdateMeter(playerController.pDefence, playerController.pMaxDefence);
            playerController.healthMeter.UpdateMeter(playerController.pHealth, playerController.pMaxHealth);
            playerController.healthNum.text = playerController.pHealth.ToString() + "/" + playerController.pMaxHealth.ToString();
            playerController.defenceNum.text = playerController.pDefence.ToString() + "/" + playerController.pMaxDefence.ToString();
        }

        go.GetComponent<enemyController>().attacking = false;



    }
    void Special()
    {
        enemyGenerator.amount = Random.Range(1, 2);
        for (int i = 0; i < enemyGenerator.amount; i++)
        {
            enemyGenerator.enemyType = enemyGenerator.Type[0];

            {
                enemyGenerator.go = Instantiate(enemyGenerator.enemyType, new Vector2((Screen.width / (enemyGenerator.amount + 2)) * (i + 1), -25), Quaternion.identity);
                enemyGenerator.go.transform.SetParent(enemyGenerator.Parent.transform, false);
                enemyGenerator.list.Add(enemyGenerator.go);
                enemyGenerator.EnemyStats();

                enemyGenerator.go.GetComponent<enemyController>().count = i;
                enemyController.alive = true;
            }
        }
        go.GetComponent<enemyController>().special = false;
    }

    //Enemy defend
    void Defend()
    {
        Attack();
        enemyController.enemy.GetComponent<enemyController>().eHealth += 10;
        enemyController.ehealthMeter.UpdateMeter(enemyController.enemy.GetComponent<enemyController>().eHealth, enemyController.enemy.GetComponent<enemyController>().eMaxHealth);
        go.GetComponent<enemyController>().defending = false;
    }
}
