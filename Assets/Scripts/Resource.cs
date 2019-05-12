using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {
    Unknown,
    ___FOOD__, Crops, SuperCrops, Hydroponic,
    ___ENERGY__, CoalPower, Windmill,
    ___POPULATION__, Town, City
}

[System.Serializable]
public struct ResourceValues {
    public int food, energy, polution, population;

    public void Reset() {
        food = energy = polution = population = 0;
    }

    public ResourceValues Add(ResourceValues v) {
        food += v.food;
        energy += v.energy;
        polution += v.polution;
        population += v.population;

        return this;
    }

    public bool LessEqual(ResourceValues v) {
        return food <= v.food
            && energy <= v.energy
            && polution <= v.polution
            && population <= v.population;
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