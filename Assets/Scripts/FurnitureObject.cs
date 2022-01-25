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

        s_DefaultEnumValues = new ReadOnlyCollection<Sides>(sides);
    }
    static readonly ReadOnlyCollection<Sides> s_DefaultEnumValues;

    public string furnitureName;

    public AgentState state;
    public Sides[] sides = s_DefaultEnumValues.ToArray();

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
