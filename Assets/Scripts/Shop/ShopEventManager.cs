using System;
using UnityEngine;
using UnityEngine.Events;

public static class ShopEventManager
{
    public static readonly ItemEvent Items= new ItemEvent();
    
    public class ItemEvent
    {
        public UnityAction<ShopItem> SelectItem;
        public UnityAction<bool> DeselectItem;
    }

    
}
