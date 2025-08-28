using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ChipBag", menuName = "Scriptable Objects/ChipBag")]
public class ChipBag : ScriptableObject
{
    [Header("Bag Info")]
    [SerializeField]
    private string Name;
    [SerializeField]
    private string Description;
    [SerializeField]
    private string EffectName;

    [SerializeField]
    private ChipType ChipType;

    [SerializeField]
    private Sprite BagPortrait;

    [SerializeField]
    List<TazoItem> PossibleTazos = new List<TazoItem>();


    public string GetName()
    {
        return Name;
    }

    public string GetDescription()
    {
        return Description;
    }
    public string GetEffectName()
    {
        return EffectName;
    }
    public Sprite GetSprite()
    {
        return BagPortrait;
    }

    public ChipType GetChipType()
    {
        return ChipType;
    }

    public TazoItem GetRandomTazo()
    {

        TazoItem Tazo =null;
        if (PossibleTazos.Count > 0)
        {
          
            Tazo=PossibleTazos[Random.Range(0, PossibleTazos.Count)];

        }

        return Tazo;
    }


    
    

}

public enum ChipType
{
    Potato,
    Tortilla,
    Puff,
}
