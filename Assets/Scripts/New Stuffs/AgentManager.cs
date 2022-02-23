using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public List<FurnitureObject> furnitureInScene = new List<FurnitureObject>();

    void Start()
    {
        furnitureInScene.AddRange(GameObject.FindObjectsOfType<FurnitureObject>());
    }
}
