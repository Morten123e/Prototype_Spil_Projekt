using UnityEngine;
using UnityEngine.InputSystem;

public class KrukkeSpil : MonoBehaviour
{
    [SerializeField] private KrukkeSkaar[] shards;               
    [SerializeField] private Transform[] spots;           
    [SerializeField] private GameObject levelCompleteMenu;
    [SerializeField] private float snapDistance = 0.6f;    

    private int draggedIndex = -1; // Nummeret på det skår, vi trækker i. -1 = trækker ikke i noget
    private Vector3 grabOffset;    // Hvor på skåret man tog fat, så det ikke hopper under fingeren
    private int placedCount = 0;   // Hvor mange skår der sidder fast

    private void Update()
    {
       
        Pointer pointer = Pointer.current;
        if (pointer == null) return;

       
        Vector3 pointerPosition = Camera.main.ScreenToWorldPoint(pointer.position.ReadValue());
        pointerPosition.z = 0;

        if (pointer.press.wasPressedThisFrame)
        {
            PickUp(pointerPosition);
        }

       
        if (draggedIndex == -1) return;

        
        shards[draggedIndex].transform.position = pointerPosition + grabOffset;

        if (pointer.press.wasReleasedThisFrame)
        {
            Drop();
        }
    }

    private void PickUp(Vector3 pointerPosition)
    {
        
        for (int i = 0; i < shards.Length; i++)
        {
            Collider2D shardCollider = shards[i].GetComponent<Collider2D>();

            
            if (!shardCollider.enabled) continue;

           
            if (!shardCollider.OverlapPoint(pointerPosition)) continue;

            
            if (draggedIndex == -1 || shards[i].GetSortingOrder() > shards[draggedIndex].GetSortingOrder())
            {
                draggedIndex = i;
            }
        }

       
        if (draggedIndex == -1) return;

        grabOffset = shards[draggedIndex].transform.position - pointerPosition;
        shards[draggedIndex].PickUp(); 
    }

    private void Drop()
    {
        KrukkeSkaar shard = shards[draggedIndex];
        Transform spot = spots[draggedIndex];

        shard.Release(); 

       
        if (Vector2.Distance(shard.transform.position, spot.position) < snapDistance)
        {
            shard.transform.position = spot.position;        
            shard.GetComponent<Collider2D>().enabled = false; 
            placedCount++;

            
            if (placedCount == shards.Length)
            {
                levelCompleteMenu.SetActive(true);
            }
        }

        draggedIndex = -1;
    }
}
