using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSpawn : MonoBehaviour
{
    public gameController gameController;
    public ShopCSVReader shopCSVReader;

    public int textRNG;

    public Text itemOne;
    public Text itemTwo;
    public Text itemThree;
    public Text itemFour;

    public Text description;
    public Text cost;

    public GameObject shopList;
    public GameObject confirmation;

    public Text text;

    private void Start()
    {
        
    }
    public void ShopStart()
    {
        gameController.waveCounter.text = "";
        gameController.turnCounter.text = "";
        textRNG = Random.Range(0, 3);


        if(textRNG == 1)
        {
            gameController.instructions.text = "Welcome! Buy something would ya?";
        }
        else if (textRNG == 2)
        {
            gameController.instructions.text = "Health potions? Weapons? Relics? They're yours my friend. As long as you have enough gold";
        }
        else if (textRNG == 3)
        {
            gameController.instructions.text = "These should help you out with... whatever you do";
        }


        CreateList();
    }

    public void CreateList()
    {
        //text = Random.Range();
    }
}
