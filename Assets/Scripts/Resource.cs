using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {
    Unknown,
    ___FOOD__, Crops, Hydroponic,
    ___ENERGY__, CoalPower, Windmill,
    ___POPULATION__, Town, City
}

public class Resource : MonoBehaviour {
    public ResourceType type;
    public int food, energy, polution, population;
}
