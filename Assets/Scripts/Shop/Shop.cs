using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    List<ShopItem> ShopInventory = new List<ShopItem>();
    List<ShopItem> ItemsForSale= new List<ShopItem>();

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
