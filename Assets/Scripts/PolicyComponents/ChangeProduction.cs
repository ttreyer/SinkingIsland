using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProduction : PolicyComponent
{
    public ResourceValues changes;

    public override void Execute(PolicyController controller) {
        ResourceController rc = controller.island.GetComponentInChildren<ResourceController>();
        rc.current.Add(changes);
    }
}