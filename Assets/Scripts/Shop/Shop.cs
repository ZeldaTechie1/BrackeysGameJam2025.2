using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    int NumberForSale = 3;
    [SerializeField]
    GameObject ShopItemsDisplay;

    [SerializeField]
    GameObject ItemNameDisplay;

    [SerializeField]
    GameObject ItemDescriptionDisplay;


    [SerializeField]
    List<GameObject> ShopInventory = new List<GameObject>();

    List<GameObject> ItemsForSale= new List<GameObject>();

    [SerializeField]
    ToggleGroup TGroup;

    ShopItem SelectedItem;

    TextMeshProUGUI Name;
    TextMeshProUGUI Description;


    private void OnEnable()
    {
       
        ShopEventManager.Items.SelectItem += SelectItem;
        ShopEventManager.Items.DeselectItem += DeselectItem;
    }
    private void OnDisable()
    {
        ShopEventManager.Items.SelectItem -= SelectItem;
        ShopEventManager.Items.DeselectItem -= DeselectItem;
    }

    void Start()
    {
        TGroup = GetComponent<ToggleGroup>();
        PopulateShop();
        Name =ItemNameDisplay.GetComponent<TextMeshProUGUI>();
        Description=ItemDescriptionDisplay.GetComponent<TextMeshProUGUI>();

    }


    // Update is called once per frame
    void Update()
    {
    }

    void PopulateShop()
    {
        RandomizeItems();

        foreach (GameObject item in ItemsForSale)
        {
            Toggle toggle = item.GetComponent<Toggle>();    
            if(toggle != null)
            {
                toggle.group = TGroup; 
            }
        }

    }
    void RandomizeItems()
    {
        for (int i = 0; i < NumberForSale; i++)
        {
            int X = Random.Range(0, ShopInventory.Count);
            GameObject item = Instantiate(ShopInventory[X], ShopItemsDisplay.transform);

            ItemsForSale.Add(item);
        }
    }

    public void DeselectItem(bool holder)
    {
        SelectedItem = null;

        Name.enabled = false;
        Description.enabled = false;
    }


    public void SelectItem(ShopItem shopItem)
    {
        SelectedItem = shopItem;

        Name.SetText(shopItem.GetItemName());
        Description.SetText(shopItem.GetItemDescrption());

        Name.enabled = true;
        Description.enabled = true;
    }

}
