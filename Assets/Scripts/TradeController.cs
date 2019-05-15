using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeController : MonoBehaviour
{
    public GameObject sourceResourceController;
    public GameObject targetResourceController;
    public ResourceController sourceResourceControllerScript;
    public ResourceController targetResourceControllerScript;

    public ResourceValues source;

    // Start is called before the first frame update
    void Start()
    {
        sourceResourceControllerScript = sourceResourceController.GetComponent<ResourceController>();
        targetResourceControllerScript = targetResourceController.GetComponent<ResourceController>();
    }

    public void TradeFoodFromSource(int foodToSend)
    {
        sourceResourceControllerScript.TradeFoodAway(foodToSend);
        source.food = sourceResourceControllerScript.trade.food;
    }

    public void TakeFoodBackToSource(int foodToTake)
    {
        sourceResourceControllerScript.TakeFoodBack(foodToTake);
        source.food = sourceResourceControllerScript.trade.food;
    }

    public void TradeEnergyFromSource(int energyToSend)
    {
        sourceResourceControllerScript.TradeEnergyAway(energyToSend);
        source.energy = sourceResourceControllerScript.trade.energy;
    }

    public void TakeEnergyBackToSource(int energyToTake)
    {
        sourceResourceControllerScript.TakeEnergyBack(energyToTake);
        source.energy = sourceResourceControllerScript.trade.energy;
    }

    public void TradePopsFromSource(int popsToSend)
    {
        sourceResourceControllerScript.TradePopsAway(popsToSend);
        source.population = sourceResourceControllerScript.trade.population;
    }

    public void TakePopsBackToSource(int popsToTake)
    {
        sourceResourceControllerScript.TakePopsBack(popsToTake);
        source.population = sourceResourceControllerScript.trade.population;
    }

    public ResourceValues ProcessTrades()
    {
        ResourceValues tradeResources = new ResourceValues {
            food = source.food,
            energy = source.energy,
            population = source.population,
        };

        source.Reset();

        return tradeResources;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
