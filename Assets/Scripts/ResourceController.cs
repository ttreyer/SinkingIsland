using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    //public int islandFood;
    //public int islandEnergy;
    //public int islandPops;
    //public int islandPopsAngry;

    public ResourceValues current;
    public ResourceValues trade;

    public Text foodToTradeDisplay;
    public Text energyToTradeDisplay;
    public Text popsToTradeDisplay;

    public Text currentFoodDisplay;
    public Text currentEnergyDisplay;
    public Text currentPopsDisplay;
    public Text currentPopsAngryDisplay;

    private int popFoodDifference;
    private int popEnergyDifference;
    private int popsLostToAnger;

    // Start is called before the first frame update
    void Start()
    {

    }

    ////this is called to check the carrying capacity of the island's resources, affected by modifiers and sea level rise
    ////will need to be amending this a lot once we integrate the prototype
    //public void AssessIslandResources()
    //{
    //    current.food = islandFood;
    //    current.energy = islandEnergy;
    //}

    public void UpdateResourceCount(string player, ResourceValues newResources)
    {
        current.Reset();
        current.Add(newResources);
        //check food and energy deficits
        if (current.food <= current.population)
        {
            popFoodDifference = (current.population - current.food);

        }

        if (current.energy <= current.population)
        {
            popEnergyDifference = (current.population - current.energy);

        }

        //calculate final total pop loss
        popsLostToAnger = Math.Max(popFoodDifference,popEnergyDifference);
        current.population -= popsLostToAnger;
        current.populationAngry += popsLostToAnger;

        //clear trades
        trade.Reset();

        UpdateCurrentUI(player);
    }

    public void UpdateCurrentUI(string player) {
        return;
        //update mockup UI
        currentFoodDisplay.text = player + " Current Food: " + current.food;
        currentEnergyDisplay.text = player + " Current Energy: " + current.energy;
        currentPopsDisplay.text = player + " Current Pops: " + current.population;
        currentPopsAngryDisplay.text = player + " Current Angry: " + current.populationAngry;

    }

    public void UpdateTradeUI() {
        return;
        foodToTradeDisplay.text = "To Trade: " + trade.food;
        energyToTradeDisplay.text = "To Trade: " + trade.energy;
        popsToTradeDisplay.text = "To Trade: " + trade.population;
    }

    //there's probably a super easy way to refactor these 6 trade methods but for now it works so I leave it as is
    public void TradeFoodAway(int Food)
    {
        if (current.food != 0)
        {
            current.food -= Food;
            trade.food += Food;
            UpdateTradeUI();
        }
    }

    public void TakeFoodBack(int Food)
    {
        if (trade.food != 0)
        {
            current.food += Food;
            trade.food -= Food;
            UpdateTradeUI();
        }
    }

    public void TradeEnergyAway(int Energy)
    {
        if (current.energy != 0)
        {
            current.energy -= Energy;
            trade.energy += Energy;
            UpdateTradeUI();
        }
    }

    public void TakeEnergyBack(int Energy)
    {
        if (trade.energy != 0)
        {
            current.energy += Energy;
            trade.energy -= Energy;
            UpdateTradeUI();
        }
    }

    public void TradePopsAway(int Pops)
    {
        if (current.population != 0)
        {
            current.population -= Pops;
            trade.population += Pops;
            UpdateTradeUI();
        }
    }

    public void TakePopsBack(int Pops)
    {
        if (trade.population != 0)
        {
            current.population += Pops;
            trade.population -= Pops;
            UpdateTradeUI();
        }
    }

    public void GetFoodFromTrade(int Food)
    {
        current.food += Food;
    }

    public void GetEnergyFromTrade(int Energy)
    {
        current.energy += Energy;
    }

    public void GetPopsFromTrade(int Pops)
    {
        current.population += Pops;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
