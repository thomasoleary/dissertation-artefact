using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    File: AgentManager.cs
    Author: Thomas (Tom) O'Leary

    Summary:
        This class handles finding all Agents within the scene.
 */

public class AgentManager : MonoBehaviour
{
    public List<FurnitureObject> furnitureInScene = new List<FurnitureObject>();

    void Start()
    {
        // Finds all agents in the scene
        furnitureInScene.AddRange(GameObject.FindObjectsOfType<FurnitureObject>());

        Shuffle();
    }

    ///<summary>
    /// Public method that is called by an Agent if it needs to move position 
    ///</summary>
    public void Shuffle()
    {
        ShuffleList(furnitureInScene);
    }

    ///<summary>
    /// Function that uses the Fisher-Yates shuffle algorithm
    ///</summary>
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



