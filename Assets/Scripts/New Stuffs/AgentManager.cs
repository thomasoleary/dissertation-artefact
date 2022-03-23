using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public List<FurnitureObject> furnitureInScene = new List<FurnitureObject>();

    void Start()
    {
        furnitureInScene.AddRange(GameObject.FindObjectsOfType<FurnitureObject>());

        for (int i = 0; i < furnitureInScene.Count; i++)
        {
            FurnitureObject temp = furnitureInScene[i];
            int randomIndex = Random.Range(i, furnitureInScene.Count);
            furnitureInScene[i] = furnitureInScene[randomIndex];
            furnitureInScene[randomIndex] = temp;
        }
    }
}


static class Utilities
{
    public static void ShuffleArray<T> (this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}