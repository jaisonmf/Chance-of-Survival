using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winScreen : MonoBehaviour
{
    public GameObject win;
    public Text winText;
    public bool endWave;
    public bool endArea;


    public gameController gameController;
    public enemyGenerator enemyGenerator;
    public playerController playerController;
    public enemyController enemyController;
    public jamesAudioScript jamesAudioScript;

    public void Victory()
    {
        win.SetActive(true);
        endWave = true;
        if (endWave == true)
        {
            winText.text = ("You have survived the encounter!\n Press 'Q' to continue deeper");
        }
        else
        {
            winText.text = ("You have slain all the monsters in this area!\n Press 'Q' to move onto the next");
        }
        
        gameController.instructions.text = ("");
        
        playerController.selecting = true;
        jamesAudioScript.StopCombatMusic();
        jamesAudioScript.PlayAmbientMusic();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && endWave == true)
        {
            //Next wave
            for (int i = 0; i < enemyGenerator.list.Count; i++)
            {
                if (enemyGenerator.list[i].GetComponent<enemyController>().eHealth < 0)
                {
                    Destroy(enemyGenerator.list[i]);
                }
            }
            enemyGenerator.list.Clear();
            gameController.WaveStart();
            endWave = false;
            playerController.selecting = false;
            win.SetActive(false);



            //Wave count + turn cout
            gameController.waveCount++;
            gameController.waveCounter.text = "Wave: " + gameController.waveCount;
            gameController.turnCounter.text = "Turn: " + gameController.turnCount;
            gameController.turnCount = 1;

            enemyGenerator.shopCounter++;
            enemyGenerator.bossCounter++;

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
        if (Input.GetKeyDown(KeyCode.Q) && endWave == true)
        {

        }
    }

}
