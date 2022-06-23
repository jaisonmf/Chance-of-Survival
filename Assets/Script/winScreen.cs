using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winScreen : MonoBehaviour
{
    public GameObject win;
    public Text winText;
    public bool endGame;

    public gameController gameController;
    public enemyGenerator enemyGenerator;
    public playerController playerController;

    public void Victory()
    {
        win.SetActive(true);
        winText.text = ("You have survived the encounter!\n Press 'E' to return home");
        gameController.instructions.text = (""); 
        endGame = true;
        playerController.selecting = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && endGame == true)
        {
            Debug.Log("Application Closes");
            Application.Quit();
        }
    }

}
