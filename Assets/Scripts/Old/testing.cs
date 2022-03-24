using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class testing : MonoBehaviour
{
    public List<FurnitureObject> test_list;

    List<FurnitureObject> GetAllFurniture()
    {
        List<FurnitureObject> furnitureInScene = new List<FurnitureObject>();
        foreach (FurnitureObject fobj in Resources.FindObjectsOfTypeAll(typeof(FurnitureObject)) as FurnitureObject[])
        {
            furnitureInScene.Add(fobj);
        }
        return furnitureInScene;
    }

    void Start()
    {
        test_list = GetAllFurniture();
    }
}