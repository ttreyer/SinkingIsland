using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceResource : PolicyComponent {
    public ResourceType type;
    public Resource newResourceType;
    public bool keepUpgrades;

    public override void Execute(PolicyController controller) {
        controller.island.GetComponentInChildren<ProductionController>()
            .ReplaceTypeWithResource(type, newResourceType, keepUpgrades);
    }
}
