using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    public RoomArrays refArrays = new RoomArrays();

    [SerializeField] private List<GameObject> hRooms = new List<GameObject>();
    [SerializeField] private List<GameObject> aRooms  = new List<GameObject>();

    [Header("Current Rooms")]
    public GameObject currentHumanRoom;
    public GameObject currentArtefactRoom;

    GameObject spawnedHumanRoom;
    GameObject spawnedArtefactRoom;

    [Header("Spawns")]
    public Transform[] spawns;

    [HideInInspector] public bool isArtefactLeftSide = false;

    int randomIndex = 0;

    public void SetTempLists()
    {
        hRooms = refArrays.humanRooms.ToList();
        aRooms = refArrays.artefactRooms.ToList();
    }
    public void PickRoomPair()
    {
        randomIndex = Random.Range(0, hRooms.Count);
        currentHumanRoom = hRooms[randomIndex];
        currentArtefactRoom = aRooms[randomIndex];

        hRooms.RemoveAt(randomIndex);
        aRooms.RemoveAt(randomIndex);
    }

    public void SpawnPair()
    {
        PickRoomPair();
        // pick a number 0 or 1
        if (Random.value < 0.5f)
        {
            // Human spawns left
            spawnedHumanRoom = Instantiate(currentHumanRoom, spawns[0].transform.position, spawns[0].transform.rotation);
            spawnedArtefactRoom = Instantiate(currentArtefactRoom, spawns[1].transform.position, spawns[1].transform.rotation);
            isArtefactLeftSide = false;
        }
        else
        {
            // artefact spawns left
            spawnedArtefactRoom = Instantiate(currentArtefactRoom, spawns[0].transform.position, spawns[0].transform.rotation);
            spawnedHumanRoom = Instantiate(currentHumanRoom, spawns[1].transform.position, spawns[1].transform.rotation);
            isArtefactLeftSide = true;
        }
    }

    public void DespawnPair()
    {
        Destroy(spawnedHumanRoom);
        Destroy(spawnedArtefactRoom);
        isArtefactLeftSide = false;
    }

}
