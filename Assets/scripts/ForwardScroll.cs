using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ForwardScroll : MonoBehaviour, IScrollHandler
{
    [SerializeField] private ScrollRect scrollRect;

    public void OnScroll(PointerEventData eventData)
    {
        scrollRect.OnScroll(eventData);
    }
}
