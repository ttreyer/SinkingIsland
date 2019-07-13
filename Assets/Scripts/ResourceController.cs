using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public ResourceValues current;
    public ResourceValues trade;
    public int initialPopulation;

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

    public void UpdateResourceCount(ResourceValues newResources)
    {
        ResourceValues stock = current;
        current = new ResourceValues();

        current.AddProduced(stock); // Add previous produced resources
        current.Add(newResources);  // Add current production and poulation
        current.AddPopulation(trade.Revert());// Remove what was traded last turn
        //ag does this trade work?

        //calculate subsequent food and energy loss based off population
        current.food -= current.TotalPopulation;
        current.energy -= current.TotalPopulation;

        // Take the biggest discontent or 0 if none
        int popUnsatisfied = Math.Max(0, Math.Max(-current.food, -current.energy));

        // Cap population turning angry by the current happy population
        int popTurningAngry = Math.Min(current.population, popUnsatisfied);

        current.population -= popTurningAngry;
        current.populationAngry += popTurningAngry;

        // Ensure we don't have any negative value
        current.SetMin(0);

        //clear trades
        trade.Reset();

        UpdateCurrentUI();
        UpdateTradeUI();
    }

    public void UpdateCurrentUI() {
        //update mockup UI
        currentFoodDisplay.text = current.food.ToString();
        currentEnergyDisplay.text = current.energy.ToString();
        currentPopsDisplay.text = current.population.ToString();
        currentPopsAngryDisplay.text = current.populationAngry.ToString();

    }

    public void UpdateTradeUI()
    {
        foodToTradeDisplay.text = trade.food.ToString();
        energyToTradeDisplay.text = trade.energy.ToString();
        popsToTradeDisplay.text = trade.population.ToString();
    }

    //there's probably a super easy way to refactor these 6 trade methods but for now it works so I leave it as is
    public void TradeFoodAway(int Food)
    {
        if (current.food != 0)
        {
            current.food -= Food;
            trade.food += Food;
            UpdateCurrentUI();
            UpdateTradeUI();
        }
    }

    public void TakeFoodBack(int Food)
    {
        if (trade.food != 0)
        {
            current.food += Food;
            trade.food -= Food;
            UpdateCurrentUI();
            UpdateTradeUI();
        }
    }

    public void TradeEnergyAway(int Energy)
    {
        if (current.energy != 0)
        {
            current.energy -= Energy;
            trade.energy += Energy;
            UpdateCurrentUI();
            UpdateTradeUI();
        }
    }

    public void TakeEnergyBack(int Energy)
    {
        if (trade.energy != 0)
        {
            current.energy += Energy;
            trade.energy -= Energy;
            UpdateCurrentUI();
            UpdateTradeUI();
        }
    }

    public void TradePopsAway(int Pops)
    {
        if (current.population != 0)
        {
            current.population -= Pops;
            trade.population += Pops;
            UpdateCurrentUI();
            UpdateTradeUI();
        }
    }

    public void TakePopsBack(int Pops)
    {
        if (trade.population != 0)
        {
            current.population += Pops;
            trade.population -= Pops;
            UpdateCurrentUI();
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
