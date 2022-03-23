using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public List<FurnitureObject> furnitureInScene = new List<FurnitureObject>();

    void Start()
    {
        furnitureInScene.AddRange(GameObject.FindObjectsOfType<FurnitureObject>());

        Shuffle();
    }

    public void Shuffle()
    {
        ShuffleList(furnitureInScene);
    }
    private static void ShuffleList<T> (List<T> listToShuffle)
    {
        for (int i = 0; i < listToShuffle.Count; i++)
        {
            T temp = listToShuffle[i];
            int randomIndex = Random.Range(i, listToShuffle.Count);
            listToShuffle[i] = listToShuffle[randomIndex];
            listToShuffle[randomIndex] = temp;
        }
    }
}



