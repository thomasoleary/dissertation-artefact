using UnityEngine;

[CreateAssetMenu(fileName = "Root Furniture", menuName = "Create New Root")]
public class RootFurniture : ScriptableObject
{
    [Header("General")]
    /// <summary>
    /// The Agents name
    /// </summary>
    public string furnitureName;
    public TypeOfFurniture typeOfFurniture;
    public AgentState state;
    public bool CanBeParent => state == AgentState.REST || state == AgentState.SLEEP;

    public RootFurniture GetInstance()
    {
        return Instantiate(this);
    }
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

/// <summary>
/// The different Types of Furniture
/// </summary>
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
