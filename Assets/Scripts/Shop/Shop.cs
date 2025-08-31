using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Space(15)]
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
    [SerializeField]
    ToggleGroup TGroup;

    [Space(15)]

    [Header("Bag Open")]
    [SerializeField]
    VideoPlayer BagOpenPlayer;

    [Header("Tazo Get")]
    [SerializeField]
    GameObject TazoGetParent;

    [Space(5)]
    [field: Header("Tazo Display")]
    [SerializeField]
    TazoDisplay TazoDisplayHolder;
    [SerializeField]
    GameObject TazoNameDisplay;
    [SerializeField]
    GameObject TazoDescriptionDisplay;
    [SerializeField]
    Button AcceptButton;


    [field:Header("Conffeti Particles")]
    [SerializeField]
    ParticleSystem PuffParticle;
    [SerializeField]
    ParticleSystem PotatoParticle;
    [SerializeField]
    ParticleSystem TortillaParticle;



    [Space(10)]
    [Header("Player Data")]
    [SerializeField]
    PlayerData PlayerData;

    [Space(10)]
   



    ShopItem SelectedItem;

    TextMeshProUGUI Name;
    TextMeshProUGUI Description;

    TextMeshProUGUI TazoName;
    TextMeshProUGUI TazoDescription;

    TazoItem PulledTazo;


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
        //Shop UI Set
        Name =ItemNameDisplay.GetComponent<TextMeshProUGUI>();
        Description=ItemDescriptionDisplay.GetComponent<TextMeshProUGUI>();
        //Tazo UI Set
        TazoName = TazoNameDisplay.GetComponent<TextMeshProUGUI>();
        TazoDescription = TazoDescriptionDisplay.GetComponent<TextMeshProUGUI>();
        
        BagOpenPlayer.loopPointReached += EndReached;

        ToggleHideTazoDisplay(true);
        BuyButton.enabled = false;
        ItemNameDisplay.SetActive(false);
        ItemDescriptionDisplay.SetActive(false);

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
        BuyButton.enabled = false;

    }


    public void SelectItem(ShopItem shopItem)
    {
        ItemNameDisplay.SetActive(true);
        ItemDescriptionDisplay.SetActive(true);

        SelectedItem = shopItem;

        Name.SetText(shopItem.GetItemName());
        Description.SetText(shopItem.GetItemDescrption());

        Name.enabled = true;
        Description.enabled = true;
        BuyButton.enabled = true;
    }

    public void GetTazoFromBag()
    {
        PulledTazo=SelectedItem.PullTazo();

        if (PulledTazo != null) 
        {

            TazoName.SetText(PulledTazo.GetName());
            TazoDescription.SetText(PulledTazo.GetDescription());
            var top= PulledTazo.GetTopMaterial();
            var bottom= PulledTazo.GetBottomMaterial();
            TazoDisplayHolder.SetMaterials(top, bottom);
        }

    }

    public void BuyItem()
    {
        //PlayerData.AddToInventory(SelectedItem.);
       
        BagOpenPlayer.Play();
        GetTazoFromBag();
        ToggleHideShopDisplay(true);
        
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.Stop();
        ToggleHideTazoDisplay(false);

        //ToggleShopDisplay(false);
    }
    // True Hides the Display False Shows it
    public void ToggleHideShopDisplay(bool toggle)
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

    // True Hides the Display False Shows it
    public void ToggleHideTazoDisplay(bool toggle)
    {
        if (toggle)
        {
            TazoGetParent.transform.parent = BackStage.transform;
            TazoDisplayHolder.Hide(true);
           

        }
        else
        {
            TazoGetParent.transform.parent = this.transform;
            TazoDisplayHolder.Hide(false);
            TazoDisplayHolder.Flip();
            switch (SelectedItem.ChipBag.GetChipType())
            {
                case ChipType.Potato:
                    PotatoParticle.Play();
                    break;
                case ChipType.Tortilla:
                    TortillaParticle.Play();
                    break;
                case ChipType.Puff:
                    PuffParticle.Play();
                    break;
            }

        }

    }

    public void AcceptTazo()
    {
        PlayerData.AddToInventory(PulledTazo);
        SceneManager.LoadScene(1);
    }


}
