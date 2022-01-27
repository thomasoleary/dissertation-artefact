using System;
using System.Linq;
using UnityEngine;

public class AgentTest : MonoBehaviour
{

    public FurnitureObject agent;

    private Vector3[] vectorAxis = new Vector3[6];

    [SerializeField] private findobjects findObjects;


    // Start is called before the first frame update
    void Start()
    {
        GameObject fObj = GameObject.FindGameObjectWithTag("find");
        findObjects = fObj.GetComponent<findobjects>() as findobjects;

        InitialiseDirections();

        //OtherSearch();
    }

    // Update is called once per frame
    void Update()
    {
        TestRay();
    }

    void TestRay()
    {
        for (int i = 0; i < agent.sides.Count(); i++)
        {
            Debug.DrawRay(transform.position, (transform.rotation * GetAxis(agent.sides[i].axis)) * agent.sides[i].distance, Color.red);
            RaycastHit groundHit;
            if (Physics.Raycast(transform.position, (transform.rotation * GetAxis(agent.sides[i].axis)), out groundHit, agent.sides[i].distance, agent.sides[i].layers))
            {
                Debug.DrawRay(transform.position, (transform.rotation * GetAxis(agent.sides[i].axis)) * agent.sides[i].distance, Color.green);

                //Debug.Log(gameObject.name + " " + agent.sides[i].axis.ToString() + " is hitting: " + groundHit.collider.name);
            }
        }
    }

    // find suitable parent
    void Search()
    {
        foreach (AgentTest at in findObjects.agentTests)
        {
            for(int i = 0; i < agent.potentialParents.Count(); i++)
            {
                if (agent.potentialParents[i].parent == at.agent)
                {
                    Debug.Log(agent + " has found parent: " + at.agent);
                }
            }

        }
    }

    void OtherSearch()
    {
        for (int i = 0; i < agent.potentialParents.Count(); i++)
        {
            foreach (AgentTest at in findObjects.agentTests)
            {
                if (agent.potentialParents[i].parent == at.agent)
                {
                    Debug.Log(agent + " has found parent: " + at.agent);
                    Debug.Log((int)agent.potentialParents[i].sideOnParent);

                    
                    if (at.agent.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren >= 1 && at.agent.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren < at.agent.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren)
                    {
                        agent.currentParent = at.agent;
                        at.agent.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren++;
                    }

                }
            }
        }
    }

    public Vector3 GetAxis(AxisDirections axis)
    {
        return vectorAxis[(int)axis];
    }

    public void InitialiseDirections()
    {
        vectorAxis[0] = Vector3.up;
        vectorAxis[1] = Vector3.down;
        vectorAxis[2] = Vector3.forward;
        vectorAxis[3] = Vector3.back;
        vectorAxis[4] = Vector3.right;
        vectorAxis[5] = Vector3.left;
    }


}

