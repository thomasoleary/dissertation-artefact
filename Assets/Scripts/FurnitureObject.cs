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

    /// <summary>
    /// The furnitures name
    /// </summary>
    public string furnitureName;

    /// <summary>
    /// The Agents state
    /// </summary>
    public AgentState state;

    /// <summary>
    /// The Agents sides.
    /// </summary>
    public Sides[] sides = defaultSidesValues.ToArray();

    /// <summary>
    /// An array of potential parents for the Agent
    /// </summary>
    public Parent[] potentialParents;

    /// <summary>
    /// The current parent of the Agent
    /// </summary>
    public FurnitureObject currentParent;

    /// <summary>
    /// Returns an instance of the Agent
    /// </summary>
    public FurnitureObject GetInstance()
    {
        return Instantiate(this);
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

    // How much clearance space this distance requires
    public float clearanceSpace;

    // How many children can be placed on the side
    public int spaceForChildren;

    // How many current children are placed on the side
    public int currentChildren;

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
    public FurnitureObject parent;
    
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


/// <summary>
/// The Agents state
/// </summary>
public enum AgentState
{
    SEARCH,
    ARRANGE,
    REST,
    SLEEP
}
