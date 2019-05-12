using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProductionController : MonoBehaviour {
    public ResourceValues production;

    void Start() {
        UpdateProduction();
    }

    public void UpdateUI() {
        /* ... */
    }

    public void UpdateProduction() {
        production.Reset();

        // Collect resources
        foreach (Resource r in GetResources()) {
            production.Add(r.baseValues);
            production.Add(r.bonusValues);
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