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
        private Agent agent;

        [SetUp]
        public void SetUp()
        {
            agentManager = GameObject.Find("Room").GetComponent<AgentManager>();
            agent = GameObject.FindGameObjectWithTag("agent").GetComponent<Agent>();
        }


        // A Test behaves as an ordinary method
        [Test]
        public void AgentManagerExists()
        {
            Assert.IsNotNull(agentManager);
            Debug.Log(agentManager);
        }

        [Test]
        public void AgentExists()
        {
            Assert.IsNotNull(agent);
            Debug.Log(agent);
        }
    }

}