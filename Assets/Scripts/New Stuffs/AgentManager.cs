using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public RootFurniture[] furnitureInScene;

    void Start()
    {
        furnitureInScene = GameObject.FindObjectsOfType<RootFurniture>();
    }
}
