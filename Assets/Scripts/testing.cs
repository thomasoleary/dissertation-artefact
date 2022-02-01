using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class testing : MonoBehaviour
{
    public List<FurnitureObject> test_list;

    List<GameObject> GetAllObjectsOnlyInScene()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                objectsInScene.Add(go);
        }

        return objectsInScene;
    }

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