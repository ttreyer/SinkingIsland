using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private enum TooltipContext {
        NONE, NOT_UI, UI
    };

    private TooltipContext context; // Remember what triggered the tooltip
    private GameObject tooltip;
    private Text title, description;

    private void Start() {
        tooltip = GetComponentInChildren<Canvas>(true).gameObject;
        SetTooltipVisibility(false);

        Text[] texts = GetComponentsInChildren<Text>(true);
        title = texts[0];
        description = texts[1];
    }

    private void SetTooltipVisibility(bool visibility) {
        if (tooltip)
            tooltip.SetActive(visibility);
    }

    public void SetContent(string newTitle, string newDescription) {
        title.text = newTitle;
        description.text = newDescription;
    }

    public void OnMouseEnter() {
        if (context == TooltipContext.NONE) {
            context = TooltipContext.NOT_UI;
            SetTooltipVisibility(true);
        }
    }

    public void OnMouseExit() {
        if (context == TooltipContext.NOT_UI) {
            SetTooltipVisibility(false);
            context = TooltipContext.NONE;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (context == TooltipContext.NONE) {
            context = TooltipContext.UI;
            SetTooltipVisibility(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (context == TooltipContext.UI) {
            SetTooltipVisibility(false);
            context = TooltipContext.NONE;
        }
    }
}
