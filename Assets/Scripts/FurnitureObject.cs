using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Create New Furniture")]
public class FurnitureObject : ScriptableObject
{

    public string furnitureName;
    public Sides[] sides;

    //public FurnitureObject[] potentialParents;
    
}
[Serializable]
public struct Sides
{
    public AxisDirections axis;
    
    [Range(0.0f, 10.0f)]
    public float distance;

    //public int numberOfChildren;

    //public float clearanceSpace;

    public LayerMask layers;
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
