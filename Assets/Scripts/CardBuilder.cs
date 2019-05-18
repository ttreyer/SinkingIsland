using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBuilder : MonoBehaviour {
    public PolicyController policy;
    public Text title;
    public Text description;
    public Text foodReq, energyReq, popReq;
    public TooltipController tooltip;
    public CardController deck;

    public void BuildWithPolicy(PolicyController pc) {
        if (policy)
            Destroy(policy.gameObject);

        policy = pc;

        title.text = pc.title;
        description.text = pc.effect;

        tooltip.SetContent(pc.title, pc.description);

        foodReq.text = pc.resourceRequirements.food.ToString();
        energyReq.text = pc.resourceRequirements.energy.ToString();
        popReq.text = pc.resourceRequirements.population.ToString();
    }

    public void ExecutePolicy() {
        if (policy) {
            if (policy.Execute()) {
                gameObject.SetActive(false);
                deck.RemovePolicyInstance(policy);
            }
        }
    }
}
