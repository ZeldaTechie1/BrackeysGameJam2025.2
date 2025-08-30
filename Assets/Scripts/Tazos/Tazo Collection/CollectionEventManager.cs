using UnityEngine;
using UnityEngine.Events;

public static class CollectionEventManager
{
    public static readonly CollectionItemEvent CollectionItems = new CollectionItemEvent();

    public class CollectionItemEvent
    {
        public UnityAction<CollectionItem> SelectItem;
        public UnityAction<bool> DeselectItem;

        public UnityAction<bool> SwapUI;
    }

}
