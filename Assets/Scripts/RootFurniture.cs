using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class RootFurniture : ScriptableObject
{
    [Header("General")]
    public string furnitureName;
    public TypeOfFurniture typeOfFurniture;
    public AgentState state;

    [Header("Agent Sides")]
    public Sides[] sides;
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

public enum TypeOfFurniture
{
    BED,
    BEDSIDE_CABINET,
    CHAIR,
    DESK,
    DRAWERS,
    FLOOR,
    TABLE_LAMP,
    WARDROBE
}
