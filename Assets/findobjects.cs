using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Test script to find all agents in the scene
    This is probably the worst way I could do this right now... but hey-ho
*/
public class findobjects : MonoBehaviour
{
    private GameObject[] agents;

    private AgentTest[] agentTests;

    public FurnitureObject[] furnitureInScene;

    // Start is called before the first frame update
    void Start()
    {

        agents = GameObject.FindGameObjectsWithTag("agent");

        agentTests = new AgentTest[agents.Length];

        furnitureInScene = new FurnitureObject[agentTests.Length];

        int i = 0;
        foreach(GameObject agent in agents)
        {
            agentTests[i] = agent.GetComponent<AgentTest>() as AgentTest;
            i++;
        }

        int j = 0;
        foreach(AgentTest at in agentTests)
        {
            furnitureInScene[j] = at.agent;
            j++;
        }

    }
}
