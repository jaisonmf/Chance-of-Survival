using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblin : MonoBehaviour
{
    public enemyController enemyController;
    public enemyGenerator enemyGenerator;
    public CSVReader cSVReader;
    public playerController playerController;
    public audioController audioController;

    public GameObject go;




    public void Goblin()
    {
        go.GetComponent<enemyController>().eMaxHealth = cSVReader.myEnemyList.enemy[0].maxHealth;
        go.GetComponent<enemyController>().eMinHealth = cSVReader.myEnemyList.enemy[0].minHealth;

        go.GetComponent<enemyController>().eMaxDamage = cSVReader.myEnemyList.enemy[0].maxDamage;
        go.GetComponent<enemyController>().eMinDamage = cSVReader.myEnemyList.enemy[0].minDamage;

        
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

        if(go.GetComponent<enemyController>().hit == true)
        {
            audioController.Hurt();
            go.GetComponent<enemyController>().hit = false;
        }

        if (go.GetComponent<enemyController>().dead == true)
        {
            audioController.Dying();
            go.GetComponent<enemyController>().dead = false;
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
            playerController.audioController.DefenceHit();
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
            playerController.audioController.Hurt();
        }

        go.GetComponent<enemyController>().attacking = false;


        
    }
    void Special()
    {
        Attack();
        enemyController.enemy.GetComponent<enemyController>().eHealth += 10;
        enemyController.ehealthMeter.UpdateMeter(enemyController.enemy.GetComponent<enemyController>().eHealth, enemyController.enemy.GetComponent<enemyController>().eMaxHealth);
        go.GetComponent<enemyController>().special = false;
    }

    //Enemy defend
    void Defend()
    {
        enemyController.enemy.GetComponent<enemyController>().eDefence += 10;
        enemyController.edefenceMeter.UpdateMeter(enemyController.enemy.GetComponent<enemyController>().eDefence, enemyController.enemy.GetComponent<enemyController>().eMaxDefence);
        go.GetComponent<enemyController>().defending = false;
    }
}
