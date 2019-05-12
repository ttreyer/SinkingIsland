using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public GameObject playerOneResourceController;
    public GameObject playerTwoResourceController;
    public GameObject playerOneTradeController;
    public GameObject playerTwoTradeController;

    public int currentTurn;
    public int turnLimit;
    public Text turnDisplay;

    private bool gameOver;
    private ResourceController playerOneResourceControllerScript;
    private ResourceController playerTwoResourceControllerScript;
    private TradeController playerOneTradeControllerScript;
    private TradeController playerTwoTradeControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerOneResourceControllerScript = playerOneResourceController.GetComponent<ResourceController>();
        playerTwoResourceControllerScript = playerTwoResourceController.GetComponent<ResourceController>();
        playerOneTradeControllerScript = playerOneTradeController.GetComponent<TradeController>();
        playerTwoTradeControllerScript = playerTwoTradeController.GetComponent<TradeController>();
        Clicked();
        gameOver = false;
    }

    //turn execution
    public void Clicked()
    {
        if ((currentTurn != turnLimit) && (gameOver == false))
        {
            currentTurn += 1;
            turnDisplay.text = "Current Turn: " + currentTurn;
            playerOneTradeControllerScript.ProcessTrades();
            playerTwoTradeControllerScript.ProcessTrades();
            playerOneResourceControllerScript.UpdateResourceCount("P1");
            playerTwoResourceControllerScript.UpdateResourceCount("P2");
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
