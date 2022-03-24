using UnityEngine;
using UnityEngine.UI;

public class DataHandler : MonoBehaviour
{
    public Spawner spawner = new Spawner();

    [Header("Study Related Things")]
    public int participantID = 1;
    public int participantStage = 0;
    public int pairID = 0;
    public bool isArtefactLeftSide = false;

    [Header("UI Things")]
    public Button aButton;
    public Button bButton;

    [Header("Menu Things")]

    public GameObject disclaimerScreen;
    public GameObject researchScreen;
    public GameObject stage2Screen;

    public GameObject finishScreen;

    void Start()
    {
        aButton.onClick.AddListener(delegate{CheckButton(0);});
        bButton.onClick.AddListener(delegate{CheckButton(1);});
    }

    void CheckButton(int index)
    {
        isArtefactLeftSide = spawner.isArtefactLeftSide;

        // A button is picked & isArtefactLeftSide is true
        // Meaning the artefact is on the left side
        if (index == 0 && isArtefactLeftSide)
        {
            Debug.Log("Artefact was picked");
        }
        // B button is picked & isArtefactLeftSide is false
        // Meaning the artefact is on the right side
        else if (index == 1 && !isArtefactLeftSide)
        {
            Debug.Log("Artefact was picked");
        }
        // Otherwise the artefact wasn't picked
        else
        {
            Debug.Log("Human was picked");
        }

        CheckState();
        
    }

    public void CheckState()
    {
        if (pairID >= 5)
        {
            if (participantStage == 1)
            {
                spawner.DespawnPair();
                researchScreen.SetActive(false);
                stage2Screen.SetActive(true);
            }
            if (participantStage == 2)
            {
                spawner.DespawnPair();
                researchScreen.SetActive(false);
                finishScreen.SetActive(true);
            }
            
        }

        if (pairID < 5)
        {
            NextPair();
        }
    }

    public void NextStage()
    {
        participantStage++;
        pairID = 0;

        spawner.SetTempLists();
        NextPair();
    }

    public void NextPair()
    {
        pairID++;
        spawner.DespawnPair();
        spawner.SpawnPair();
    }

    public void FinishStudy()
    {
        participantID++;
        participantStage = 0;
        pairID = 0;

        finishScreen.SetActive(false);
        disclaimerScreen.SetActive(true);

    }
}
