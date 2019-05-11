using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandProductionController : MonoBehaviour {
    public int food, energy, polution, population;

    public void UpdateProduction() {
        // Reset counters
        food = energy = polution = population = 0;

        // Collect resources
        foreach (Resource r in GetComponentsInChildren<Resource>(false)) {
            food += r.food;
            energy += r.energy;
            polution += r.polution;
            population += r.population;
        }
    }

    // Update is called once per frame
    void Update() {
        UpdateProduction();
    }
}