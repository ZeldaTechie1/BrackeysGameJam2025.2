using UnityEngine;

public class WorldMap : MonoBehaviour
{
    [SerializeField]
    GameObject Map;
    [SerializeField]
    GameObject Collection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        CollectionEventManager.CollectionItems.SwapUI += SwapUI;

    }
    private void OnDisable()
    {
        CollectionEventManager.CollectionItems.SwapUI -= SwapUI;
    }

    void Start()
    {
        Map.SetActive(true);
        Collection.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapUI(bool X)
    {
        Map.SetActive(!Map.activeInHierarchy);
        Collection.SetActive(!Collection.activeInHierarchy);
    }


}
