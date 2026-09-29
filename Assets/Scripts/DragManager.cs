using UnityEngine;
using UnityEngine.InputSystem;

// Sidder på ét tomt GameObject i scenen. Der må kun være én.
// Husk at vælge layers i pieceLayer og holeLayer i Inspectoren.
public class DragManager : MonoBehaviour
{
    public LayerMask pieceLayer;
    public LayerMask holeLayer;

    private Camera cam;
    private Piece draggedPiece;
    private Vector3 grabOffset;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Mus på computer, finger på mobil
        Pointer pointer = Pointer.current;
        if (pointer == null) return;

        Vector3 pointerWorld = GetPointerWorldPosition(pointer);

        if (pointer.press.wasPressedThisFrame)
        {
            TryPickUp(pointerWorld);
        }

        if (draggedPiece != null && pointer.press.isPressed)
        {
            draggedPiece.transform.position = pointerWorld + grabOffset;
        }

        if (draggedPiece != null && pointer.press.wasReleasedThisFrame)
        {
            Drop();
        }
    }

    Vector3 GetPointerWorldPosition(Pointer pointer)
    {
        Vector2 screenPos = pointer.position.ReadValue();
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;
        return worldPos;
    }

    void TryPickUp(Vector3 pointerWorld)
    {
        // Find alle brikker under fingeren og vælg den øverste
        Collider2D[] hits = Physics2D.OverlapPointAll(pointerWorld, pieceLayer);
        Piece topPiece = null;

        foreach (Collider2D hit in hits)
        {
            Piece piece = hit.GetComponent<Piece>();
            if (piece == null) continue;

            if (topPiece == null || piece.GetSortingOrder() > topPiece.GetSortingOrder())
            {
                topPiece = piece;
            }
        }

        if (topPiece == null) return;

        draggedPiece = topPiece;
        grabOffset = topPiece.transform.position - pointerWorld;
        draggedPiece.PickUp();
    }

    void Drop()
    {
        // Ligger brikkens midte inde i et huls collider?
        Collider2D holeHit = Physics2D.OverlapPoint(draggedPiece.transform.position, holeLayer);
        Hole hole = null;
        if (holeHit != null) hole = holeHit.GetComponent<Hole>();

        if (hole != null && !hole.isFilled && hole.shape == draggedPiece.shape)
        {
            draggedPiece.transform.position = hole.transform.position;
            hole.isFilled = true;
            draggedPiece.currentHole = hole;
        }

        // Ingen else: rammer den ikke et hul, bliver den bare liggende

        draggedPiece.Release();
        draggedPiece = null;
    }
}
