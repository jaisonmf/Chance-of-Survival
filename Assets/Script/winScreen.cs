using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winScreen : MonoBehaviour
{
    public GameObject win;
    public Text winText;
    public bool endGame;
    private bool listClear;

    public gameController gameController;
    public enemyGenerator enemyGenerator;
    public playerController playerController;
    public enemyController enemyController;
    public jamesAudioScript jamesAudioScript;

    public void Victory()
    {
        win.SetActive(true);
        winText.text = ("You have survived the encounter!\n Press 'E' to return home or 'Q' to continue deeper");
        gameController.instructions.text = (""); 
        endGame = true;
        playerController.selecting = true;
        enemyGenerator.list.Clear();
        listClear = true;
        jamesAudioScript.StopCombatMusic();
        jamesAudioScript.PlayAmbientMusic();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && endGame == true)
        {
            Debug.Log("Application Closes");
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Q) && endGame == true && listClear == true)
        {
            //Next wave
            gameController.WaveStart();
            listClear = false;
            endGame = false;
            playerController.selecting = false;
            win.SetActive(false);

            

            //Wave count + turn cout
            gameController.waveCount++;
            gameController.waveCounter.text = "Wave: " + gameController.waveCount;
            gameController.turnCounter.text = "Turn: " + gameController.turnCount;
            gameController.turnCount = 1;

            //PlayerController
            playerController.PlayerStart();
            playerController.energy = playerController.maxEnergy;
            playerController.pHealth += 15;
            playerController.pDefence = 0;
            playerController.healthNum.text = playerController.pHealth.ToString() + "/" + playerController.pMaxHealth.ToString();
            playerController.healthMeter.UpdateMeter(playerController.pHealth, playerController.pMaxHealth);
            playerController.defenceNum.text = playerController.pDefence.ToString() + "/" + playerController.pMaxDefence.ToString();
            playerController.defenceMeter.UpdateMeter(playerController.pDefence, playerController.pMaxDefence);
            playerController.energyCount.text = playerController.energy.ToString();



        }
    }

}
