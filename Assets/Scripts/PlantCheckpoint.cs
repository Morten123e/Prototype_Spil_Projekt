using UnityEngine;

public class PlantCheckpoint : MonoBehaviour
{
    private PlantSlotManager plantSlotManager;
    [SerializeField] private int sizeOrder;
    Draggable[] plants;

    private void Start()
    {
        // Finder vores PlantSlotManager
        plantSlotManager = FindFirstObjectByType<PlantSlotManager>();
        plants = plantSlotManager.GetPlants();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int GetSizeOrder() 
    { 
        return sizeOrder; 
    }
    public bool CorrectPlantPlacement()
    {
          for (int i = 0; i < plants.Length; i++)
        {
            if (plants[i].GetComponent<PlantCheckpoint>().GetSizeOrder() != i)
            {
                return false;
            }
        }
        return true;
        
    }

}
