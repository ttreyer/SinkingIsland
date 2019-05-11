using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public int currentFood;
    public int currentEnergy;
    public int currentPops;
    public int currentPopsAngry;

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

    public void UpdateResourceCount()
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

        //update mockup UI
        currentFoodDisplay.text = "P1 Current Food: " + currentFood;
        currentEnergyDisplay.text = "P1 Current Energy: " + currentEnergy;
        currentPopsDisplay.text = "P1 Current Pops: " + currentPops;
        currentPopsAngryDisplay.text = "P1 Current Angry: " + currentPopsAngry;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
