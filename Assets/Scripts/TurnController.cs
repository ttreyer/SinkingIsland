using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public GameObject islandA, islandB;

    public CardController p1Cards, p2Cards;

    public int currentTurn;
    public int turnLimit;
    public Text turnDisplay;

    public ResourceValues source;

    private List<Action>[] turnActions;

    private bool started, gameOver;
    private ResourceController p1Resources, p2Resources;
    private WaterLevelController p1Water, p2Water;
    private ProductionController p1Production, p2Production;
    private SeaLevelController seaLevel;
    private MusicController musicController;
    
    // Start is called before the first frame update
    void Start()
    {
        p1Resources = islandA.GetComponentInChildren<ResourceController>();
        p2Resources = islandB.GetComponentInChildren<ResourceController>();
        p1Production = islandA.GetComponentInChildren<ProductionController>();
        p2Production = islandB.GetComponentInChildren<ProductionController>();

        seaLevel = GetComponent<SeaLevelController>();
        musicController = GetComponent<MusicController>();

        started = gameOver = false;

        turnActions = new List<Action>[turnLimit];
        for (int i = 0; i < turnLimit; ++i)
            turnActions[i] = new List<Action>();
    }

    //turn execution
    public void Clicked()
    {
        if ((currentTurn < turnLimit) && (gameOver == false)) {
            currentTurn += 1;

            turnDisplay.text = "End turn " + currentTurn;

            //sea level calculations
            seaLevel.UpdateSeaLevel();

            /* Collect effective production after eventual flood */
            p1Production.UpdateProduction();
            p2Production.UpdateProduction();

            /* Apply delayed actions first */
            foreach (Action action in ActionsForTurn(currentTurn))
                action();

            ResourceValues playerOneResources = p2Resources.trade;
            ResourceValues playerTwoResources = p1Resources.trade;

            //collecting all the resources
            playerOneResources.Add(p1Production.production);
            playerTwoResources.Add(p2Production.production);

            //update game with new amount of resources from production on every turn but 1st
            p1Resources.UpdateResourceCount(playerOneResources);
            p2Resources.UpdateResourceCount(playerTwoResources);

            //draw a new hand of policies for each players
            p1Cards.DrawNewHand();
            p2Cards.DrawNewHand();
        } else if (currentTurn == turnLimit) {
            currentTurn += 1; // Increment so we don't restart the music each click
            musicController.PlayLevel(4);
            GameOver("You win!");
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

    public void GameOver(String reason) {
        gameOver = true;
        turnDisplay.text = "Game has ended! " + reason;
    }

    private List<Action> ActionsForTurn(int turn) {
        return turnActions[turn - 1]; // List is 0 based, turn are 1 based
    }

    public void DelayAction(int turn, Action action) {
        if (currentTurn + turn <= turnActions.Length)
            ActionsForTurn(currentTurn + turn).Add(action);
    }
}
