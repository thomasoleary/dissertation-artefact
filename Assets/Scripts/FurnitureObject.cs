using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Create New Furniture")]
public class FurnitureObject : ScriptableObject
{
    static FurnitureObject()
    {
        Sides[] sides = new Sides[6];
        foreach(var v in Enum.GetValues(typeof(AxisDirections)))
        {
            sides[(int)v].axis = (AxisDirections)v;
            sides[(int)v].distance = 1.0f;
        }

        defaultEnumValues = new ReadOnlyCollection<Sides>(sides);
    }
    static readonly ReadOnlyCollection<Sides> defaultEnumValues;

    public string furnitureName;

    public AgentState state;
    public Sides[] sides = defaultEnumValues.ToArray();

    public Parent[] potentialParents;

    public FurnitureObject currentParent;
    
}

[Serializable]
public struct Sides
{
    public AxisDirections axis;

    [Range(0.0f, 10.0f)]
    public float distance;

    public float clearanceSpace;

    public int spaceForChildren;
    public int currentChildren;

    public LayerMask layers;
}

[Serializable]
public struct Parent
{
    public FurnitureObject parent;
    public AxisDirections sideOnParent;
    public AxisDirections sideOnAgent;
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
