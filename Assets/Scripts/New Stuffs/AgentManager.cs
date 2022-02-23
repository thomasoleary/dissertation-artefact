using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{

    public List<RootFurniture> furnitureInScene = new List<RootFurniture>();

    void Start()
    {
        furnitureInScene.AddRange(GameObject.FindObjectsOfType<RootFurniture>());
    }
}
