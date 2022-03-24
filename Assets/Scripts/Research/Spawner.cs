using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/* 
    File: Spawner.cs
    Author: Thomas (Tom) O'Leary

    Summary:
        This class is responsible for randomly selecting the order in which 
        the Pair of rooms get shown during the Research Study.
 */
public class Spawner : MonoBehaviour
{

    #region Variables
    [Header("Arrays/Lists")]
    public RoomArrays rooms;
    [SerializeField] private List<GameObject> hRooms;
    [SerializeField] private List<GameObject> aRooms;

    
    [Header("Current Rooms")]
    public GameObject currentHumanRoom;
    public GameObject currentArtefactRoom;
    public int currentPairID = 0;

    GameObject spawnedHumanRoom;
    GameObject spawnedArtefactRoom;

    [Header("Spawns")]
    public Transform[] spawns;

    [Header("Misc")]
    [HideInInspector] public bool isArtefactLeftSide = false;
    int randomIndex = 0;

    #endregion Variables

    ///<summary>
    /// Creates a temporary list for the Rooms to be picked & deleted from
    ///</summary>
    public void SetTempLists()
    {
        hRooms = rooms.humanRooms.ToList();
        aRooms = rooms.artefactRooms.ToList();
    }

    ///<summary>
    /// Selects the next Pair of rooms
    ///</summary>
    public void PickRoomPair()
    {
        randomIndex = Random.Range(0, hRooms.Count);
        currentHumanRoom = hRooms[randomIndex];
        currentArtefactRoom = aRooms[randomIndex];

        currentPairID = GetPairID();

        hRooms.RemoveAt(randomIndex);
        aRooms.RemoveAt(randomIndex);
    }

    ///<summary>
    /// Spawns the pair of rooms on two Transforms in the scene.
    ///</summary>
    public void SpawnPair()
    {
        PickRoomPair();
        // pick a number 0 or 1
        if (Random.value < 0.5f)
        {
            // Human Spawns Left
            spawnedHumanRoom = Instantiate(currentHumanRoom, spawns[0].transform.position, spawns[0].transform.rotation);
            // Artefact Spawns Right
            spawnedArtefactRoom = Instantiate(currentArtefactRoom, spawns[1].transform.position, spawns[1].transform.rotation);
            isArtefactLeftSide = false;
        }
        else
        {
            // Artefact Spawns Left
            spawnedArtefactRoom = Instantiate(currentArtefactRoom, spawns[0].transform.position, spawns[0].transform.rotation);
            // Human Spawns Right
            spawnedHumanRoom = Instantiate(currentHumanRoom, spawns[1].transform.position, spawns[1].transform.rotation);
            isArtefactLeftSide = true;
        }
    }

    ///<summary>
    /// Despawns the current pair of Rooms
    ///</summary>
    public void DespawnPair()
    {
        Destroy(spawnedHumanRoom);
        Destroy(spawnedArtefactRoom);
        isArtefactLeftSide = false;
    }

    ///<summary>
    /// Get's the Room's pair ID.
    /// For e.g. aRoom1 & hRoom1 is picked = the pair ID is 1
    ///</summary>
    public int GetPairID()
    {
        for (int i = 0; i < rooms.humanRooms.Count(); i++)
        {
            if (currentHumanRoom == rooms.humanRooms[i])
            {
                return i + 1;
            }
        }
        return 0;
    }

}
