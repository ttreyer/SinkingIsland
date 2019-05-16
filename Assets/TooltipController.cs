using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class TooltipController : MonoBehaviour {
    private GameObject tooltip;

    private void Start() {
        tooltip = GetComponentInChildren<Canvas>(true).gameObject;
        SetTooltipVisibility(false);
    }

    public void OnMouseEnter() {
        SetTooltipVisibility(true);
    }

    public void OnMouseExit() {
        SetTooltipVisibility(false);
    }

    private void SetTooltipVisibility(bool visibility) {
        if (tooltip)
            tooltip.SetActive(visibility);
    }
}
