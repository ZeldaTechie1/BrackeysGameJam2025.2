using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    List<GameObject> PogInventory = new List<GameObject>();
    [SerializeField]
    List<GameObject> PogHand = new List<GameObject>();
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
    public List<GameObject> GetInventory()
    {
        return PogInventory;
    }
    public void AddToInventory(GameObject pog)
    {
        PogInventory.Add(pog);
    }
    public void RemoveFromInventory(GameObject pog)
    {
        PogInventory.Remove(pog);
    }
    public void ClearInventory()
    {
        PogInventory.Clear();
    }

    //Hand 
    public List<GameObject> GetHand()
    {
        return PogHand;
    }
    public void AddToHand(GameObject pog)
    {
        PogHand.Add(pog);
    }
    public void RemoveFromHand(GameObject pog)
    {
        PogHand.Remove(pog);
    }
    public void ClearHand()
    {
        PogHand.Clear();    
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
        ChipEffect= null;
    }


    
}
