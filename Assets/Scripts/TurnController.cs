using System;
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

    public List<Action>[] turnActions;

    private bool started, gameOver;
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
        started = gameOver = false;

        turnActions = new List<Action>[turnLimit];
        for (int i = 0; i < turnLimit; ++i)
            turnActions[i] = new List<Action>();
    }

    //turn execution
    public void Clicked()
    {
        if ((currentTurn != turnLimit) && (gameOver == false))
        {
            currentTurn += 1;
            //turnDisplay.text = "Current Turn: " + currentTurn;

            // When do we want to handle the delayed actions?
            foreach (Action action in ActionsForTurn(currentTurn))
                action();

            ResourceValues playerOneResources = playerOneTradeControllerScript.ProcessTrades();
            ResourceValues playerTwoResources = playerTwoTradeControllerScript.ProcessTrades();
            playerOneResources.Add(playerOneResourceController.GetComponentInChildren<ProductionController>().production);

            playerTwoResources.Add(playerTwoResourceController.GetComponentInChildren<ProductionController>().production);
            playerOneResourceControllerScript.UpdateResourceCount("P1", playerOneResources);
            playerTwoResourceControllerScript.UpdateResourceCount("P2", playerTwoResources);
        }

        else if (currentTurn == turnLimit)
        {
            gameOver = true;
            //turnDisplay.text = "Turn limit exceeded";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) {
            started = true;
            Clicked();
        }
    }

    private List<Action> ActionsForTurn(int turn) {
        return turnActions[turn - 1]; // List is 0 based, turn are 1 based
    }

    public void DelayAction(int turn, Action action) {
        ActionsForTurn(currentTurn + turn).Add(action);
    }
}
