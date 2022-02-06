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

    public bool hasFoundParent = false;

    /// <summary>
    /// The current parent of the Agent
    /// </summary>
    public FurnitureObject currentParent;

    [Header("Agent State Details")]
    public int searchIndex = 10;

    /// <summary>
    /// Returns an instance of the Agent
    /// </summary>
    public FurnitureObject GetInstance()
    {
        return Instantiate(this);
    }

    public virtual void Init(string furnitureName)
    {
        this.furnitureName = furnitureName;
        //Debug.Log(this.furnitureName + " test init");
    }
    
}











