using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyController : MonoBehaviour
{
    //Call scripts
    public gameController gameController;
    public enemyGenerator enemyGenerator;
    public playerController playerController;
    public winScreen winScreen;
    public loseScreen loseScreen;

    //Enemy GUI
    public GameObject enemy;
    public GameObject enemyBars;
    public GameObject selection;
    public propertyMeter ehealthMeter;
    public propertyMeter edefenceMeter;
    public Text damageText;
    public GameObject damageOutput;
    public Animator damageAnim;

    //Delay
    private bool isCoroutineOn;

    //Stats
    public int eMaxHealth;
    public int eHealth;
    public int eMaxDamage;
    public int eMinDamage;
    public int eDefence;
    public int eMaxDefence;
    public int count;
    private int Action;
    public int eDamage;
    public bool alive = true;

    //Certain stats cannot go over a certain threshold
    private void Update()
    {
        if (eDefence >= eMaxDefence)
        {
            eDefence = eMaxDefence;
        }
        if(eHealth >= eMaxHealth)
        {
            eHealth = eMaxHealth;
        }
        if (gameController.enemyTurn == false)
        {
            selection.SetActive(true);
        }
        else if (gameController.enemyTurn == true)
        {
            selection.SetActive(false);
        }
    }

    //Enemy delay on turn + Enemy turn start
    public void EnemyStart()
    {
        if (alive == true)
        {
            selection.SetActive(false);
            playerController.end.interactable = false;
            playerController.defend.interactable = false;
            playerController.attack.interactable = false;
            playerController.heal.interactable = false;
            StartCoroutine(Delay(3));
        }
    }

    //Enemy takes turn + ends turns
    IEnumerator Delay(float time)
    {
        if (isCoroutineOn)
            yield break;

        isCoroutineOn = true;

        yield return new WaitForSeconds(time);
        EnemyGo();

        //Checks if the player has died
        if( playerController.pHealth <= 0)
        {
            loseScreen.DeathScreen();
        }
        //Updates player health/defence bars, resets player energy to 5, starts the player turn
        ehealthMeter.UpdateMeter(eHealth, eMaxHealth);
        edefenceMeter.UpdateMeter(eDefence, eMaxDefence);
        gameController.PlayerTurn();
        playerController.energy = playerController.maxEnergy;
        playerController.energyCount.text = playerController.energy.ToString();
        isCoroutineOn = false;
    }

    //Enemy behaviour
    public void EnemyGo()
    {
        if(enemy.GetComponent<enemyController>().eDefence > 0)
        {
            enemy.GetComponent<enemyController>().eDefence = 0;
        }
        Action = Random.Range(1, 7);
        Debug.Log(Action);
        //Above 75%
        if (enemy.GetComponent<enemyController>().eHealth > enemy.GetComponent<enemyController>().eMaxHealth * 0.75)
        {
            if (Action <= 3)
            {
                Attack();
            }
            else if (Action <= 5)
            {
                Special();
            }
            else if(Action == 6)
            {
                Defend();
            }
        }
        //Above 25%
        else if (enemy.GetComponent<enemyController>().eHealth > enemy.GetComponent<enemyController>().eMaxHealth * 0.25)
        {
            if (Action == 1 || Action == 2)
            {
                Attack();
            }
            else if (Action == 3 || Action == 4)
            {
                Defend();
            }

            else Special();
        }
        //Lower than 25%
        else
        {
            if (Action <= 4)
            {
                Defend();
            }
            else if (Action == 5)
            {
                Special();
            }
            else if (Action == 6)
            {
                Attack();
            }
        }

    }

    //Enemy attack against player
    public void Attack()
    {
        Damage();
        Debug.Log("weeeeeeeeeeeeeeeeeeeee");
        //not enough to break defend
        if (enemy.GetComponent<enemyController>().eDamage - playerController.pDefence <= playerController.pDefence)
        {
            playerController.pDefence -= enemy.GetComponent<enemyController>().eDamage;
            playerController.defenceMeter.UpdateMeter(playerController.pDefence, playerController.pMaxDefence);
            playerController.defenceNum.text = playerController.pDefence.ToString() + "/" + playerController.pMaxDefence.ToString();
        }
        //breaks defence + goes into health. Also if player has no defence
        else
        {
            playerController.pHealth = playerController.pHealth - enemy.GetComponent<enemyController>().eDamage + playerController.pDefence;
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
        enemy.GetComponent<enemyController>().eHealth += 10;
        ehealthMeter.UpdateMeter(eHealth, eMaxHealth);
    }

    //Enemy defend
    public void Defend()
    {
        enemy.GetComponent<enemyController>().eDefence += 10;
        edefenceMeter.UpdateMeter(eDefence, eMaxDefence);
    }

    //Damage calculation
    private void Damage()
    {
        eDamage = Random.Range(eMaxDamage, eMinDamage);
    }

}