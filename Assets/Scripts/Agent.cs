using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    #region Variables
    public string furnitureName;

    public FurnitureObject agentSO;

    // This is the agent that is actually used as it is Instantiated
    public FurnitureObject agent;

    [SerializeField] private AgentManager aManager;

    #endregion Variables

    void Awake()
    {
        agent = agentSO.GetInstance();
        agent.Init(furnitureName, this.gameObject);

        FindTransforms();
    }

    void Update()
    {
        AgentBehaviour(agent.state);
    }

    void AgentBehaviour(AgentState state)
    {
        switch (state)
        {
            case AgentState.SEARCH:
                if (Search())
                    agent.state = AgentState.ARRANGE;
                break;
            
            case AgentState.ARRANGE:
                Arrange();
                agent.state = AgentState.REST;
                break;
            
            case AgentState.REST:
                // TO-DO
                // Agent needs to check for other Agent collisions
                    // If so, lose parent and search again.
                break;

            case AgentState.SLEEP:
                // This state is for Floors & Walls.
                break;
        }
    }

    ///<summary>
    /// The agent searches for a suitable parent
    ///</summary>
    bool Search()
    {
        // TO-DO
        // Agent needs to be deleted if a potential parent cannot be found

        for (int i = 0; i < agent.potentialParents.Count(); i++)
        {
            foreach(FurnitureObject fObject in aManager.furnitureInScene)
            {
                // If the object is the same type as the preffered parent & can be a parent
                if (agent.potentialParents[i].parentType == fObject.typeOfFurniture && fObject.CanBeParent)
                {
                    // If there is space for a child on this parent
                    if (fObject.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren >= 1)
                        if (fObject.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren < fObject.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren)
                        {
                            // Parent is found
                            fObject.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren++;

                            agent.currentParent = agent.potentialParents[i];
                            agent.currentParentAgent = fObject;
                            agent.currentParentSide = fObject.sides[(int)agent.potentialParents[i].sideOnParent];
                            agent.hasFoundParent = true;
                            return true;
                        }
                }
            }
        }
        return false;
    }

    ///<summary>
    /// Arranges the agents position and rotation
    ///</summary>
    void Arrange()
    {
        this.gameObject.transform.position = agent.currentParentSide.childPlacement.position;
        this.gameObject.transform.rotation = agent.currentParentSide.childPlacement.rotation;
    }

    ///<summary>
    /// Finds necessary transforms for the agent.
    /// Only certain sides of an agent will have a Child Placement
    ///</summary>
    void FindTransforms()
    {
        agent.agentPlacement = this.transform;
        foreach (Transform child in transform)
        {
            for (int i = 0; i < agent.sides.Count(); i++)
            {
                
                if (agent.sides[i].axis.ToString() == child.name)
                {
                    agent.sides[i].childPlacement = child;

                }
            }
        }
    }
}
