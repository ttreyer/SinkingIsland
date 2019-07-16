using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ProductionController : MonoBehaviour {
    public ResourceValues production;
    //public TooltipController tooltip;

    [Header("Production UI")]
    public Text foodDisplay;
    public Text energyDisplay;
    public Text popsDisplay;
    public Text popsAngryDisplay;

    private string NUMBER_FORMAT = "+#;-#;+0";

    void Start() {
        UpdateProduction();
    }

    public void UpdateUI() {
        foodDisplay.text = production.food.ToString(NUMBER_FORMAT);
        energyDisplay.text = production.energy.ToString(NUMBER_FORMAT);
        popsDisplay.text = production.population.ToString(NUMBER_FORMAT);
        popsAngryDisplay.text = production.populationAngry.ToString(NUMBER_FORMAT);
    }

    public void UpdateProduction() {
        production.Reset();

        // Collect resources
        foreach (Resource r in GetResources()) {
            production.Add(r.baseValues);
            production.Add(r.bonusValues);
            //tooltip.SetContent(r.title, r.description);
        }

        UpdateUI();
    }

    public void UpgradeTypeWithValues(ResourceType type, ResourceValues addValues) {
        foreach (Resource r in GetResourcesWithType(type))
            r.UpgradeValues(addValues);
    }

    public void ReplaceTypeWithResource(ResourceType type, Resource newResource, bool keepBonus = false) {
        foreach (Resource r in GetResourcesWithType(type)) {
            Resource nr = Instantiate(newResource, r.transform.position, r.transform.rotation, r.transform.parent);
            if (keepBonus)
                nr.UpgradeValues(r.bonusValues);
            Destroy(r.gameObject);
        }
    }

    private Resource[] GetResources() {
        return GetComponentsInChildren<Resource>(false);
    }

    private Resource[] GetResourcesWithType(ResourceType type) {
        return GetResources().Where(r => r.type == type).ToArray();
    }
}