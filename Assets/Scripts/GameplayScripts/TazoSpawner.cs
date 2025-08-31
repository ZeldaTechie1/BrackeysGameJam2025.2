using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TazoSpawner : MonoBehaviour
{
    [SerializeField] float tazoOffset;
    [SerializeField]List<TazoItem> playerTazos;
    [SerializeField] List<TazoItem> opponentTazos;
    [SerializeField] GameObject TazosContainer;

    [SerializeField] List<TazoItem> petTazos;
    [SerializeField] List<TazoItem> horrorTazos;
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject displayItemPrefab;
    [SerializeField] GameObject playerTazosContainer;
    [SerializeField] GameObject opponentTazosContainer;

    int tazosSpawned;

    public void Setup()
    {
        playerTazos.Clear();
        opponentTazos.Clear();
        GetPlayerTazos();
        ChooseOpponentTazos();
        DisplayTazos();
        tazosSpawned = 0;
        //somehow get the player tazos and the opponent tazos;
        for(int count = 0; count < playerTazos.Count; count++)
        {
            Vector3 spawnPosition = new Vector3(0, tazoOffset, 0);
            spawnPosition.y = tazoOffset * tazosSpawned++;
            Debug.Log(spawnPosition);
            GameObject playerTazo = Instantiate(playerTazos[count].GetTazo(), spawnPosition, Quaternion.identity, TazosContainer.transform);
            spawnPosition.y = tazoOffset * tazosSpawned++;
            Debug.Log(spawnPosition);
            GameObject opponentTazo = Instantiate(opponentTazos[count].GetTazo(), spawnPosition, Quaternion.identity, TazosContainer.transform);
        }

    }

    private void DisplayTazos()
    {
        foreach(TazoItem tazoItem in playerTazos)
        {
            GameObject newItem = Instantiate(displayItemPrefab, playerTazosContainer.transform);
            CollectionItem item = newItem.GetComponent<CollectionItem>();
            item.TazoItem = tazoItem;
            item.SetUp();
        }
        foreach (TazoItem tazoItem in opponentTazos)
        {
            GameObject newItem = Instantiate(displayItemPrefab, opponentTazosContainer.transform);
            CollectionItem item = newItem.GetComponent<CollectionItem>();
            item.TazoItem = tazoItem;
            item.SetUp();
        }
    }

    private void GetPlayerTazos()
    {
        List<TazoItem> tazos = playerData.GetHand();
        foreach(TazoItem item in tazos)
        {
            playerTazos.Add(item);
        }
    }

    void ChooseOpponentTazos()
    {
        List<TazoItem> seriesTazos = new();
        int randRange = Random.Range(0, 2);
        if(randRange == 0)
        {
            foreach(TazoItem item in horrorTazos)
            {
                seriesTazos.Add(item);
            }
        }
        else
        {
            foreach (TazoItem item in petTazos)
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
