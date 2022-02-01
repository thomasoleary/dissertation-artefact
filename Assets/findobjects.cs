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

    public AgentTest[] agentTests;

    // Start is called before the first frame update
    void Start()
    {

        agents = GameObject.FindGameObjectsWithTag("agent");

        agentTests = new AgentTest[agents.Length];

        int i = 0;
        foreach(GameObject agent in agents)
        {
            agentTests[i] = agent.GetComponent<AgentTest>() as AgentTest;
            i++;
        }


    }
}
