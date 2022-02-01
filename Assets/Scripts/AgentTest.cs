using System;
using System.Linq;
using UnityEngine;
/*
    Test script where I'm putting all logic together, once it's at a decent state - I will change it into a better looking script with hopefully cleaner code :)
    - Tom
*/
public class AgentTest : MonoBehaviour
{
    /// <summary>
    /// Original SO reference, that will be instantiated
    /// </summary>
    public FurnitureObject agentAsset;

    /// <summary>
    /// The actual Agent being refferred to (using the instantiated agentAsset)
    /// </summary>
    public FurnitureObject agent;

    /// <summary>
    /// This array is used to match up the AxisDirections to the correct Vector3
    /// </summary>
    private Vector3[] vectorAxis = new Vector3[6];

    [SerializeField] private findobjects findObjects;

    void Awake()
    {
        agent = agentAsset.GetInstance();
        //Debug.Log(agent);
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameObject fObj = GameObject.FindGameObjectWithTag("find");
        //findObjects = fObj.GetComponent<findobjects>() as findobjects;

        InitialiseDirections();

        AnotherSearch();
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
    /* void Search()
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
    } */

    // The current test Search function I am using to find a potential parent
    /* void OtherSearch()
    {
        for (int i = 0; i < agent.potentialParents.Count(); i++)
        {
            foreach (AgentTest at in findObjects.agentTests)
            {
                if (agent.potentialParents[i].parent == at.agent)
                {
                    Debug.Log(agent + " has found parent: " + at.agent);
                    Debug.Log((int)agent.potentialParents[i].sideOnParent);

                    // if potential parent has more than 1 space for children (on specific side)
                    // and potential parents current children is less than the space for children (on specific side)
                    if (at.agent.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren >= 1 && at.agent.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren < at.agent.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren)
                    {
                        // potential parent becomes parent
                        agent.currentParent = at.agent;

                        // increase the parents amount of children on side
                        at.agent.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren++;
                    }

                }
            }
        }
    }
 */
    
    void AnotherSearch()
    {
        for (int i = 0; i < agent.potentialParents.Count(); i++)
        {
            foreach (FurnitureObject fobj in findObjects.furnitureInScene)
            {
                if (agent.potentialParents[i].parentType == fobj.typeOfFurniture)
                {
                    Debug.Log(agent + " has found parent: " + fobj);
                }
            }
        }
    }

    /// <summary>
    /// Returns an element in the vectorAxis array dependent on the AxisDirections enum
    /// E.g. AxisDirections.UP will return Vector3.up
    /// </summary>
    public Vector3 GetAxis(AxisDirections axis)
    {
        return vectorAxis[(int)axis];
    }

    /// <summary>
    /// Each element in the vectorAxis array being initialised with a Vector3 direction
    /// </summary>
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

