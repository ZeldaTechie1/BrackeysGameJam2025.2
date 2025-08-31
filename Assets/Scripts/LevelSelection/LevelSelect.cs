using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    private List<LevelSelectionBox> LVLSBoxes = new List<LevelSelectionBox>();

    [SerializeField]
    PlayerData Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    void Reset()
    {


    }
    private void Start()
    {
        SceneSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwapUI()
    {
        CollectionEventManager.CollectionItems.SwapUI(true);
    }


    public void SceneSetUp()
    {
        System.Collections.Generic.IEnumerable<LevelSelectionBox> SceneObjects = this.gameObject.GetComponentsInChildren<LevelSelectionBox>().Where(go => go.gameObject != this.gameObject);

        foreach (var lvlsBox in SceneObjects)
        {
            LVLSBoxes.Add(lvlsBox);
        }
        LevelBoxSetUp();


        Debug.Log("LVLSBOXES:" + LVLSBoxes.Count());
    }
    public void LevelBoxSetUp()
    {
        if (LVLSBoxes.Count > 0 && LVLSBoxes.Count > Player.GetWins())
        {
            foreach (LevelSelectionBox LvlsBox in LVLSBoxes)
            {
                if (LvlsBox.LevelID >= Player.GetWins())
                {
                    LvlsBox.SetDefaultPortrait();
                }
                else
                {
                    LvlsBox.SetLossPortrait();
                }

            }
                
        }
    }


    public void LoadNextLevel()
    {
        if(Player.GetHand().Count < Player.MaxHandSize)
        {
            CollectionEventManager.CollectionItems.SwapUI(true);
            return;
        }
        if(LVLSBoxes.Count > 0&&LVLSBoxes.Count> Player.GetWins()) 
        {
            foreach(LevelSelectionBox LvlsBox in LVLSBoxes)
            {
                if (LvlsBox.LevelID == Player.GetWins()) 
                {

                    Debug.Log("Load Level:" +LvlsBox.LevelName);
                    SceneManager.LoadScene(2);
                }
            }

        }
     
    }
}
