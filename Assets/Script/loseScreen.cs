using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loseScreen : MonoBehaviour
{
    public Text gameOverText;
    public GameObject gameOver;

    public bool lost = false;
    public gameController gameController;

    public void DeathScreen()
    {
        gameController.instructions.text = "";
        gameOver.SetActive(true);
        gameOverText.text = "You have fallen in battle, and the goblins scurry back into the forest\nPress 'E' to accept defeat";
        lost = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && lost == true)
        {
            Debug.Log("Application Closes");
            Application.Quit();
        }
    }

}
