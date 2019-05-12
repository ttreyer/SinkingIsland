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

    public int currentFood;
    public int currentEnergy;
    public int currentPops;
    public int currentPopsAngry;

    public int foodToTrade;
    public int energyToTrade;
    public int popsToTrade;

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
    //    currentFood = islandFood;
    //    currentEnergy = islandEnergy;
    //}

    public void UpdateResourceCount(string player)
    {
        //check food and energy deficits
        if (currentFood <= currentPops)
        {
            popFoodDifference = (currentPops - currentFood);

        }

        if (currentEnergy <= currentPops)
        {
            popEnergyDifference = (currentPops - currentEnergy);

        }

        //calculate final total pop loss
        popsLostToAnger = Math.Max(popFoodDifference,popEnergyDifference);
        currentPops -= popsLostToAnger;
        currentPopsAngry += popsLostToAnger;

        //clear trades
        foodToTrade = 0;
        energyToTrade = 0;
        popsToTrade = 0;

        //update mockup UI
        currentFoodDisplay.text =  player + " Current Food: " + currentFood;
        currentEnergyDisplay.text = player + " Current Energy: " + currentEnergy;
        currentPopsDisplay.text = player + " Current Pops: " + currentPops;
        currentPopsAngryDisplay.text = player + " Current Angry: " + currentPopsAngry;
        foodToTradeDisplay.text = "To Trade: " + foodToTrade;
        energyToTradeDisplay.text = "To Trade: " + energyToTrade;
        popsToTradeDisplay.text = "To Trade: " + popsToTrade;
    }

    //there's probably a super easy way to refactor these 6 trade methods but for now it works so I leave it as is
    public void TradeFoodAway(int Food)
    {
        if (currentFood != 0)
        {
            currentFood -= Food;
            foodToTrade += Food;
            foodToTradeDisplay.text = "To Trade: " + foodToTrade;
        }
    }

    public void TakeFoodBack(int Food)
    {
        if (foodToTrade != 0)
        {
            currentFood += Food;
            foodToTrade -= Food;
            foodToTradeDisplay.text = "To Trade: " + foodToTrade;
        }
    }

    public void TradeEnergyAway(int Energy)
    {
        if (currentEnergy != 0)
        {
            currentEnergy -= Energy;
            energyToTrade += Energy;
            energyToTradeDisplay.text = "To Trade: " + energyToTrade;
        }
    }

    public void TakeEnergyBack(int Energy)
    {
        if (energyToTrade != 0)
        {
            currentEnergy += Energy;
            energyToTrade -= Energy;
            energyToTradeDisplay.text = "To Trade: " + energyToTrade;
        }
    }

    public void TradePopsAway(int Pops)
    {
        if (currentPops != 0)
        {
            currentPops -= Pops;
            popsToTrade += Pops;
            popsToTradeDisplay.text = "To Trade: " + popsToTrade;
        }
    }

    public void TakePopsBack(int Pops)
    {
        if (popsToTrade != 0)
        {
            currentPops += Pops;
            popsToTrade -= Pops;
            popsToTradeDisplay.text = "To Trade: " + popsToTrade;
        }
    }

    public void GetFoodFromTrade(int Food)
    {
        currentFood += Food;
    }

    public void GetEnergyFromTrade(int Energy)
    {
        currentEnergy += Energy;
    }

    public void GetPopsFromTrade(int Pops)
    {
        currentPops += Pops;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
