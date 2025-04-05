using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClosureArea : MonoBehaviour, IPointerClickHandler
{
    public Action HandleClosure { private get; set; }

    public void OnPointerClick(PointerEventData eventData) {
        HandleClosure();
    }
}
