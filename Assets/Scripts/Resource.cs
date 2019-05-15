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

    public ResourceValues Add(ResourceValues v) {
        food += v.food;
        energy += v.energy;
        polution += v.polution;
        population += v.population;
        populationAngry += v.populationAngry;

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
}

public class Resource : MonoBehaviour {
    public ResourceType type;
    public ResourceValues baseValues;
    public ResourceValues bonusValues;

    public void UpgradeValues(ResourceValues addedValues) {
        bonusValues.Add(addedValues);
    }

    public ResourceValues GetValues() {
        return new ResourceValues()
            .Add(baseValues)
            .Add(bonusValues);
    }
}