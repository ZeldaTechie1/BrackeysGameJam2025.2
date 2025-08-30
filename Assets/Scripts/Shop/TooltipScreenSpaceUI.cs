using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TooltipScreenSpaceUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private RectTransform CanvasRectTransform;
    private RectTransform BackgroundTransform;
    private TextMeshProUGUI ToolTipText;
    private RectTransform rectTransform;

    void Awake()
    {
        BackgroundTransform = transform.Find("Background").GetComponent<RectTransform>();
        ToolTipText= transform.Find("Text").GetComponent<TextMeshProUGUI>();
        CanvasRectTransform=transform.GetComponent<RectTransform>();
        rectTransform= transform.GetComponent<RectTransform>();

        SetText("HELLO WORLD!");
        
        
    }

    private void SetText(string Text)
    {
       
        ToolTipText.SetText(Text);
        ToolTipText.ForceMeshUpdate();
        Vector2 textSize = ToolTipText.GetRenderedValues(false).Abs();

        BackgroundTransform.sizeDelta = textSize;
        
    }

    // Update is called once per frame
    void Update()
    {

        UpdateToolTip();

    }

    public void UpdateToolTip()
    {
        Vector2 anchoredPosition = Mouse.current.position.ReadValue()/ CanvasRectTransform.localScale.x;

        if (anchoredPosition.x + BackgroundTransform.rect.width < CanvasRectTransform.rect.width)
        {
            anchoredPosition.x = CanvasRectTransform.rect.width - BackgroundTransform.rect.width;
        }
        if (anchoredPosition.y + BackgroundTransform.rect.height < CanvasRectTransform.rect.height)
        {
            anchoredPosition.y = CanvasRectTransform.rect.height - BackgroundTransform.rect.height;
        }

        rectTransform.position = anchoredPosition;




    }
}
