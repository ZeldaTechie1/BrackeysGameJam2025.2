using TMPro;
using UnityEngine;
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
        rectTransform= this.transform.GetComponent<RectTransform>();

        SetText("HELLO WORLD!");
        
        
    }

    private void SetText(string Text)
    {
        ToolTipText.text = Text;

        Vector2 textSize = ToolTipText.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);

        BackgroundTransform.sizeDelta = textSize+paddingSize;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 anchoredPosition=Input.mousePosition/ CanvasRectTransform.localScale.x;

        if (anchoredPosition.x + BackgroundTransform.rect.width > CanvasRectTransform.rect.width)
        {
            anchoredPosition.x = CanvasRectTransform.rect.width - BackgroundTransform.rect.width;
        }
        if (anchoredPosition.x + BackgroundTransform.rect.height > CanvasRectTransform.rect.height)
        {
            anchoredPosition.y = CanvasRectTransform.rect.height - BackgroundTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
        


    }
}
