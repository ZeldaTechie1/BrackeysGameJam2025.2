using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    private List<LevelSelectionBox> LVLSBoxes = new List<LevelSelectionBox>();

    [SerializeField]
    int EnemiesDefeated = 0;

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
        if (LVLSBoxes.Count > 0 && LVLSBoxes.Count > EnemiesDefeated)
        {
            foreach (LevelSelectionBox LvlsBox in LVLSBoxes)
            {
                if (LvlsBox.LevelID >= EnemiesDefeated)
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
        if(LVLSBoxes.Count > 0&&LVLSBoxes.Count>EnemiesDefeated) 
        {
            foreach(LevelSelectionBox LvlsBox in LVLSBoxes)
            {
                if (LvlsBox.LevelID == EnemiesDefeated) 
                {

                    Debug.Log("Load Level:" +LvlsBox.LevelName);
                    //SceneManager.LoadScene(LVLSBoxes[EnemiesDefeated].LevelID);
                }
            }

        }
     
    }
}
