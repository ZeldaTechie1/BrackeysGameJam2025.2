using Unity.VisualScripting;
using UnityEngine;

public class TazoDisplay : MonoBehaviour
{
    [Header("Tazo Mesh Renderers")]
    [SerializeField]
    MeshRenderer Top;
    [SerializeField]
    MeshRenderer Bottom;

    [Header("Tazo Materials")]
    [SerializeField]
    Material TazoTop;
    [SerializeField]
    Material TazoBottom;

    [Header("Tazo Animation")]
    [SerializeField]
    Animator Anim;

    private void Awake()
    {
        if (TazoTop != null)
        {
            Top.material = TazoTop;
        }
        if (TazoBottom != null)
        {
            Bottom.material = TazoBottom;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(TazoTop != null)
        {
            Top.material=TazoTop;
        }
        if(TazoBottom != null)
        {
            Bottom.material=TazoBottom;
        }
        
    }

    public void SetMaterials(Material top, Material bottom)
    {
        Top.material = TazoTop;
        Bottom.material = TazoBottom;
    }

    public void Flip()
    {
        if(Anim!= null)
        {
            Anim.SetTrigger("DisplayFlip");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
