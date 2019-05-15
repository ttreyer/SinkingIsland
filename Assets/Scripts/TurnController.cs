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
    public GameObject playerOneWaterLevelController;
    public GameObject playerTwoWaterLevelController;
    public GameObject seaLevelController;
    public GameObject playerOneProductionController;
    public GameObject playerTwoProductionController;

    public CardController p1Cards, p2Cards;

    public int currentTurn;
    public int turnLimit;
    public Text turnDisplay;

    public List<Action>[] turnActions;

    private bool started, gameOver;
    private ResourceController playerOneResourceControllerScript;
    private ResourceController playerTwoResourceControllerScript;
    private TradeController playerOneTradeControllerScript;
    private TradeController playerTwoTradeControllerScript;
    private WaterLevelController playerOneWaterLevelControllerScript;
    private WaterLevelController playerTwoWaterLevelControllerScript;
    private SeaLevelController seaLevelControllerScript;
    private ProductionController playerOneProductionControllerScript;
    private ProductionController playerTwoProductionControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerOneResourceControllerScript = playerOneResourceController.GetComponent<ResourceController>();
        playerTwoResourceControllerScript = playerTwoResourceController.GetComponent<ResourceController>();
        playerOneTradeControllerScript = playerOneTradeController.GetComponent<TradeController>();
        playerTwoTradeControllerScript = playerTwoTradeController.GetComponent<TradeController>();
        playerOneWaterLevelControllerScript = playerOneWaterLevelController.GetComponent<WaterLevelController>();
        playerTwoWaterLevelControllerScript = playerTwoWaterLevelController.GetComponent<WaterLevelController>();
        playerOneProductionControllerScript = playerOneProductionController.GetComponent<ProductionController>();
        playerTwoProductionControllerScript = playerTwoProductionController.GetComponent<ProductionController>();
        seaLevelControllerScript = seaLevelController.GetComponent<SeaLevelController>();
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

            turnDisplay.text = "Current Turn: " + currentTurn;

            //sea level calculations
            seaLevelControllerScript.DrawPollutionFromIslands();
            if (seaLevelControllerScript.currentPollutionLevel >= 20 && seaLevelControllerScript.currentWaterHeight == 0)
            {
                playerOneWaterLevelControllerScript.RaiseWaterLevel();
                playerTwoWaterLevelControllerScript.RaiseWaterLevel();
                seaLevelControllerScript.currentWaterHeight = 1;
                playerOneProductionControllerScript.UpdateProduction();
                playerTwoProductionControllerScript.UpdateProduction();

                gameObject.GetComponent<MusicController>().PlayLevel(1);
            }

            if (seaLevelControllerScript.currentPollutionLevel >= 30 && seaLevelControllerScript.currentWaterHeight == 1)
            {
                playerOneWaterLevelControllerScript.RaiseWaterLevel();
                playerTwoWaterLevelControllerScript.RaiseWaterLevel();
                seaLevelControllerScript.currentWaterHeight = 2;
                playerOneProductionControllerScript.UpdateProduction();
                playerTwoProductionControllerScript.UpdateProduction();
                gameObject.GetComponent<MusicController>().PlayLevel(2);

            }

            if (seaLevelControllerScript.currentPollutionLevel >= 40 && seaLevelControllerScript.currentWaterHeight == 2)
            {
                playerOneWaterLevelControllerScript.RaiseWaterLevel();
                playerTwoWaterLevelControllerScript.RaiseWaterLevel();
                seaLevelControllerScript.currentWaterHeight = 3;
                playerOneProductionControllerScript.UpdateProduction();
                playerTwoProductionControllerScript.UpdateProduction();
                gameObject.GetComponent<MusicController>().PlayLevel(3);

            }

            playerOneResourceControllerScript.ResetCurrentResource();
            playerTwoResourceControllerScript.ResetCurrentResource();

            // When do we want to handle the delayed actions?
            foreach (Action action in ActionsForTurn(currentTurn))
                action();

            ResourceValues playerOneResources = playerOneTradeControllerScript.ProcessTrades();
            ResourceValues playerTwoResources = playerTwoTradeControllerScript.ProcessTrades();
            playerOneResources.Add(playerOneResourceController.GetComponentInChildren<ProductionController>().production);

            playerTwoResources.Add(playerTwoResourceController.GetComponentInChildren<ProductionController>().production);
            playerOneResourceControllerScript.UpdateResourceCount("P1", playerOneResources);
            playerTwoResourceControllerScript.UpdateResourceCount("P2", playerTwoResources);

            p1Cards.DrawNewHand();
            p2Cards.DrawNewHand();
        }

        else if (currentTurn == turnLimit)
        {
            gameOver = true;
            turnDisplay.text = "Game has ended!";
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
        if (currentTurn + turn <= turnActions.Length)
            ActionsForTurn(currentTurn + turn).Add(action);
    }
}
