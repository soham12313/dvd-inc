using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private Canvas canvasTransform;
    private Vector2 mousePos;

    private void OnEnable()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform.transform as RectTransform, Input.mousePosition, canvasTransform.worldCamera, out mousePos);
        transform.localPosition = mousePos;
    }

    public void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform.transform as RectTransform, Input.mousePosition, canvasTransform.worldCamera, out mousePos);
        transform.localPosition = mousePos;
    }

    public void SetTitleText(string text)
    {
        this.title.text = text;
    }

    public void SetDescriptionText(string text)
    {
        this.description.text = text;
    }

    public void SetCountText(string text)
    {
        this.count.text = text;
    }

}
