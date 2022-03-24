using System.Collections;
using System.Linq;
using UnityEngine;

public class MoreAgentTest : MonoBehaviour
{
    public string furnitureName;

    public FurnitureObject agentSO;

    public FurnitureObject agent;

    [SerializeField] private AgentManager aManager;
    [SerializeField] private BoxCollider boxCollider;

    private Vector3[] vectorAxis = new Vector3[6];
    private Vector3[] transformDirs = new Vector3[6];
    private float[] axisScale = new float[6];

    private float[] colliderBounds = new float[6];

    Vector3 rayOrigin;
    Vector3 rayDirection;

    void Awake()
    {
        agent = agentSO.GetInstance();
        agent.Init(furnitureName, this.gameObject);

        TryGetComponent(out boxCollider);

        InitialiseDirections();
        FindTransforms();

        /* if (agent.agentPlacement)
            Debug.Log(agent.agentPlacement.position); */
    }

    void Update()
    {
        /* if (this.tag == "agent")
            TestRay(); */
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


    void TestRay()
    {
        for (int i = 0; i < agent.sides.Count(); i++)
        {
            Sides currentAgentSide = agent.sides[i];

            //rayOrigin = transform.position + transform.TransformDirection(GetAxis(agent.sides[i].axis)) * (GetAxisScale(agent.sides[i].axis) / 2);
            rayOrigin = (transform.position + agent.boxCollider.center) + transform.TransformDirection(GetAxis(currentAgentSide.axis)) * GetColliderBounds(currentAgentSide.axis);
            rayDirection = (transform.rotation) * GetAxis(currentAgentSide.axis);// * GetAxis(currentAgentSide.axis) ) * currentAgentSide.distance;
            Gizmos.DrawWireCube(agent.boxCollider.center + rayDirection * currentAgentSide.distance, transform.lossyScale);
            if (Physics.BoxCast(agent.boxCollider.center, transform.localScale, rayDirection, transform.rotation, currentAgentSide.distance, currentAgentSide.layers))
            {

            }
            //Debug.DrawRay(rayOrigin, rayDirection, Color.red);

            //RaycastHit groundHit;
            /* if (Physics.Raycast(rayOrigin, rayDirection, out groundHit, currentAgentSide.distance, currentAgentSide.layers))
            {
                Debug.DrawRay(rayOrigin, rayDirection, Color.green);
            } */
        }
    }

    public void InitialiseDirections()
    {
        vectorAxis[0] = Vector3.up;
        vectorAxis[1] = Vector3.down;
        vectorAxis[2] = Vector3.forward;
        vectorAxis[3] = Vector3.back;
        vectorAxis[4] = Vector3.right;
        vectorAxis[5] = Vector3.left;

        transformDirs[0] = transform.up;
        transformDirs[1] = -transform.up;
        transformDirs[2] = transform.forward;
        transformDirs[3] = -transform.forward;
        transformDirs[4] = transform.right;
        transformDirs[5] = -transform.right;

        axisScale[0] = agent.gameObject.transform.localScale.y;
        axisScale[1] = agent.gameObject.transform.localScale.y;
        axisScale[2] = agent.gameObject.transform.localScale.z;
        axisScale[3] = agent.gameObject.transform.localScale.z;
        axisScale[4] = agent.gameObject.transform.localScale.x;
        axisScale[5] = agent.gameObject.transform.localScale.x;

        colliderBounds[0] = agent.boxCollider.bounds.extents.y;
        colliderBounds[1] = agent.boxCollider.bounds.extents.y;
        colliderBounds[2] = agent.boxCollider.bounds.extents.z;
        colliderBounds[3] = agent.boxCollider.bounds.extents.z;
        colliderBounds[4] = agent.boxCollider.bounds.extents.x;
        colliderBounds[5] = agent.boxCollider.bounds.extents.x;
    }

    public Vector3 GetAxis(AxisDirections axis) { return vectorAxis[(int)axis]; }
    public Vector3 GetTransformDir(AxisDirections axis) { return transformDirs[(int)axis]; }
    public float GetAxisScale(AxisDirections axis) { return axisScale[(int)axis]; }
    public float GetColliderBounds(AxisDirections axis) { return colliderBounds[(int)axis];}


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

   /*  void OnTriggerEnter(Collider other)
    {
        if (other.tag == "agent" && this.tag == "agent")
        {
            Debug.Log(agent.name + " is colliding with: " + other.name);

            //agent.currentParent = null;
            agent.currentParentAgent.sides[(int)agent.currentParent.sideOnParent].currentChildren = 0;

            agent.currentParentAgent = null;
            agent.hasFoundParent = false;

            aManager.Shuffle();

            agent.state = AgentState.SEARCH;
        }
    } */
}
