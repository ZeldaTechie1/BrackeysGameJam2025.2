using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Collection : MonoBehaviour
{
    // Shop Options
    [Header("Collection SetUp")]
    [SerializeField]
    List<CollectionItem> CollectionInventory = new List<CollectionItem>();
    Dictionary<CollectionItem, GameObject> HandInventory= new Dictionary<CollectionItem, GameObject>();
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
    TazoDisplay TazoDisplayHolder;
    [SerializeField]
    GameObject TazoInfoDisplay;
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
    [Header("Hand Display")]
    [SerializeField]
    GameObject HandDisplay;

    [Header("Warning Message Display")]
    [SerializeField]
    GameObject WarningDisplay;
    [SerializeField]
    GameObject WarningText;
    TextMeshProUGUI Warning;

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
        Warning=WarningText.GetComponent<TextMeshProUGUI>();    
        //Tazo UI Set


        ToggleHideAddButton(true);
        ToggleHideRemoveButton(true);
        ToggleHideWarningText(true);
        ToggleHideTazoText(true);

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
                HandInventory.Add(CollectionItem,item);
                

            }
           
        }
        

    }

    public void UpdatePlayerHand()
    {
        ClearPlayerHand();

    }

    private void ClearPlayerHand()
    {
       
    }

    public void DeselectItem(bool holder)
    {
        SelectedItem = null;
        Name.enabled = false;
        Description.enabled = false;
        HandAdd.enabled = false;
        HandRemove.enabled=false;
    }

    public void ToggleHideTazoText(bool hide)
    {
        if (hide)
        {
            TazoNameDisplay.transform.parent = BackStage.transform;
            



        }
        else
        {
            TazoNameDisplay.transform.parent = TazoInfoDisplay.transform;
         

        }
    }

    public void SelectItem(CollectionItem Item)
    {
        ToggleHideTazoText(false);
        SelectedItem = Item;

        Name.SetText(Item.GetItemName());
        Description.SetText(Item.GetItemDescrption());
        Debug.Log(Item.GetItemDescrption());
        var top = SelectedItem.TazoItem.GetTopMaterial();
        var bottom = SelectedItem.TazoItem.GetBottomMaterial();
        TazoDisplayHolder.SetMaterials(top, bottom);
        Name.enabled = true;
        Description.enabled = true;
        if(TazoInHand(SelectedItem.TazoItem))
        {
            ToggleHideAddButton(true);
            ToggleHideRemoveButton(false);
        }
        else
        {
            ToggleHideAddButton(false);
            ToggleHideRemoveButton(true);
        }
       

    }

    public bool TazoInHand(TazoItem T)
    {
        foreach (TazoItem tazo in PlayerHand)
        {
            if(tazo==T)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveSelected()
    {
       if(SelectedItem!=null)
       {
            if (TazoInHand(SelectedItem.TazoItem))
            {
                foreach(KeyValuePair<CollectionItem,GameObject> entry in HandInventory)
                {
                    if (entry.Key.TazoItem == SelectedItem.TazoItem)
                    {
                        GameObject GO = entry.Value;
                        Destroy(GO);
                        HandInventory.Remove(entry.Key);
                        break;
                    }
                }
                PlayerHand.Remove(SelectedItem.TazoItem);
                ToggleHideAddButton(false);
                ToggleHideRemoveButton(true);
            }
       }
    }
    public void AddSelected()
    {
        TazoItem tazo = SelectedItem.TazoItem;
        if(!TazoInHand(tazo))
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
                HandInventory.Add(CollectionItem, item);
            }
            PlayerHand.Add(SelectedItem.TazoItem);
            ToggleHideAddButton(true);
            ToggleHideRemoveButton(false);

        }
        else
        {
            Debug.Log("Cant Add a duplicate");
        }
    }


    public void ToggleHideAddButton(bool toggle)
    {
        HandAdd.enabled = !toggle;
        HandAdd.transform.gameObject.SetActive(!toggle);

    }
    public void ToggleHideRemoveButton(bool toggle)
    {
        HandRemove.enabled = !toggle;
        HandRemove.transform.gameObject.SetActive(!toggle);
    }

    public void ToggleHideWarningText(bool toggle)
    {
        WarningDisplay.SetActive(!toggle);
    }

    public void ExitButton()
    {
        if (HandInventory.Count == PlayerData.HandSize)
        {
            PlayerData.MassAddToHand(PlayerHand);
            Debug.Log("Wow!");
            CollectionEventManager.CollectionItems.SwapUI(true);

        }
        else
        {
            DisplayHandSizeWarning();
        }
    }

    private void DisplayHandSizeWarning()
    {

        ToggleHideWarningText(false);
    }
}
