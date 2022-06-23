using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectEnemy : MonoBehaviour
{
    public enemyController enemyController;
    public playerController playerController;
    public Button selection;
    public GameObject selectButton;
    public GameObject selectedEnemy;


    private void Start()
    {
        selectButton.SetActive(false);
    }
    public void Select(int Select)
    {
        if(Select == 0 && playerController.selecting == true)
        {
            Selected();
        }
    }




    void Selected()
    {
        playerController.Attack(this.transform.parent.GetComponent<enemyController>().count);
    }
}
