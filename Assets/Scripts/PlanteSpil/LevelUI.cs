using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private TMP_Text tryAgainText;
    [SerializeField] private GameObject levelCompleteMenu;
    private PlantCheckpoint plantCheckpoint;    
    private bool correctCheck;
    private void Start()
    {
        plantCheckpoint = FindFirstObjectByType<PlantCheckpoint>();
    }

    public void removeInstructions()
    {
        instructionText.gameObject.SetActive(false);
    }

    public void checkPlants()
    {
        correctCheck = plantCheckpoint.CorrectPlantPlacement();

        if(correctCheck)
        {
            levelComplete();
        }
        else
        {
            tryAgain();
            Invoke("hideTryAgain", 1f);
        }
    }

    private void levelComplete()
    {
        levelCompleteMenu.gameObject.SetActive(true);
    }

    private void tryAgain()
    {
       tryAgainText.gameObject.SetActive(true);
    }
    
    private void hideTryAgain()
    {
        tryAgainText.gameObject.SetActive(false);
    }
}


