using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Shop : MonoBehaviour
{
    // Shop Options
    [Header("Shop Settings")]
    [SerializeField]
    int NumberForSale = 3;
    [SerializeField]
    List<ChipBag> ShopInventory = new List<ChipBag>();
    [SerializeField]
    GameObject BaseItem;

    List<GameObject> ItemsForSale = new List<GameObject>();
    //Shop Displays
    [Space(10)]
    [Header("Shop Displays")]
    [SerializeField]
    GameObject ShopItemsDisplay;
    [SerializeField]
    GameObject ItemNameDisplay;
    [SerializeField]
    GameObject ItemDescriptionDisplay;
    [SerializeField]
    Button BuyButton;
    [SerializeField]
    GameObject BackStage;


    [Header("Bag Open")]
    [SerializeField]
    VideoPlayer BagOpenPlayer;

    [SerializeField]
    ToggleGroup TGroup;
    [Space(10)]
    [Header("Player Data")]
    [SerializeField]
    PlayerData PlayerData;

    [Space(10)]
   



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
        BagOpenPlayer.loopPointReached += EndReached;

    }


    // Update is called once per frame
    void Update()
    {
        if(SelectedItem==null)
        {
            BuyButton.enabled= false;
        }
        else
        {
            BuyButton.enabled = true;
        }
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
            GameObject item = Instantiate(BaseItem, ShopItemsDisplay.transform);
            
            ShopItem shopItem = item.GetComponent<ShopItem>();

            if(shopItem != null)
            {

                shopItem.ChipBag = ShopInventory[X];
                shopItem.SetUp();
                ItemsForSale.Add(item);
            }
        
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

    public void BuyItem()
    {
        //PlayerData.AddToInventory(SelectedItem.);
        BagOpenPlayer.Play();
        ToggleShopDisplay(true);


    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.Stop();
        ToggleShopDisplay(false);
    }

    public void ToggleShopDisplay(bool toggle)
    {
        if(toggle)
        {
            ItemNameDisplay.transform.parent = BackStage.transform;
            ShopItemsDisplay.transform.parent = BackStage.transform;
        }
        else
        {
            ItemNameDisplay.transform.parent = this.transform;
            ShopItemsDisplay.transform.parent = this.transform;
        }

    }

}
