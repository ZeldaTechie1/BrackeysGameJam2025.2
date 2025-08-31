using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TazoSpawner : MonoBehaviour
{
    [SerializeField] float tazoOffset;
    [SerializeField]List<GameObject> playerTazos;
    [SerializeField] List<GameObject> opponentTazos;
    [SerializeField] GameObject TazosContainer;

    [SerializeField] List<GameObject> petTazos;
    [SerializeField] List<GameObject> horrorTazos;

    int tazosSpawned;

    public void Setup()
    {
        GetPlayerTazos();
        ChooseOpponentTazos();
        tazosSpawned = 0;
        //somehow get the player tazos and the opponent tazos;
        for(int count = 0; count < playerTazos.Count; count++)
        {
            Vector3 spawnPosition = new Vector3(0, tazoOffset, 0);
            spawnPosition.y = tazoOffset * tazosSpawned++;
            Debug.Log(spawnPosition);
            GameObject playerTazo = Instantiate(playerTazos[count], spawnPosition, Quaternion.identity, TazosContainer.transform);
            spawnPosition.y = tazoOffset * tazosSpawned++;
            Debug.Log(spawnPosition);
            GameObject opponentTazo = Instantiate(opponentTazos[count], spawnPosition, Quaternion.identity, TazosContainer.transform);
        }

    }

    private void GetPlayerTazos()
    {
            
    }

    void ChooseOpponentTazos()
    {
        List<GameObject> seriesTazos = new();
        int randRange = Random.Range(0, 2);
        if(randRange == 0)
        {
            foreach(GameObject item in horrorTazos)
            {
                seriesTazos.Add(item);
            }
        }
        else
        {
            foreach (GameObject item in petTazos)
            {
                seriesTazos.Add(item);
            }
        }

        for(int i = 0; i < 5; i++)
        {
            int randomPog = Random.Range(0,seriesTazos.Count);
            Debug.Log(randomPog);
            opponentTazos.Add(seriesTazos[randomPog]);
            seriesTazos.RemoveAt(randomPog);
        }
    }

}
