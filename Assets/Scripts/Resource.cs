using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ResourceType {
    Unknown,
    ___FOOD__, Crops, SuperCrops, Hydroponic, Cows, Rice, Banana, Corn,
    ___ENERGY__, CoalPower, Windmill, Hydro, Nuclear,
    ___POPULATION__, Town, City, Village
}

[System.Serializable]
public struct ResourceValues {
    public int food, energy, polution, population, populationAngry;

    public int TotalPopulation { get { return population + populationAngry; } }

    public void Reset() {
        food = energy = polution = population = populationAngry = 0;
    }

    public ResourceValues Add(ResourceValues v)
    {
        food += v.food;
        energy += v.energy;
        polution += v.polution;
        population += v.population;
        populationAngry += v.populationAngry;

        return this;

    }

    public ResourceValues AddProduced(ResourceValues v) {
        food += v.food;
        energy += v.energy;
        population += v.population;
        //polution += v.polution;

        return this;
    }

    public ResourceValues AddPopulation(ResourceValues v) {
        population += v.population;
        populationAngry += v.populationAngry;

        return this;
    }

    //only used for Policy requirements
    public ResourceValues Subtract(ResourceValues v)
        {
            food -= v.food;
            energy -= v.energy;
            population -= v.population;

            return this;
        }

    public bool LessEqual(ResourceValues v) {
        return food <= v.food
            && energy <= v.energy
            && polution <= v.polution
            && population <= v.population
            && populationAngry <= v.populationAngry;
    }

    public void SetMin(int min = 0) {
        food = Math.Max(min, food);
        energy = Math.Max(min, energy);
        polution = Math.Max(min, polution);
        population = Math.Max(min, population);
        populationAngry = Math.Max(min, populationAngry);
    }

    public ResourceValues Revert() {
        food = -food;
        energy = -energy;
        polution = -polution;
        population = -population;
        populationAngry = -populationAngry;

        return this;
    }

    public override string ToString() {
        List<string> resources = new List<string>();

        resources.Add("Food: " + food);
        resources.Add("Energy: " + energy);
        resources.Add("Pollution: " + polution);
        resources.Add("Population: " + population);
        resources.Add("Population angry: " + populationAngry);

        return String.Join(", ", resources);
    }
}

public class Resource : MonoBehaviour {
    public ResourceType type;
    public ResourceValues baseValues;
    public ResourceValues bonusValues;
    public string title, description;
    public TooltipController tooltip;

    void Start() {
        UpdateTooltip();
    }

    public void UpgradeValues(ResourceValues addedValues) {
        bonusValues.Add(addedValues);
        UpdateTooltip();
    }

    public override string ToString() {
        List<string> resources = new List<string>();

        int food = baseValues.food + bonusValues.food;
        if (food != 0)
            resources.Add("Food: " + food.ToString(NUMBER_FORMAT));

        int energy = baseValues.energy + bonusValues.energy;
        if (energy != 0)
            resources.Add("Energy: " + energy.ToString(NUMBER_FORMAT));

        int polution = baseValues.polution + bonusValues.polution;
        if (polution != 0)
            resources.Add("Pollution: " + polution.ToString(NUMBER_FORMAT));

        int population = baseValues.population + bonusValues.population;
        if (population != 0)
            resources.Add("Population: " + population.ToString(NUMBER_FORMAT));

        int populationAngry = baseValues.populationAngry + bonusValues.populationAngry;
        if (populationAngry != 0)
            resources.Add("Population angry: " + populationAngry.ToString(NUMBER_FORMAT));

        return String.Join("\n", resources);
    }

    private string NUMBER_FORMAT = "+#;-#;0";
    private void UpdateTooltip() {
        if (tooltip)
            tooltip.SetContent(title, ToString());
    }

    public ResourceValues GetValues() {
        return new ResourceValues()
            .Add(baseValues)
            .Add(bonusValues);
    }
}
