using System;
using System.Collections;
using System.Linq;
using UnityEngine;
/*
    Test script where I'm putting all logic together, once it's at a decent state - I will change it into a better looking script with hopefully cleaner code :)
    - Tom
*/
public class AgentTest : MonoBehaviour
{

    public string furnitureName;

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
    private Vector3[] transformDirs = new Vector3[6];
    private float[] boundsExtents = new float[6];

    [SerializeField] private findobjects findObjects;

    [SerializeField] private BoxCollider objectCollider;


    void Awake()
    {
        agent = agentAsset.GetInstance();
        agent.Init(furnitureName, this.gameObject);
        InitialiseDirections();

    }

    // Start is called before the first frame update
    void Start()
    {
        objectCollider = gameObject.GetComponent<BoxCollider>();

        
    }

    //bool doOnce = false;
    // Update is called once per frame
    void Update()
    {
        /* if (!doOnce)
        {
            StartCoroutine(MoreSearch());
            doOnce = true;
        } */
        TestRay();
    }

    Vector3 rayOrigin;
    Vector3 rayDirection;
    void TestRay()
    {
        for (int i = 0; i < agent.sides.Count(); i++)
        {
            rayOrigin = transform.position + transform.TransformDirection(GetAxis(agent.sides[i].axis)) * (GetBoundsAxis(agent.sides[i].axis) / 2);
            rayDirection = (transform.rotation * GetAxis(agent.sides[i].axis) ) * agent.sides[i].distance;
            
            Debug.DrawRay(rayOrigin, rayDirection, Color.red);

            RaycastHit groundHit;
            if (Physics.Raycast(rayOrigin, rayDirection, out groundHit, agent.sides[i].distance, agent.sides[i].layers))
            {
                Debug.DrawRay(rayOrigin, rayDirection, Color.green);
            }
        }
    }

   /*  IEnumerator MoreSearch()
    {
        if (agent.state == AgentState.SLEEP)
            yield return null;
            
        for (int j = 0; j <= agent.searchIndex; j++)
        {
            //Debug.Log(j);
            if (j == agent.searchIndex && !agent.hasFoundParent)
                Debug.Log(agent.furnitureName + " could not find parent");
                yield return null;
            
            for (int i = 0; i < agent.potentialParents.Count(); i++)
            {
                foreach (FurnitureObject fobj in findObjects.furnitureInScene)
                {
                    if (!agent.hasFoundParent)
                    {
                        // if the type of furniture in the scene is the same type as a potential parent
                        if (agent.potentialParents[i].parentType == fobj.typeOfFurniture)
                        {
                            // find if potential parent has space for agent
                            if (fobj.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren >= 1)
                                if (fobj.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren < fobj.sides[(int)agent.potentialParents[i].sideOnParent].spaceForChildren)
                                {
                                    //Debug.Log("There is space for: " + agent.name + " on " + fobj.name + " " + fobj.sides[(int)agent.potentialParents[i].sideOnParent].axis);

                                    fobj.sides[(int)agent.potentialParents[i].sideOnParent].currentChildren++;
                                    agent.currentParent = fobj;
                                    agent.hasFoundParent = true;
                                    Debug.Log(agent.furnitureName + " has found parent & side: " + fobj.furnitureName + " " + fobj.sides[(int)agent.potentialParents[i].sideOnParent].axis);
                                    yield return null;
                                }
                        }
                    }
                }
            }
        }
        yield return null;
    } */


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

        transformDirs[0] = transform.up;
        transformDirs[1] = -transform.up;
        transformDirs[2] = transform.forward;
        transformDirs[3] = -transform.forward;
        transformDirs[4] = transform.right;
        transformDirs[5] = -transform.right;

        boundsExtents[0] = agent.gameObject.transform.localScale.y;
        boundsExtents[1] = agent.gameObject.transform.localScale.y;
        boundsExtents[2] = agent.gameObject.transform.localScale.z;
        boundsExtents[3] = agent.gameObject.transform.localScale.z;
        boundsExtents[4] = agent.gameObject.transform.localScale.x;
        boundsExtents[5] = agent.gameObject.transform.localScale.x;
    }

    /// <summary>
    /// Returns an element in the vectorAxis array dependent on the AxisDirections enum
    /// E.g. AxisDirections.UP will return Vector3.up
    /// </summary>
    public Vector3 GetAxis(AxisDirections axis)
    {
        return vectorAxis[(int)axis];
    }

    public Vector3 GetTransformDir(AxisDirections axis)
    {
        return transformDirs[(int)axis];
    }

    public float GetBoundsAxis(AxisDirections axis)
    {
        return boundsExtents[(int)axis];
    }

}

