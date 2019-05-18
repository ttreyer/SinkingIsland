using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardController : MonoBehaviour {
    public CardBuilder cardPrefab;
    public PolicyController[] policies;
    public int maxEcologyLevel = 10;
    public int ecologyWindow = 2;
    public CardBuilder[] currentHand;

    private List<PolicyController>[] policiesByEcology;


    // Start is called before the first frame update
    void Start() {
        // Group policies by ecology level
        policiesByEcology = new List<PolicyController>[maxEcologyLevel + 1];
        for (int i = 0; i < maxEcologyLevel + 1; ++i)
            policiesByEcology[i] = new List<PolicyController>();

        foreach (PolicyController pc in policies)
            for (int i = 0; i < pc.cardCountPerDeck; i++)
                policiesByEcology[pc.ecologyLevel].Add(pc);
    }

    public void DrawNewHand() {
        foreach (CardBuilder card in currentHand)
            card.gameObject.SetActive(true);

        List<PolicyController> currentPolicies = GetRandomPolicies(0, currentHand.Length);
        for (int i = 0; i < currentHand.Length; ++i) {
            CardBuilder card = currentHand[i];
            PolicyController pci = Instantiate(currentPolicies[i], card.transform);
            pci.island = gameObject;
            card.BuildWithPolicy(pci);
        }
    }

    public void RemovePolicyInstance(PolicyController pc) {
        // Use title to find the policy to remove
        int pcid = policiesByEcology[pc.ecologyLevel]
            .FindIndex((spc) => spc.title == pc.title);
        if (pcid >= 0)
            policiesByEcology[pc.ecologyLevel].RemoveAt(pcid);
    }

    private List<PolicyController> GetRandomPolicies(int ecologyLevel, int policyCount) {
        List<PolicyController> results = new List<PolicyController>();
        List<PolicyController> reasonablePolicies = GetPoliciesAroundLevel(ecologyLevel);

        int boundedCount = System.Math.Min(policyCount, reasonablePolicies.Count);
        return reasonablePolicies
            .OrderBy(p => Random.value)
            .Take(boundedCount)
            .ToList();
    }

    private List<PolicyController> GetPoliciesAroundLevel(int ecologyLevel) {
        List<PolicyController> results = new List<PolicyController>();

        for (int i = 0; i <= maxEcologyLevel; ++i) {
            if (System.Math.Abs(i) <= ecologyWindow)
                results.AddRange(policiesByEcology[i]);
        }

        return results;
    }
}
