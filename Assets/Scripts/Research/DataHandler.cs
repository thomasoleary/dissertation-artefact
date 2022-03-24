using UnityEngine;
using UnityEngine.UI;
using System.IO;

/* 
    File: DataHandler.cs
    Author: Thomas (Tom) O'Leary

    Summary:
        This class handles the collection and saving of nearly all data regarding the Research Study.
 */

public class DataHandler : MonoBehaviour
{
    #region Variables
    public Spawner spawner;

    string path = "";

    [Header("Study Related Things")]
    [SerializeField]private int participantID = 1;
    [SerializeField]private int participantStage = 0;
    [SerializeField]private int pairNumber = 0;
    private bool isArtefactLeftSide = false;
    private bool isArtefactPicked = false;

    [Header("UI Things")]
    public Button aButton;
    public Button bButton;

    [Header("Menu Things")]
    public GameObject disclaimerScreen;
    public GameObject researchScreen;
    public GameObject stage2Screen;
    public GameObject finishScreen;

    #endregion Variables

    void Awake()
    {
        // File path for the CSV file where the Research Data is saved to
        path = Application.dataPath + "/ResearchData.csv";
    }

    void Start()
    {
        aButton.onClick.AddListener(delegate{ButtonClicked(0);});
        bButton.onClick.AddListener(delegate{ButtonClicked(1);});
    }

    ///<summary>
    /// Run whenever the A/B buttons are clicked.
    ///</summary>
    /// <param name="index">If index is 0, the left button was picked otherwise the right was picked.</param>
    void ButtonClicked(int index)
    {
        isArtefactLeftSide = spawner.isArtefactLeftSide;

        // A button is picked & isArtefactLeftSide is true
        // Meaning the artefact is on the left side & was picked
        if (index == 0 && isArtefactLeftSide)
        {
            Debug.Log("Artefact was picked");
            isArtefactPicked = true;
        }
        // B button is picked & isArtefactLeftSide is false
        // Meaning the artefact is on the right side & was picked
        else if (index == 1 && !isArtefactLeftSide)
        {
            Debug.Log("Artefact was picked");
            isArtefactPicked = true;
        }
        // Otherwise the artefact wasn't picked but the Human room was
        else
        {
            Debug.Log("Human was picked");
            isArtefactPicked = false;
        }


        // Write to csv with relevant information before moving on
        WriteToCSV(participantID, participantStage, spawner.currentPairID, spawner.currentArtefactRoom.name, spawner.currentHumanRoom.name, isArtefactPicked);
        
        CheckState();
    }

    ///<summary>
    /// Checks if the study has finished Stage 1/Stage 2
    ///</summary>
    public void CheckState()
    {
        if (pairNumber >= 5)
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

        if (pairNumber < 5)
        {
            NextPair();
        }
    }

    ///<summary>
    /// Starts a Stage of the Research Study
    ///</summary>
    public void NextStage()
    {
        participantStage++;
        pairNumber = 0;

        spawner.SetTempLists();
        NextPair();
    }

    ///<summary>
    /// Gets the Next Pair for the Research Study
    ///</summary>
    public void NextPair()
    {
        pairNumber++;
        spawner.DespawnPair();
        spawner.SpawnPair();
    }

    ///<summary>
    /// Finishes the Study for the participant
    /// Resetting all neccessary variables
    ///</summary>
    public void FinishStudy()
    {
        participantID++;
        participantStage = 0;
        pairNumber = 0;

        finishScreen.SetActive(false);
        disclaimerScreen.SetActive(true);
    }

    ///<summary>
    /// Writes neccessary data to a CSV file.
    ///</summary>
    bool firstTime = true;
    public void WriteToCSV(int partID, int stage, int pair, string aName, string hName, bool aPicked)
    {
        // Only writes what each row is on the first time being called.
        if (firstTime)
        {
            TextWriter tw = new StreamWriter(path, false);
            tw.WriteLine("PARTICIPANTID, STAGE, PAIRID, ARTEFACTNAME, HUMANNAME, ARTEFACTPICKED");
            tw.Close();
            firstTime = false;
        }

        // Data being written here
        TextWriter tw2 = new StreamWriter(path, true);
        tw2.WriteLine(partID + ", " + stage + ", " + pair + ", " + aName + ", " + hName + ", " + aPicked);
        tw2.Close();
    }

    ///<summary>
    /// Application Quits.
    ///</summary>
    public void Quit()
    {
        Application.Quit();
    }
}
