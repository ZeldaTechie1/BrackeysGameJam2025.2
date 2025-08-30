using UnityEngine;
using UnityEngine.UI;

public class CollectionItem : MonoBehaviour
{
    [SerializeField]
    public GameObject Display;
    public TazoDisplay TazoDisplay;
    [SerializeField]
    public TazoItem TazoItem;


    private Toggle toggle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggle = this.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp()
    {
        if (TazoItem != null) 
        {
            TazoDisplay = Display.GetComponent<TazoDisplay>();
            var top = TazoItem.GetTopMaterial();
            var bottom = TazoItem.GetBottomMaterial();
            TazoDisplay.SetMaterials(top, bottom);
        }

    }

    public void FlipTazo()
    {
        TazoDisplay.Flip();
    }

    public string GetItemName()
    {
        return TazoItem.GetName();
    }
    public string GetItemDescrption()
    {
        return TazoItem.GetDescription();
    }

    public void SelectItem()
    {
       
        if (toggle != null && toggle.isOn)
        {
            CollectionEventManager.CollectionItems.SelectItem(this);
            toggle.group.SetAllTogglesOff(false);
            //Add Tazo Display Wiggle
        }
        else if (toggle != null && !toggle.isOn)
        {
           CollectionEventManager.CollectionItems.DeselectItem(true);
           //Remove Tazo Display Wiggle
        }

    }

}

