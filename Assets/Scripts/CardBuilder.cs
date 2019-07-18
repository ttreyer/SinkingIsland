using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBuilder : MonoBehaviour {
    public PolicyController policy;
    public CardController deck;
    public bool isHeld;

    [Header("UI")]
    public Text title;
    public Text description;
    public Text foodReq, energyReq, popReq;
    public Button cardButton;
    public TooltipController tooltip;

    private AudioSource executeSFX;

    private void Start() {
        executeSFX = GetComponent<AudioSource>();
    }

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
                SetEnabled(false);
                deck.RemovePolicyInstance(policy);
                isHeld = false;
                cardButton.image.color = Color.white;
                executeSFX.Play();
            }
        }
    }

    public void HoldPolicy()
    {
        if (isHeld == false)
        {
            cardButton.image.color = Color.yellow;
            isHeld = true;
        }
        else if (isHeld == true)
        {
            cardButton.image.color = Color.white;
            isHeld = false;
        }
    }

    /* The new UI uses auto layout to position cards.
     * Dis-activating the card removes it from the auto layout.
     * Instead of having 3 cards and a blank space,
     * we get 3 cards and a larger End turn button.
     * The solution is to disable each child scripts,
     * which disable the render and effectively hide the card.
     */
    public void SetEnabled(bool state) {
        foreach (var script in GetComponentsInChildren<MonoBehaviour>())
            script.enabled = state;
    }
}
