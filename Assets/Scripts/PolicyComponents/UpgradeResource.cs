using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeResource : PolicyComponent {
    public ResourceType type;
    public ResourceValues upgradeValues;

    public override void Execute(PolicyController controller) {
        controller.island
            .GetComponentInChildren<ProductionController>()
            .UpgradeTypeWithValues(type, upgradeValues);
    }
}
