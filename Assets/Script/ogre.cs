using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ogre : MonoBehaviour
{
    public enemyController enemyController;
    public enemyGenerator enemyGenerator;
    public CSVReader cSVReader;
    public playerController playerController;

    public GameObject go;


    public void Ogre()
    {
        go.GetComponent<enemyController>().eMaxHealth = cSVReader.myEnemyList.enemy[1].maxHealth;
        go.GetComponent<enemyController>().eMinHealth = cSVReader.myEnemyList.enemy[1].minHealth;

        go.GetComponent<enemyController>().eMaxDamage = cSVReader.myEnemyList.enemy[1].maxDamage;
        go.GetComponent<enemyController>().eMinDamage = cSVReader.myEnemyList.enemy[1].minDamage;


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
        if (enemyController.enemy.GetComponent<enemyController>().eDamage - playerController.pDefence <= playerController.pDefence)
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
        enemyController.enemy.GetComponent<enemyController>().eHealth += 25;
        enemyController.ehealthMeter.UpdateMeter(enemyController.enemy.GetComponent<enemyController>().eHealth, enemyController.enemy.GetComponent<enemyController>().eMaxHealth);
        go.GetComponent<enemyController>().special = false;
    }

    //Enemy defend
    void Defend()
    {
        enemyController.enemy.GetComponent<enemyController>().eDefence += 25;
        enemyController.edefenceMeter.UpdateMeter(enemyController.enemy.GetComponent<enemyController>().eDefence, enemyController.enemy.GetComponent<enemyController>().eMaxDefence);
        go.GetComponent<enemyController>().defending = false;
    }
}
