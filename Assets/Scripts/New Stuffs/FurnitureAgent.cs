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
    public RootFurniture currentParent;

    /* public FurnitureAgent GetInstance()
    {
        return Instantiate(this);
    } */

    public virtual void Init(string furnitureName)
    {
        this.furnitureName = furnitureName;
    }
}

