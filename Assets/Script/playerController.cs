using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    //Call scripts
    public gameController gameControl;
    public enemyGenerator enemyGenerator;
    public enemyController enemyController; 
    public selectEnemy selectEnemy;
    public loseScreen loseScreen;
    public winScreen winScreen;
    public audioController audioController;
    public jamesAudioScript jamesAudio;

    //Sliders
    public propertyMeter healthMeter;
    public propertyMeter defenceMeter;

    //GUI
    public GameObject menu;
    public Text healthNum;
    public Text defenceNum;
    public bool selecting = false;
    public GameObject selectMenu;
    public bool playersTurn;
    public bool isCoroutineOn;
    public bool wait;
    public bool belowHalfHealth = false;
    public bool aboveHalfHealth = true;
    public bool levelling;

    //Stats
    public int pMaxHealth = 100;
    public int pHealth;    
    private int pMaxDamage = 200;
    private int pMinDamage = 100;
    public int pDamage;
    public int pMaxDefence = 50;
    public int pDefence = 0;
    public int energy;
    public Text energyCount;
    public int maxEnergy = 5;

    //Buttons
    public Button attack;
    public Button heal;
    public Button defend;
    public Button end;

    //Animated Text
    public Animator healAnim;
    public Animator defendAnim;
    public Text healText;
    public Text defendText;

    //Level Up
    public int killCount;
    public GameObject levelOptions;

    //Status Effects
    public bool slow;

    private void Start()
    {
        pHealth = pMaxHealth;
    }

    //Player death & defence/health doesnt pass a certain threshold
    private void Update()
    {
        if(pHealth > pMaxHealth)
        {
            pHealth = pMaxHealth;
            healthNum.text = pHealth.ToString() + "/" + pMaxHealth.ToString();
        }

        if(pDefence > pMaxDefence)
        {
            pDefence=pMaxDefence;
            defenceNum.text = pDefence.ToString() + "/" + pMaxDefence.ToString();
        }

        if(pDefence <= 0)
        {
            pDefence = 0;
            defenceNum.text = pDefence.ToString() + "/" + pMaxDefence.ToString();
        }

        if(pHealth <= 0)
        {
            menu.SetActive(false);
            Destroy(this);
            loseScreen.DeathScreen();

        }
        if (pHealth >= pMaxHealth * 0.5 & !aboveHalfHealth)
        {
            aboveHalfHealth = true;
        }
        if (pHealth <= pMaxHealth * 0.5 & aboveHalfHealth)
        {
            belowHalfHealth = true;
            aboveHalfHealth = false;
        }
        if (belowHalfHealth)
        {
            jamesAudio.PlayLowHelthAlert();
            belowHalfHealth = false;
        }

        Buttons();
    }
    //Player turn starts, called by gameController & enemyController
    public void PlayerStart()
    {
        if (levelling == false)
        {
            wait = false;
            playersTurn = true;
            energyCount.text = energy.ToString();
            healthNum.text = pHealth.ToString() + "/" + pMaxHealth.ToString();
            healthMeter.UpdateMeter(pHealth, pMaxHealth);
            defenceNum.text = pDefence.ToString() + "/" + pMaxDefence.ToString();
            defenceMeter.UpdateMeter(pDefence, pMaxDefence);

            end.interactable = true;
        }
      
    }

    //Player turn
    public void PlayerGo(int ButtonPress)
    {
        //Attack, player MUST have 1 energy point or more
        if (ButtonPress == 1 && energy >= 1)
        {
            selecting = true;
            energy -= 1;
            energyCount.text = energy.ToString();
            jamesAudio.PlayerSelectsAttackAudio();
            


        }
        //Heal, player MUST have 2 energy points or more
        if (ButtonPress == 2 && energy >= 2 && pHealth < pMaxHealth)
        {
            pHealth += 10;
            healText.text = 10.ToString();
            healthMeter.UpdateMeter(pHealth, pMaxHealth);
            healAnim.Play("heal");
            energy -= 2;
            PlayerStart();
        }
        //Defend, player MUST have 2 energy points or more
        if (ButtonPress == 3 && energy >= 2 && pDefence < 50)
        {
            pDefence += 15;
            defendText.text = 15.ToString();
            defenceMeter.UpdateMeter(pDefence, pMaxDefence);
            defendAnim.Play("defend");
            energy -= 2;
            PlayerStart();
        }
        //End Turn, player can end turn whenever
        if (ButtonPress == 4)
        {
            audioController.PlayerEndSound();
            gameControl.EnemyTurn();
            gameControl.turnCounter.text = "Turn: " + gameControl.turnCount;
            gameControl.turnCount += 1;
            wait = true;

        }
    }

    //Attack Calculation againts enemy/enemies
    public void Attack(int listIndex)
    {
        GameObject enemy = enemyGenerator.list[listIndex];
        Damage();
        if (pDamage - enemy.GetComponent<enemyController>().eDefence <= enemy.GetComponent<enemyController>().eDefence)
        {
            enemy.GetComponent<enemyController>().eDefence -= pDamage;
            enemy.GetComponent<enemyController>().edefenceMeter.UpdateMeter(enemy.GetComponent<enemyController>().eDefence, enemy.GetComponent<enemyController>().eMaxDefence);
        }
        //breaks defence + goes into health. Also if enemy has no defence
        else
        {
            enemy.GetComponent<enemyController>().eHealth = enemy.GetComponent<enemyController>().eHealth - pDamage + enemy.GetComponent<enemyController>().eDefence;
            enemy.GetComponent<enemyController>().eDefence = 0;
            enemy.GetComponent<enemyController>().edefenceMeter.UpdateMeter(enemy.GetComponent<enemyController>().eDefence, enemy.GetComponent<enemyController>().eMaxDefence);
            enemy.GetComponent<enemyController>().ehealthMeter.UpdateMeter(enemy.GetComponent<enemyController>().eHealth, enemy.GetComponent<enemyController>().eMaxHealth);
        }
        selecting = false;
        wait = true;
        audioController.PlayerAttackingSound();

        StartCoroutine(Delay(0.5f));
        enemy.GetComponent<enemyController>().damageOutput.SetActive(true);
        enemy.GetComponent<enemyController>().damageText.text = pDamage.ToString();
        enemy.GetComponent<enemyController>().damageAnim.Play("damage");

        IEnumerator Delay(float time)
        {
            if (isCoroutineOn)
                yield break;

            isCoroutineOn = true;

            yield return new WaitForSeconds(time);
            
            if (enemy.GetComponent<enemyController>().eHealth <= 0)
            {
                enemy.GetComponent<enemyController>().dead = true;
                enemy.GetComponent<enemyController>().alive = false;
                enemy.GetComponent<enemyController>().image.enabled = false;
                enemy.GetComponent<enemyController>().healthbar.SetActive(false);
                enemy.GetComponent<enemyController>().selectArrow.transform.position = new Vector2(0, -200);
                energy += 1;
                killCount++;
            }
            else
            {
                enemy.GetComponent<enemyController>().hit = true;
            }

            if (killCount == 3)
            {
                levelOptions.SetActive(true);
                playersTurn = false;
                jamesAudio.PlayerLevelUpAudio();
                wait = true;
                levelling = true;
            }

            PlayerStart();

            isCoroutineOn = false;
        }
       

        //Checking whether all enemies are alive, if all enemies have less than or equal to 0 health, player wins
        bool alive = false;
        for (int i = 0; i < enemyGenerator.list.Count; i++)
        {
            if (enemyGenerator.list[i].GetComponent<enemyController>().eHealth > 0)
            {
                alive = true;
                
            }

        }
        if (alive == false)
        {

            for(int i = 0;i < enemyGenerator.list.Count; i++)
            {
                if (enemyGenerator.list[i].GetComponent<enemyController>().eHealth < 0)
                {
                   // Destroy(enemyGenerator.list[i]);
                }
            }
            selecting = true;
            winScreen.Victory();
            
        }
    }

   

    //Damage calculation
    private void Damage()
    {
        pDamage = Random.Range(pMaxDamage, pMinDamage);
    }

    //Button Avaliability
    private void Buttons()
    {
        if (energy == 0 || selecting == true || playersTurn == false || wait == true || levelling == true)
        {
            attack.interactable = false;
        }
        else
        {
            attack.interactable = true;
        }
        
        if (energy < 2 || pHealth == pMaxHealth || selecting == true || playersTurn == false || wait == true || levelling == true)
        {
            heal.interactable = false;
        }
        else
        {
            heal.interactable=true;
        }
        
        if (energy < 2 || pDefence == 50 || selecting == true || playersTurn == false || wait == true || levelling == true)
        {
            defend.interactable=false;
        }
        else
        {
            defend.interactable = true;
        }

        if (selecting == true || playersTurn == false || wait == true || levelling == true)
        {
            end.interactable = false;
        }

    }

    public void LevelUp(int LevelUp)
    {
        if (LevelUp == 1)
        {
            PlayerStart();
            pMaxHealth += 5;
            pHealth += 5;
            healthNum.text = pHealth.ToString() + "/" + pMaxHealth.ToString();
            healthMeter.UpdateMeter(pHealth, pMaxHealth);
            levelOptions.SetActive(false);
            killCount = 0;
            jamesAudio.PlayerLevelUpHealthAudio();
            levelling = false;
            wait = false;

        }
        else if (LevelUp == 2)
        {
            PlayerStart();
            pMaxDamage += 3;
            pMinDamage += 3;
            levelOptions.SetActive(false);
            killCount = 0;
            playersTurn = true;
            jamesAudio.PlayerLevelUpDamageAudio();
            levelling = false;
            wait = false;
        }
        else if (LevelUp == 3)
        {
            PlayerStart();
            maxEnergy += 1;
            levelOptions.SetActive(false);
            killCount = 0;
            playersTurn = true;
            energyCount.text = energy.ToString();
            jamesAudio.PlayerLevelUpEnergyAudio();
            levelling = false;
            wait = false;
        }
    }
}
