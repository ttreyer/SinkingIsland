using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeController : MonoBehaviour
{
    public GameObject sourceResourceController;
    public GameObject targetResourceController;
    private ResourceController sourceResourceControllerScript;
    private ResourceController targetResourceControllerScript;
    public int sourceFood;
    public int sourceEnergy;
    public int sourcePops;

    // Start is called before the first frame update
    void Start()
    {
        sourceResourceControllerScript = sourceResourceController.GetComponent<ResourceController>();
        targetResourceControllerScript = targetResourceController.GetComponent<ResourceController>();
    }

    public void TradeFoodFromSource(int foodToSend)
    {
        sourceResourceControllerScript.TradeFoodAway(foodToSend);
        sourceFood = sourceResourceController.GetComponent<ResourceController>().foodToTrade;
    }

    public void TakeFoodBackToSource(int foodToTake)
    {
        sourceResourceControllerScript.TakeFoodBack(foodToTake);
        sourceFood = sourceResourceController.GetComponent<ResourceController>().foodToTrade;
    }

    public void TradeEnergyFromSource(int energyToSend)
    {
        sourceResourceControllerScript.TradeEnergyAway(energyToSend);
        sourceEnergy = sourceResourceController.GetComponent<ResourceController>().energyToTrade;
    }

    public void TakeEnergyBackToSource(int energyToTake)
    {
        sourceResourceControllerScript.TakeEnergyBack(energyToTake);
        sourceEnergy = sourceResourceController.GetComponent<ResourceController>().energyToTrade;
    }

    public void TradePopsFromSource(int popsToSend)
    {
        sourceResourceControllerScript.TradePopsAway(popsToSend);
        sourcePops = sourceResourceController.GetComponent<ResourceController>().popsToTrade;
    }

    public void TakePopsBackToSource(int popsToTake)
    {
        sourceResourceControllerScript.TakePopsBack(popsToTake);
        sourcePops = sourceResourceController.GetComponent<ResourceController>().popsToTrade;
    }

    public void ProcessTrades()
    {
        targetResourceControllerScript.GetFoodFromTrade(sourceFood);
        targetResourceControllerScript.GetEnergyFromTrade(sourceEnergy);
        targetResourceControllerScript.GetPopsFromTrade(sourcePops);
        sourceFood = 0;
        sourceEnergy = 0;
        sourcePops = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
