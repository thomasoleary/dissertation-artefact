using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UnitTesting
    {
        private AgentManager agentManager;
        private GameObject[] agents;

        private int agentInitCount;
        private int agentInitAmount;

        [SetUp]
        public void SetUp()
        {
            agentManager = GameObject.Find("Room").GetComponent<AgentManager>();
            agents = GameObject.FindGameObjectsWithTag("agent");

            agentInitAmount = agents.GetLength(0);
        }


        // A Test behaves as an ordinary method
        [Test]
        public void AgentManagerExists()
        {
            Assert.IsNotNull(agentManager);
            Debug.Log(agentManager);
        }

        [Test]
        public void AgentsExists()
        {
            Assert.IsNotNull(agents);
            Debug.Log(agents);
        }

        [Test]
        public void HasAgentInitialised()
        {
            foreach (var agent in agents)
            {
                if (agent.GetComponent<Agent>().agentSO.isInitialised)
                {
                    agentInitCount++;
                }
            }

            Assert.IsNotNull(agentInitCount == agentInitAmount);
        }
    }

}