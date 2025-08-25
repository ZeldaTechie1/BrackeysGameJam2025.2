using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelSelectionBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Image CharacterPortrait;
    private TextMeshProUGUI CharacterNameText;
    [SerializeField]
    private  Sprite DefaultPortrait;
    [SerializeField]
    private Sprite LossPortrait;
    [SerializeField]
    private string CharacterTitle;

    [SerializeField]
    public int LevelID;

    [SerializeField]
    public string LevelName;





    void Awake()
    {
        System.Collections.Generic.IEnumerable<Image> SceneObjects = this.gameObject.GetComponentsInChildren<Image>().Where(go => go.gameObject != this.gameObject);

        foreach (Image image in SceneObjects)
        {
            if(image!=this.gameObject.GetComponent<Image>())
            {
                CharacterPortrait = image;
            }
        }


       
        CharacterNameText=GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log("BEEP");
        
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDefaultPortrait()
    {
        UpdateCharacter(DefaultPortrait, CharacterTitle);
    }
    public void SetLossPortrait()
    {
       
        UpdateCharacter(LossPortrait, "<s>" + CharacterTitle+ "<s>");
    }

    public void UpdateCharacter(Sprite portrait, string CharacterTitle)
    {
        CharacterPortrait.sprite=portrait;
        CharacterNameText.text=CharacterTitle;
    }

}
