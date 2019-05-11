using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public GameObject playerOneResourceController;
    public GameObject playerTwoResourceController;
    
    public int currentTurn;
    public int turnLimit;
    public Text turnDisplay;

    private bool gameOver;
    private ResourceController playerOneResourceControllerScript;
    private ResourceController playerTwoResourceControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerOneResourceControllerScript = playerOneResourceController.GetComponent<ResourceController>();
        playerTwoResourceControllerScript = playerTwoResourceController.GetComponent<ResourceController>();
        Clicked();
        gameOver = false;
    }

    public void Clicked()
    {
        if ((currentTurn != turnLimit) && (gameOver == false))
        {
            currentTurn += 1;
            turnDisplay.text = "Current Turn: " + currentTurn;
            playerOneResourceControllerScript.UpdateResourceCount();
            playerTwoResourceControllerScript.UpdateResourceCount();
        }

        else if (currentTurn == turnLimit)
        {
            gameOver = true;
            turnDisplay.text = "Turn limit exceeded";
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
