using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orc : MonoBehaviour
{
    public enemyController enemyController;
    public enemyGenerator enemyGenerator;
    public CSVReader BosscSVReader;
    public playerController playerController;

    public GameObject go;


    public void Orc()
    {
        go.GetComponent<enemyController>().eMaxHealth = BosscSVReader.myEnemyList.enemy[0].maxHealth;
        go.GetComponent<enemyController>().eMinHealth = BosscSVReader.myEnemyList.enemy[0].minHealth;

        go.GetComponent<enemyController>().eMaxDamage = BosscSVReader.myEnemyList.enemy[0].maxDamage;
        go.GetComponent<enemyController>().eMinDamage = BosscSVReader.myEnemyList.enemy[0].minDamage;

        go.GetComponent<enemyController>().minGold = BosscSVReader.myEnemyList.enemy[0].minGold;
        go.GetComponent<enemyController>().maxGold = BosscSVReader.myEnemyList.enemy[0].maxGold;


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
        Attack();
        playerController.slow = true;
        Debug.Log("woahhhhhhhhj");
        go.GetComponent<enemyController>().special = false;
    }

    //Enemy defend
    void Defend()
    {
        Attack();
        enemyController.enemy.GetComponent<enemyController>().eDefence += 10;
        enemyController.edefenceMeter.UpdateMeter(enemyController.enemy.GetComponent<enemyController>().eDefence, enemyController.enemy.GetComponent<enemyController>().eMaxDefence);
        go.GetComponent<enemyController>().defending = false;
    }
}
