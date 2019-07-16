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

    [Header("Tooltips")]
    public Text foodTooltip;
    public Text energyTooltip;
    public Text popsTooltip;
    public Text popsAngryTooltip;

    private readonly string PRODUCTION_FORMAT = "(+#);(-#);(+0)";
    private readonly string SOURCE_FORMAT = "+#;-#";

    private List<string> foodSources, energySources, popsSources, popsAngrySources;

    private void Start() {
        foodSources = new List<string>();
        energySources = new List<string>();
        popsSources = new List<string>();
        popsAngrySources = new List<string>();
    }

    public void UpdateUI() {
        foodDisplay.text = production.food.ToString(PRODUCTION_FORMAT);
        energyDisplay.text = production.energy.ToString(PRODUCTION_FORMAT);
        popsDisplay.text = production.population.ToString(PRODUCTION_FORMAT);
        popsAngryDisplay.text = production.populationAngry.ToString(PRODUCTION_FORMAT);
    }

    private void ResetTooltips() {
        foodSources.Clear();
        energySources.Clear();
        popsSources.Clear();
        popsAngrySources.Clear();
    }

    public void UpdateTooltips() {
        foodTooltip.text = string.Join("\n", foodSources);
        energyTooltip.text = string.Join("\n", energySources);
        popsTooltip.text = string.Join("\n", popsSources);
        popsAngryTooltip.text = string.Join("\n", popsAngrySources);
    }

    private void AddSource(Resource resource) {
        ResourceValues v = resource.TotalValues;

        if (v.food != 0)
            foodSources.Add(resource.title + ": " + v.food.ToString(SOURCE_FORMAT));

        if (v.energy != 0)
            energySources.Add(resource.title + ": " + v.energy.ToString(SOURCE_FORMAT));

        if (v.population != 0)
            popsSources.Add(resource.title + ": " + v.population.ToString(SOURCE_FORMAT));

        if (v.populationAngry != 0)
            popsAngrySources.Add(resource.title + ": " + v.populationAngry.ToString(SOURCE_FORMAT));
    }

    public void UpdateProduction() {
        production.Reset();
        ResetTooltips();

        // Collect resources
        foreach (Resource r in GetResources()) {
            production.Add(r.TotalValues);
            AddSource(r);
        }

        UpdateUI();
        UpdateTooltips();
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