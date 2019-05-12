using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProduction : PolicyComponent
{
    public ResourceValues changes;

    public override void Execute(PolicyController controller) {
        ProductionController pc = controller.island.GetComponentInChildren<ProductionController>();
        pc.production.Add(changes);
        pc.UpdateUI();
    }
}