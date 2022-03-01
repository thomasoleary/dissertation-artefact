using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Create New Furniture")]
public class FurnitureObject : ScriptableObject
{
    
    /// <summary>
    /// Constructor that allocates a default side & distance to each element in the Sides array
    /// </summary>
    static FurnitureObject()
    {
        Sides[] sides = new Sides[6];
        foreach(var v in Enum.GetValues(typeof(AxisDirections)))
        {
            sides[(int)v].axis = (AxisDirections)v;
            sides[(int)v].distance = 1.0f;
        }

        defaultSidesValues = new ReadOnlyCollection<Sides>(sides);
    }
    static readonly ReadOnlyCollection<Sides> defaultSidesValues;

    [Header("General")]
    /// <summary>
    /// The furnitures name
    /// </summary>
    public string furnitureName;

    /// <summary>
    /// The type of furniture the object is
    /// </summary>
    public TypeOfFurniture typeOfFurniture;

    /// <summary>
    /// The Agents state
    /// </summary>
    public AgentState state;

    /// <summary>
    /// The BoxCollider of the Agent
    /// </summary>
    [HideInInspector] public BoxCollider boxCollider;

    /// <summary>
    /// The GameObject of the Agent
    /// </summary>
    [HideInInspector] public GameObject gameObject;

    [Header("Agent Sides")]
    /// <summary>
    /// The Agents sides.
    /// </summary>
    public Sides[] sides = defaultSidesValues.ToArray();

    [Header("Agent Parent Details")]
    /// <summary>
    /// An array of potential parents for the Agent
    /// </summary>
    public Parent[] potentialParents;

    /// <summary>
    /// Whether or not the Agent has found a parent
    /// </summary>
    public bool hasFoundParent = false;

    /// <summary>
    /// Whether or not the Agent can be a parent itself
    /// </summary>
    public bool CanBeParent => state == AgentState.REST || state == AgentState.SLEEP;

    /// <summary>
    /// The current parent of the Agent
    /// </summary>
    public FurnitureObject currentParent;

    public BoxCollider parentCollider;

    public GameObject parentGObj;

    [Header("Agent State Details")]
    public int searchIndex = 10;

    /// <summary>
    /// Returns an instance of the Agent
    /// </summary>
    public FurnitureObject GetInstance()
    {
        return Instantiate(this);
    }

    public virtual void Init(string name, GameObject gObj)
    {
        this.furnitureName = name;
        this.gameObject = gObj;
        this.boxCollider = gameObject.GetComponent<BoxCollider>();
    }
    
}

/// <summary>
/// Sides struct that contains data required for each side of the Agent
/// </summary>
[Serializable]
public struct Sides
{
    // The direction of the side
    public AxisDirections axis;

    // The distance of the side

    [Range(0.0f, 10.0f)]
    public float distance;

    public Vector3 origin;
    public Vector3 normal;
    

    // How much clearance space this distance requires
    public float clearanceSpace;

    // How many children can be placed on the side
    public int spaceForChildren;

    // How many current children are placed on the side
    public int currentChildren;

    // If the side has max amount of children
    public bool hasMaxChildren;

    // The layer that the side can raycast
    public LayerMask layers;
}


/// <summary>
/// Parent struct that handles neccessary data about a parent
/// </summary>
[Serializable]
public struct Parent
{
    // The type of agent the parent is
    //public FurnitureObject parent;

    public TypeOfFurniture parentType;
    
    // What side on the parent the agent should be placed on
    public AxisDirections sideOnParent;

    // What side the agent should place on the parent
    public AxisDirections sideOnAgent;
}

/// <summary>
/// All directions that are used
/// </summary>
public enum AxisDirections
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    FORWARD,
    BACK
}













