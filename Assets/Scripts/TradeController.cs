using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeController : MonoBehaviour
{
    public GameObject sourceResourceController;
    public GameObject targetResourceController;
    private ResourceController sourceResourceControllerScript;
    private ResourceController targetResourceControllerScript;

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
        source.food = sourceResourceController.GetComponent<ResourceController>().trade.food;
    }

    public void TakeFoodBackToSource(int foodToTake)
    {
        sourceResourceControllerScript.TakeFoodBack(foodToTake);
        source.food = sourceResourceController.GetComponent<ResourceController>().trade.food;
    }

    public void TradeEnergyFromSource(int energyToSend)
    {
        sourceResourceControllerScript.TradeEnergyAway(energyToSend);
        source.energy = sourceResourceController.GetComponent<ResourceController>().trade.energy;
    }

    public void TakeEnergyBackToSource(int energyToTake)
    {
        sourceResourceControllerScript.TakeEnergyBack(energyToTake);
        source.energy = sourceResourceController.GetComponent<ResourceController>().trade.energy;
    }

    public void TradePopsFromSource(int popsToSend)
    {
        sourceResourceControllerScript.TradePopsAway(popsToSend);
        source.population = sourceResourceController.GetComponent<ResourceController>().trade.population;
    }

    public void TakePopsBackToSource(int popsToTake)
    {
        sourceResourceControllerScript.TakePopsBack(popsToTake);
        source.population = sourceResourceController.GetComponent<ResourceController>().trade.population;
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
