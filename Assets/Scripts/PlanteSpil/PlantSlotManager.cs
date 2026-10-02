using UnityEngine;

public class PlantSlotManager : MonoBehaviour
{
    [SerializeField] private Transform[] slots;
    [SerializeField] private Draggable[] plants;
    private PlantCheckpoint plantChecPoint;

    private void Start()
    {
        plantChecPoint = FindFirstObjectByType<PlantCheckpoint>();

        // Sætter alle planterne på deres slots med det samme
        for (int i = 0; i < plants.Length; i++)
        {
            plants[i].SetSlotAtStart(i, slots[i].position);
        }
    }

    public int FindClosestSlot(Vector3 plantPosition)
    {
        int closestSlot = 0;
        float shortestDistance = Mathf.Infinity;

        // Går igennem alle vores slots
        for (int i = 0; i < slots.Length; i++)
        {
            float distance = Mathf.Abs(
                plantPosition.x - slots[i].position.x
            );

            // Hvis denne slot er tættere på end den tidligere
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestSlot = i;
            }
        }

        return closestSlot;
    }

    public Vector3 GetSlotPosition(int slotIndex)
    {
        // Giver positionen på den slot vi spørger efter
        return slots[slotIndex].position;
    }

    public void MovePlantToNewSlot(Draggable draggedPlant, int newSlot)
    {
        int oldSlot = draggedPlant.GetCurrentSlot();

        // Hvis vi stadig er på samme slot, skal der ikke ske noget
        if (oldSlot == newSlot)
        {
            return;
        }

        // Hvis vi bevæger os mod højre
        if (newSlot > oldSlot)
        {
            for (int i = oldSlot; i < newSlot; i++)
            {
                plants[i] = plants[i + 1];

                // Flytter planten der stod til højre, en plads til venstre
                plants[i].MoveToSlot(i);
            }
        }

        // Hvis vi bevæger os mod venstre
        else
        {
            for (int i = oldSlot; i > newSlot; i--)
            {
                plants[i] = plants[i - 1];

                // Flytter planten der stod til venstre, en plads til højre
                plants[i].MoveToSlot(i);
            }
        }

        // Den plante vi trækker får den nye slot
        plants[newSlot] = draggedPlant;

        draggedPlant.MoveToSlot(newSlot);
        if (plantChecPoint.CorrectPlantPlacement() == true)
        {
            
        }
    }

    public Draggable[] GetPlants()
    {
        return plants;
    }
}
