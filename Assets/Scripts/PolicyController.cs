using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicyController : MonoBehaviour {
    public GameObject island;
    public int cardCountPerDeck = 1;
    public string title, description, effect;
    public ResourceValues resourceRequirements;
    public int ecologyLevel;

    private bool CheckRequirements() {
        ResourceController pc = island.GetComponentInChildren<ResourceController>();
        return resourceRequirements.LessEqual(pc.current);
    }

    public bool Execute(bool checkRequirements = true) {
        if (checkRequirements && !CheckRequirements())
            return false;

        foreach (PolicyComponent component in GetComponents<PolicyComponent>())
            component.Execute(this);

        return true;
    }
}
