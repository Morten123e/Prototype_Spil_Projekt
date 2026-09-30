using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour
{
    Vector3 touchPositionOffset; // Bruges til at vide hvor på planten man trykker
    float fixedY;

    private LevelUI levelUI;
    private PlantSlotManager slotManager;
    private int currentSlot;

    private bool isDragging = false;

    private void Start()
    {
        // Finder vores PlantSlotManager
        slotManager = FindFirstObjectByType<PlantSlotManager>();
        //finder vores levelUI
        levelUI = FindFirstObjectByType<LevelUI>();
    }

    private Vector3 GetTouchWorldPosition()
    {
        // Finder touch-positionen på skærmen
        Vector2 touchPosition =
            Touchscreen.current.primaryTouch.position.ReadValue();

        // Konverterer screen position til world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(
                touchPosition.x,
                touchPosition.y,
                -Camera.main.transform.position.z
            )
        );

        return worldPosition;
    }

    private void Update()
    {
        // Spilleren trykker på skærmen
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector3 touchPosition = GetTouchWorldPosition();

            // Finder ud af om spilleren trykkede på denne plante
            Collider2D hit =
                Physics2D.OverlapPoint(touchPosition);

            if (hit != null && hit.gameObject == gameObject)
            {
                StartDragging();
                levelUI.removeInstructions();
            }
        }

        // Spilleren trækker fingeren
        if (isDragging &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            Drag();
        }

        // Spilleren løfter fingeren
        if (isDragging &&
            Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        {
            StopDragging();
        }
    }

    private void StartDragging()
    {
        isDragging = true;

        // Fanger touch-positionen
        touchPositionOffset =
            transform.position - GetTouchWorldPosition();

        // Gemmer plantens Y-position
        fixedY = transform.position.y;
    }

    private void Drag()
    {
        // Finder touch-positionen
        Vector3 newPosition =
            GetTouchWorldPosition() + touchPositionOffset;

        //bruger vi til at lave en slags mur så du ikke kan dragge planterne ud af frame
        float clampedX = Mathf.Clamp(newPosition.x, -10f, 8f);

        transform.position = new Vector3(
            clampedX,
            fixedY,
            transform.position.z
        );

        // Finder hvilken slot vi er kommet hen imod
        int newSlot =
            slotManager.FindClosestSlot(transform.position);

        // Hvis vi er kommet over i en ny slot,
        // flytter vi planterne
        if (newSlot != currentSlot)
        {
            slotManager.MovePlantToNewSlot(
                this,
                newSlot
            );
        }
    }

    private void StopDragging()
    {
        isDragging = false;

        // Finder den slot vi er tættest på
        int newSlot =
            slotManager.FindClosestSlot(transform.position);

        // Flytter planten til slotten
        MoveToSlot(newSlot);
    }

    public void SetSlotAtStart(
        int slotIndex,
        Vector3 slotPosition)
    {
        // Gemmer hvilken slot planten står på
        currentSlot = slotIndex;

        // Sætter planten direkte på slotten
        transform.position = slotPosition;
    }

    public int GetCurrentSlot()
    {
        return currentSlot;
    }

    public void MoveToSlot(int slotIndex)
    {
        // Gemmer den nye slot
        currentSlot = slotIndex;

        // Flytter planten til slotten
        transform.position =
            slotManager.GetSlotPosition(slotIndex);
    }
    
}
