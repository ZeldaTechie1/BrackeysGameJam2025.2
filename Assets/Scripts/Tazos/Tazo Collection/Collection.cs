using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Collection : MonoBehaviour
{
    // Shop Options
    [Header("Collection SetUp")]
    [SerializeField]
    List<CollectionItem> CollectionInventory = new List<CollectionItem>();
    [SerializeField]
    List<CollectionItem> HandInventory = new List<CollectionItem>();
    [SerializeField]
    GameObject BaseCollectionItem;
    [SerializeField]
    GameObject HandCollectionItem;


    List<TazoItem> PlayerTazos = new List<TazoItem>();
    List<TazoItem> PlayerHand = new List<TazoItem>();



    //Shop Displays
    [Space(15)]
    [Header("Selected Tazo Display")]
    [Space(5)]
    [field: Header("Tazo Display")]
    [SerializeField]
    TazoDisplay TazoDisplay;
    [SerializeField]
    GameObject TazoNameDisplay;
    [SerializeField]
    GameObject TazoDescriptionDisplay;
    [SerializeField]
    Button HandAdd;
    [SerializeField]
    Button HandRemove;
    [SerializeField]
    GameObject BackStage;
    [SerializeField]
    ToggleGroup TGroup;

    [Space(10)]

    [Header("Collection Display")]
    [SerializeField]
    GameObject CollectionDisplay;
    [Space(5)]

    [Header("Collection Display")]
    [SerializeField]
    GameObject HandDisplay;

    [Header("Player Data")]
    [SerializeField]
    PlayerData PlayerData;

    [Space(10)]




    public CollectionItem SelectedItem;

    TextMeshProUGUI Name;
    TextMeshProUGUI Description;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {

        CollectionEventManager.CollectionItems.SelectItem += SelectItem;
        CollectionEventManager.CollectionItems.DeselectItem += DeselectItem;
    }
    private void OnDisable()
    {

        CollectionEventManager.CollectionItems.SelectItem -= SelectItem;
        CollectionEventManager.CollectionItems.DeselectItem -= DeselectItem;
    }


    void Start()
    {


        TGroup = GetComponent<ToggleGroup>();
        PopulateCollection();
        //Shop UI Set
        Name = TazoNameDisplay.GetComponent<TextMeshProUGUI>();
        Description = TazoDescriptionDisplay.GetComponent<TextMeshProUGUI>();
        //Tazo UI Set

    }


    // Update is called once per frame
    void Update()
    {

    }


    void GetPlayerCollection()
    {
        PlayerTazos=PlayerData.GetInventory();
        PlayerHand=PlayerData.GetHand();
    }

    void PopulateCollection()
    {
        GetPlayerCollection();
        foreach(TazoItem tazo in PlayerTazos)
        {
            GameObject item = Instantiate(BaseCollectionItem, CollectionDisplay.transform);
            CollectionItem CollectionItem = item.GetComponent<CollectionItem>();

            Toggle toggle = item.GetComponent<Toggle>();
            if (toggle != null)
            {
                toggle.group = TGroup;
            }

            if (CollectionItem != null)
            {
                CollectionItem.TazoItem=tazo;
                CollectionItem.SetUp();
                CollectionInventory.Add(CollectionItem);
                Debug.Log("SQUEE");


            }
        }
        foreach (TazoItem tazo in PlayerHand)
        {
            GameObject item = Instantiate(HandCollectionItem, HandDisplay.transform);
            CollectionItem CollectionItem = item.GetComponent<CollectionItem>();

            Toggle toggle = item.GetComponent<Toggle>();
            if (toggle != null)
            {
                toggle.group = TGroup;
            }

            if (CollectionItem != null)
            {
                CollectionItem.TazoItem = tazo;
                CollectionItem.SetUp();
                HandInventory.Add(CollectionItem);


            }
        }


    }




    public void DeselectItem(bool holder)
    {
        SelectedItem = null;
        Name.enabled = false;
        Description.enabled = false;
        HandAdd.enabled = false;
        HandRemove.enabled=false;
    }


    public void SelectItem(CollectionItem Item)
    {
        Debug.Log("DEBUG");
        SelectedItem = Item;

        Name.SetText(Item.GetItemName());
        Description.SetText(Item.GetItemDescrption());

        Name.enabled = true;
        HandAdd.enabled = true;
        HandRemove.enabled = true;
    }

    public void ToggleHideAddButton(bool toggle)
    {

    }
    public void ToggleHideRemoveButton(bool toggle)
    {

    }
}
