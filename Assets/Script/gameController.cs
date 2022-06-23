using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public bool enemyTurn = false;
    public bool gameStart = false;
    public bool generated = false;
    public bool aggrovated = false;

    public enemyController enemyController;
    public playerController playerController;
    public enemyGenerator enemyGenerator;
    public winScreen winScreen;

    public Text turnCounter;
    public int turnCount;

    public Text instructions;
    
    
    private void Start()
    {
        enemyTurn = false;
        turnCount = 0;
        turnCounter.text = "Turn: " + turnCount;
        instructions.text = ("You have been attacked!\nPress 'E' to continue");
        playerController.healthNum.text = playerController.pHealth.ToString() + "/" + playerController.pMaxHealth.ToString();
        playerController.defenceNum.text = playerController.pDefence.ToString() + "/" + playerController.pMaxDefence.ToString();
   
    
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && generated == false)
        {
            enemyGenerator.EnemyGeneration();
            gameStart = true;
            generated = true;
            PlayerTurn();
            turnCount++;
        }

    }
    public void PlayerTurn()
    {
            playerController.PlayerStart();
            instructions.text = ("Select your move");
            enemyGenerator.Aggression();


    }

    public void EnemyTurn()
    {
        instructions.text = ("");
            for (int i = 0; i < enemyGenerator.list.Count; i++)
            {
            enemyGenerator.list[i].GetComponent<enemyController>().EnemyStart();
            aggrovated = true;
            }
    }

}
