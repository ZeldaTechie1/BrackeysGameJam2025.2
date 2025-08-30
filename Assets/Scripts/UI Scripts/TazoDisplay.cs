using Unity.VisualScripting;
using UnityEngine;

public class TazoDisplay : MonoBehaviour
{
    [Header("Tazo Mesh Renderers")]
    [SerializeField]
    MeshRenderer Core;
    [SerializeField]
    MeshRenderer Top;
    [SerializeField]
    MeshRenderer Bottom;
    [SerializeField]
    MeshRenderer Border;

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
            Top.materials[0]=TazoTop;
        }
        if(TazoBottom != null)
        {
            Bottom.materials[0]=TazoBottom;
        }
        
    }

    public void SetMaterials(Material top, Material bottom)
    {
        TazoTop = top;
        TazoBottom = bottom;
        Top.material = TazoTop;
        Bottom.material = TazoBottom;

        Top.UpdateGIMaterials();
        Bottom.UpdateGIMaterials();


       
    }

    public void Flip()
    {
        if(Anim!= null)
        {
            Anim.SetTrigger("DisplayFlip");
        }
    }

    public void Hide(bool hide)
    {
        if (hide)
        {
            Core .enabled = false;
            Top.enabled = false;
            Bottom.enabled = false;
            Border.enabled = false;

        }
        else 
        {
            Core.enabled = true;
            Top.enabled = true;
            Bottom.enabled = true;
            Border.enabled = true;
        
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
