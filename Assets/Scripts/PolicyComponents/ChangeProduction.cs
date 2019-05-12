using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProduction : PolicyComponent
{
    public ResourceValues changes;

    public override void Execute(PolicyController controller) {
        TradeController tc = controller.island.GetComponentInChildren<TradeController>();
        tc.source.Add(changes);
    }
}