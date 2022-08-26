using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSpawn : MonoBehaviour
{
    public gameController gameController;
    public ShopCSVReader shopCSVReader;
    public playerController playerController;
    public winScreen winScreen;
    public enemyGenerator enemyGenerator;

    public int textRNG;
    public int One;
    public int Two;
    public int Three;
    public int Four;


    public int itemCount = 4;

    public Text itemOne;
    public Text itemTwo;
    public Text itemThree;
    public Text itemFour;

    //public GameObject healthVial;
    //public GameObject shackles;

    public Text description;
    public Text cost;

    public GameObject shopList;
    public GameObject confirmation;
    public GameObject shopMenu;

    
    private void Start()
    {

    }
    public void ShopStart()
    {
        textRNG = Random.Range(0, 3);
        shopMenu.SetActive(true);
        shopList.SetActive(true);
        gameController.waveCounter.text = "";
        gameController.turnCounter.text = "";
        


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
        One = Random.Range(0, shopCSVReader.myShoppingList.shop.Length);

        itemOne.text = shopCSVReader.myShoppingList.shop[One].name;

        Two = Random.Range(0, shopCSVReader.myShoppingList.shop.Length);

        itemTwo.text = shopCSVReader.myShoppingList.shop[Two].name;

        Three = Random.Range(0, shopCSVReader.myShoppingList.shop.Length);

        itemThree.text = shopCSVReader.myShoppingList.shop[Three].name;

        Four = Random.Range(0, shopCSVReader.myShoppingList.shop.Length);

        itemFour.text = shopCSVReader.myShoppingList.shop[Four].name;
    }

    public void Nothing()
    {

    }


    public void SelectedItem(int selected)
    {
        if(selected == 1)
        {
            cost.text = shopCSVReader.myShoppingList.shop[One].cost.ToString();
            description.text = shopCSVReader.myShoppingList.shop[One].description;
            shopList.SetActive(false);
            confirmation.SetActive(true);
        }

        if (selected == 2)
        {
            cost.text = shopCSVReader.myShoppingList.shop[Two].cost.ToString();
            description.text = shopCSVReader.myShoppingList.shop[Two].description;
            shopList.SetActive(false);
            confirmation.SetActive(true);
        }

        if (selected == 3)
        {
            cost.text = shopCSVReader.myShoppingList.shop[Three].cost.ToString();
            description.text = shopCSVReader.myShoppingList.shop[Three].description;
            shopList.SetActive(false);
            confirmation.SetActive(true);
        }

        if (selected == 4)
        {
            cost.text = shopCSVReader.myShoppingList.shop[Four].cost.ToString();
            description.text = shopCSVReader.myShoppingList.shop[Four].description;
            shopList.SetActive(false);
            confirmation.SetActive(true);
        }
        if (selected == 5)
        {
            shopMenu.SetActive(false);
            shopList.SetActive(false);
            enemyGenerator.shopCounter = 0;
            winScreen.Victory();
        }
    }

    public void Confirm(int select)
    {
        if(select == 1)
        {
            int costInt = (int.Parse(cost.text));
            if(playerController.gold < costInt)
            {
                gameController.instructions.text = "Sorry friend, try to buy this when you're a little - mmmm - richer";
            }
            else
            {
                playerController.gold -= costInt;
                Nothing();
                shopList.SetActive(true);
                confirmation.SetActive(false);
            }
        }
        if(select == 2)
        {
            shopList.SetActive(true);
            confirmation.SetActive(false);
            Nothing();
        }
    }
}
