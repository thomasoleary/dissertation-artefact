using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public FurnitureAgent[] furnitureInScene;

    void Start()
    {
        furnitureInScene = GameObject.FindObjectsOfType<FurnitureAgent>();
    }
}
