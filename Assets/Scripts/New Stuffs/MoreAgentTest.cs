using System.Collections;
using System.Linq;
using UnityEngine;

public class MoreAgentTest : MonoBehaviour
{
    public string furnitureName;

    public RootFurniture agentSO;

    public FurnitureAgent agent;

    [SerializeField] private AgentManager aManager;

    void Awake()
    {
        agent = agentSO.GetInstance() as FurnitureAgent;
        agent.Init(furnitureName);

    }

    void Update()
    {
        AgentMethod(agent.state);
    }

    void AgentMethod(AgentState state)
    {
        switch (state)
        {
            case AgentState.SEARCH:
                if (Search())
                    agent.state = AgentState.ARRANGE;
                /* 
                All agents start in this state

                agent searches for possible parents by examining other agents in the room

                if a possible parent is found
                    agent examines it's semantics of this potential parent
                        looking for a suitable side with enough space and places left

                if the possible parent is suitable
                    agent state changes to ARRANGE

                else
                    it tries to find another parent

                if no parent can be found, the room is full and the agent is deleted
                
                 */
                break;
            
            case AgentState.ARRANGE:
                agent.state = AgentState.REST;
                break;
            
            case AgentState.REST:
                Debug.Log(agent.furnitureName + " is Resting");
                break;

            case AgentState.SLEEP:
                Debug.Log(agent.furnitureName + " is Sleeping");
                break;
        }
    }

    bool Search()
    {
        Debug.Log(agent.furnitureName + " is Searching");

        for (int i = 0; i < agent.potentialParents.Count(); i++)
        {
            foreach(FurnitureAgent fAgent in aManager.furnitureInScene)
            {
                if (agent.potentialParents[i].parentType == fAgent.typeOfFurniture && fAgent.CanBeParent)
                {
                    if (fAgent.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren >= 1)
                        if (fAgent.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren < fAgent.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren)
                        {
                            fAgent.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren++;
                            agent.currentParent = fAgent;
                            agent.hasFoundParent = true;
                            Debug.Log(agent.furnitureName + " has found parent: " + fAgent.furnitureName + " " + fAgent.sides[(int)agent.potentialParents[i].sideOnParent].axis);
                            return true;
                        }
                }
            }
        }
        return false;
    }

}
