using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public IslandProductionController prodController;

    public Resource newResourcePrefab;

    public void TestUpgradeCrops() {
        prodController.UpgradeTypeWithValues(ResourceType.Crops, new ResourceValues {
            food = 1
        });
    }

    public void TestUpgradeCropsDirty() {
        prodController.UpgradeTypeWithValues(ResourceType.Crops, new ResourceValues {
            food = 2, polution = 1
        });
    }

    public void TestReplaceCrops() {
        prodController.ReplaceTypeWithResource(ResourceType.Crops, newResourcePrefab, true);
    }
}
