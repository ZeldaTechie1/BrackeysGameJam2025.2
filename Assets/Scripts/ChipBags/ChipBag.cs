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
    private Sprite BagPortrait;

    [SerializeField]
    List<GameObject> PossibleTazos = new List<GameObject>();


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

    public GameObject GetRandomTasso()
    {

        GameObject Tazo=null;
        if (PossibleTazos.Count > 0)
        {
            for (int i = 0; i < PossibleTazos.Count; i++)
            {

                var TazoCompoenent = PossibleTazos[i].GetComponent<Tazo>();

                if (TazoCompoenent == null)
                {
                    PossibleTazos.Remove(PossibleTazos[i]);
                }
            }

            Tazo=PossibleTazos[Random.Range(0, PossibleTazos.Count)];

        }

        return Tazo;
    }


    
    

}
