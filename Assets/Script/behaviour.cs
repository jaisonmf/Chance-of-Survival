using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviour : enemyController
{
    public enemyController enemyController;

   
    public void Attack()
    {
        enemyController.Damage();
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
    }
    //Enemy 'special move'
    public void Special()
    {
        Attack();
        enemyController.enemy.GetComponent<enemyController>().eHealth += 10;
        enemyController.ehealthMeter.UpdateMeter(enemyController.eHealth, enemyController.eMaxHealth);
    }

    //Enemy defend
    public void Defend()
    {
        enemyController.enemy.GetComponent<enemyController>().eDefence += 10;
        enemyController.edefenceMeter.UpdateMeter(enemyController.eDefence, enemyController.eMaxDefence);
    }
}
