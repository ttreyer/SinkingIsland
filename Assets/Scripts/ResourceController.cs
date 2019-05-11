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
        //UpdateResourceCount();
    }

    public void UpdateResourceCount()
    {
        //check food and energy deficits
        if (currentFood <= currentPops)
        {
            popFoodDifference = (currentPops - currentFood);
            //popsLostToAnger = popResourceDifference;

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
        currentFoodDisplay.text = "Current Food: " + currentFood;
        currentEnergyDisplay.text = "Current Energy: " + currentEnergy;
        currentPopsDisplay.text = "Current Pops: " + currentPops;
        currentPopsAngryDisplay.text = "Current Angry Pops: " + currentPopsAngry;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
