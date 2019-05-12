using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicyController : MonoBehaviour {
    public GameObject island;
    public string title, description;
    public ResourceValues resourceRequirements;
    public int ecologyLevel;

    private bool CheckRequirements() {
        ProductionController pc = island.GetComponentInChildren<ProductionController>();
        return resourceRequirements.LessEqual(pc.production);
    }

    public void Execute() {
        if (!CheckRequirements())
            return;

        foreach (PolicyComponent component in GetComponents<PolicyComponent>())
            component.Execute(this);
    }
}
