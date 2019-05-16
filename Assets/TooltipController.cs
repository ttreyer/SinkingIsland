using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public enum TooltipContext {
        NONE, NOT_UI, UI
    };

    public TooltipContext context; // Remember what trigger the tooltip
    private GameObject tooltip;

    private void Start() {
        tooltip = GetComponentInChildren<Canvas>(true).gameObject;
        SetTooltipVisibility(false);
    }

    private void SetTooltipVisibility(bool visibility) {
        if (tooltip)
            tooltip.SetActive(visibility);
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
