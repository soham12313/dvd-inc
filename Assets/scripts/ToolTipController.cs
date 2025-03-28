using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI count;

    public void Update()
    {
        transform.position = Input.mousePosition + new Vector3(1, 1);
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
