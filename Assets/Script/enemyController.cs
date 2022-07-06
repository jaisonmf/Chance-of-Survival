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
    public audioController audioController;

    //Enemy GUI
    public GameObject enemy;
    public GameObject enemyBars;
    public GameObject healthbar;
    public GameObject selection;
    public GameObject selectArrow;
    public propertyMeter ehealthMeter;
    public propertyMeter edefenceMeter;
    public Text damageText;
    public GameObject damageOutput;
    public Animator damageAnim;
    public Image image;



    public GameObject typeOfEnemy;

    //Delay
    private bool isCoroutineOn;

    //Stats
    public int eMaxHealth;
    public int eMinHealth;
    public int eHealth;
    public int eMaxDamage;
    public int eMinDamage;
    public int eDefence;
    public int eMaxDefence;
    public int count;
    public int Action;
    public int eDamage;
    public bool alive = true;

    public bool attacking = false;
    public bool defending = false;
    public bool special = false;
    public bool hit = false;
    public bool dead = false;

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
        else
        {
            enemy.SetActive(false);
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
        if ( playerController.pHealth <= 0)
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

    //Damage calculation
    public void Damage()
    {
        eDamage = Random.Range(eMaxDamage, eMinDamage);
    }


    //Enemy behaviour
    public void EnemyGo()
    {
        if (enemy.GetComponent<enemyController>().eDefence > 0)
        {
            enemy.GetComponent<enemyController>().eDefence = 0;
        }
        
        Action = Random.Range(0, 7);


        {
            if (enemy.GetComponent<enemyController>().eHealth > enemy.GetComponent<enemyController>().eMaxHealth * 0.75)
            {
                if (Action <= 3)
                {
                    attacking = true;
                }
                else if (Action <= 5)
                {
                    special = true;
                }
                else if (Action == 6)
                {
                    defending = true;
                }
            }
            //Above 25%
            else if (enemy.GetComponent<enemyController>().eHealth > enemy.GetComponent<enemyController>().eMaxHealth * 0.25)
            {
                if (Action == 1 || Action == 2)
                {
                    attacking = true;
                }
                else if (Action == 3 || Action == 4)
                {
                    defending = true;
                }

                else special = true;
            }
            //Lower than 25%
            else
            {
                if (Action <= 4)
                {
                    defending = true;
                }
                else if (Action == 5)
                {
                    special = true;
                }
                else if (Action == 6)
                {
                    attacking = true;
                }
            }

        }

    }

    


}