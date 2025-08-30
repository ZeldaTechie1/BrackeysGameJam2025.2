using UnityEngine;

[CreateAssetMenu(fileName = "TazoItem", menuName = "Scriptable Objects/TazoItem")]
public class TazoItem : ScriptableObject
{
    [Header("Tazo Info")]
    [SerializeField]
    private string Name;
    [SerializeField]
    private string Description;
    [SerializeField]
    private string EffectName;
    [SerializeField]
    //The Tazo that will atcually be in play
    private GameObject InGameTazo;

    [Header("Tazo Materials")]
    [SerializeField]
    Material TazoTop;
    [SerializeField]
    Material TazoBottom;

    private void Awake()
    {
        
    }
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
    public GameObject GetTazo()
    {
        return InGameTazo;
    }

    public void PullMats()
    {
        if (InGameTazo != null)
        {
            GameObject Top = InGameTazo.transform.Find("Top").gameObject;
            GameObject Bottom = InGameTazo.transform.Find("Bottom").gameObject;

            try
            {
                TazoTop = Top.GetComponent<Material>();
                TazoBottom = Bottom.GetComponent<Material>();
            }
            catch
            {

            }

        }
    }
    public Material GetTopMaterial()
    {

       if(TazoTop == null)
       {
            PullMats();
       }

        return TazoTop;
    }
    public Material GetBottomMaterial()
    {
        if (TazoBottom == null)
        {
            PullMats();
        }
        return TazoBottom;
    }

}
