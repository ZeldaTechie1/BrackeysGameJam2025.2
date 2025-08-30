using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{

    [SerializeField]
    public ChipBag ChipBag;

    [SerializeField]
    public Image BagImage;

  

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


    public void SetUp()
    {
        BagImage.sprite = ChipBag.GetSprite();

    }

   public string GetItemName()
   {
        return ChipBag.GetName();
   }
    public string GetItemDescrption()
    {
        return ChipBag.GetDescription();
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

    public TazoItem PullTazo()
    {
        return ChipBag.GetRandomTazo();
    }
}
