using System;

using UnityEngine;

[CreateAssetMenu(fileName = "Root Furniture", menuName = "Create New Root")]
public class RootFurniture : ScriptableObject
{
    [Header("General")]
    public string furnitureName;
    public TypeOfFurniture typeOfFurniture;
    public AgentState state;
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
    WALL,
    WARDROBE
}
