using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Agent", menuName = "Create New Agent")]
public class FurnitureAgent : RootFurniture
{
    /// <summary>
    /// Class constructor to initialise the Agents sides with the neccessary AxisDirections
    /// </summary>
    static FurnitureAgent()
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

    [Header("Agent Sides")]
    /// <summary>
    /// The Sides array that contains all information an Agent needs regarding each side of itself
    /// </summary>
    public Sides[] sides = defaultSidesValues.ToArray();

    [Header("Agent Parent Details")]
    /// <summary>
    /// An array of potential parents for the Agent
    /// </summary>
    public Parent[] potentialParents;

    /// <summary>
    /// If the Agent has found a parent or not
    /// </summary>
    public bool hasFoundParent = false;

    /// <summary>
    /// The current parent of the Agent
    /// </summary>
    public FurnitureAgent currentParent;

    public FurnitureAgent GetInstance()
    {
        return Instantiate(this);
    }

    public virtual void Init(string furnitureName)
    {
        this.furnitureName = furnitureName;
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

