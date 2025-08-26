using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Image Portrait;
    [SerializeField]
    private TextMeshProUGUI ItemNameText;
    [SerializeField]
    private string ItemDescription;

    public GameObject Item;


    private string ItemName;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUp();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUp()
    {
        try
        {
            ItemName = ItemNameText.text; 
            
        }
        catch(Exception ex) 
        {
            Debug.LogException(ex);

        }

    }

}
