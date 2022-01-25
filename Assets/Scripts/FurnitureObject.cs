using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Create New Furniture")]
public class FurnitureObject : ScriptableObject
{

    public string furnitureName;

    public AgentState state;
    public Sides[] sides;

    //public FurnitureObject[] potentialParents;

    public Parent[] potentialParents;
    
}
[Serializable]
public struct Sides
{
    public AxisDirections axis;

    [Range(0.0f, 10.0f)]
    public float distance;

    public float clearanceSpace;

    public int numberOfChildren;
    
    public LayerMask layers;
}

[Serializable]
public struct Parent
{
    public FurnitureObject parent;
    public AxisDirections placeableSide;
}

public enum AxisDirections
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    FORWARD,
    BACK
}

public enum AgentState
{
    SEARCH,
    ARRANGE,
    REST,
    SLEEP
}
