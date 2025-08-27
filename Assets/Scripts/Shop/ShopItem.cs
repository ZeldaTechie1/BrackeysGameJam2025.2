using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private string ItemDescription;
    [SerializeField]
    private string ItemName;
    [SerializeField]
    public GameObject Item;

  

    private Toggle toggle;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUp();
        toggle= this.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetUp()
    {


    }

   public string GetItemName()
   {
        return ItemName;
   }
    public string GetItemDescrption()
    {
        return ItemDescription;
    }

    public void SelectItem()
    {
       

        if(toggle!=null&&toggle.isOn)
        {
            ShopEventManager.Items.SelectItem(this);
            toggle.group.SetAllTogglesOff(false);
        }
        else if (toggle!=null&&!toggle.isOn) 
        {
            ShopEventManager.Items.DeselectItem(true);
          
        }

    }
}
