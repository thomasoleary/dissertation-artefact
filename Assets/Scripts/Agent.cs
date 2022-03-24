using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    #region Variables
    public string furnitureName;

    public FurnitureObject agentSO;

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
        AgentMethod(agent.state);
    }

    void AgentMethod(AgentState state)
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
                //Debug.Log(agent.furnitureName + " is Resting");
                break;

            case AgentState.SLEEP:
                //Debug.Log(agent.furnitureName + " is Sleeping");
                break;
        }
    }

    bool Search()
    {
        // TO-DO
        // Agent needs to be deleted if a potential parent cannot be found

        //Debug.Log(agent.furnitureName + " is Searching");

        for (int i = 0; i < agent.potentialParents.Count(); i++)
        {
            foreach(FurnitureObject fObject in aManager.furnitureInScene)
            {
                if (agent.potentialParents[i].parentType == fObject.typeOfFurniture && fObject.CanBeParent)
                {
                    if (fObject.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren >= 1)
                        if (fObject.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren < fObject.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren)
                        {
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

    void Arrange()
    {
        this.gameObject.transform.position = agent.currentParentSide.childPlacement.position;
        this.gameObject.transform.rotation = agent.currentParentSide.childPlacement.rotation;
    }

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
