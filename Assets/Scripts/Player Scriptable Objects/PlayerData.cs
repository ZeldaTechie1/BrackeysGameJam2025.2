using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    List<TazoItem> TazoInventory = new List<TazoItem>();
    [SerializeField]
    List<TazoItem> TazoHand = new List<TazoItem>();


    [SerializeField]
    int MaxHandSize;

    [SerializeField]
    public int HandSize;


    [SerializeField]
    GameObject ChipEffect;

    [SerializeField]
    int NumberOfWins;

    [SerializeField]
    int NumberOfLosses;


    //WIN LOSS

    public void IncrementWin()
    {
        NumberOfWins++;
    }
    public int GetWins()
    {
        return NumberOfWins;
    }

    public void IncrementLoss()
    {
        NumberOfLosses++;
    }
    public int GetLosses()
    {
        return NumberOfLosses;
    }

    //Inventory
    public List<TazoItem> GetInventory()
    {
        return TazoInventory;
    }
    public void AddToInventory(TazoItem pog)
    {
        TazoInventory.Add(pog);
    }
    public void RemoveFromInventory(TazoItem pog)
    {
        TazoInventory.Remove(pog);
    }
    public void ClearInventory()
    {
        TazoInventory.Clear();
    }

    //Hand 
    public List<TazoItem> GetHand()
    {
        return TazoHand;
    }
    public void AddToHand(TazoItem pog)
    {
        TazoHand.Add(pog);
    }
    public void MassAddToHand(List<TazoItem> pog)
    {
        TazoHand.Clear();
        TazoHand = pog;
    }

    public void RemoveFromHand(TazoItem pog)
    {
        TazoHand.Remove(pog);
    }
    public void ClearHand()
    {
        TazoHand.Clear();    
    }

    //Chips
    public void SetChipEffect(GameObject chipEffect)
    {
        ChipEffect = chipEffect;
    }
    public GameObject GetChipEffect()
    {
         return ChipEffect;
    }



    public void EndRun()
    {
        ClearInventory();
        ClearHand();
        NumberOfWins = 0;
        NumberOfLosses= 0;
        HandSize = MaxHandSize;
        ChipEffect= null;
    }


    
}
