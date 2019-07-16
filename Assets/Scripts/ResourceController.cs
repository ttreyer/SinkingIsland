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

    [Header("Trade UI")]
    public Text foodToTradeDisplay;
    public Text energyToTradeDisplay;
    public Text popsToTradeDisplay;

    [Header("Stock UI")]
    public Text currentFoodDisplay;
    public Text currentEnergyDisplay;
    public Text currentPopsDisplay;
    public Text currentPopsAngryDisplay;

    private int popFoodDifference;
    private int popEnergyDifference;
    private int popsLostToAnger;

    public void UpdateResourceCount(ResourceValues changes)
    {
        // The TurnController put the production and the received trades in `changes`
        current.Add(changes);

        // Don't accumulate the polution
        // The SeaLevelController uses this value to do the accumulation
        // So we set it to the production of the polution
        current.polution = changes.polution;

        // Calculate subsequent food and energy loss based off population
        current.food -= current.TotalPopulation;
        current.energy -= current.TotalPopulation;

        // Take the biggest discontent or 0 if none
        int popUnsatisfied = Math.Max(0, Math.Max(-current.food, -current.energy));

        // Cap population turning angry by the current happy population
        int popTurningAngry = Math.Min(current.population, popUnsatisfied);

        current.population -= popTurningAngry;
        current.populationAngry += popTurningAngry;

        // Ensure we don't have any negative food or energy
        current.SetMin(0);

        // Clear trades from last turn
        trade.Reset();

        UpdateUI();
    }

    public void UpdateUI() {
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
    public void TradeFood(int food) {
        if (current.food - food >= 0 && trade.food + food >= 0) {
            current.food -= food;
            trade.food += food;

            UpdateUI();
        }
    }

    public void TradeEnergy(int energy) {
        if (current.energy - energy >= 0 && trade.energy + energy >= 0) {
            current.energy -= energy;
            trade.energy += energy;

            UpdateUI();
        }
    }

    public void TradePopulation(int population) {
        if (current.population - population >= 0 && trade.population + population >= 0) {
            current.population -= population;
            trade.population += population;

            UpdateUI();
        }
    }
}
