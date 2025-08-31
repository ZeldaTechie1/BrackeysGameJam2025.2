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

    [SerializeField]
    public GameObject Selection;

  

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
        Selection.SetActive(false);

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
            Selection.SetActive(false);
            ShopEventManager.Items.SelectItem(this);
            toggle.group.SetAllTogglesOff(false);
        }
        else if (toggle!=null&&!toggle.isOn) 
        {
            Selection.SetActive(false);
            ShopEventManager.Items.DeselectItem(true);
          
        }
        else
        {
            Selection.SetActive(false);
        }

    }

    public void SetActiveSelection(bool active)
    {
        //Selection.SetActive(active);
    }

    public TazoItem PullTazo()
    {
        return ChipBag.GetRandomTazo();
    }
}
