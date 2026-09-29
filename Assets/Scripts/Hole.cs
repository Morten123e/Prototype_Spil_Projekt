using UnityEngine;

// Sidder på hvert hul/slot.
// Kræver: Collider2D med Is Trigger slået til, layer "Holes".
public class Hole : MonoBehaviour
{
    public ShapeType shape;
    [HideInInspector] public bool isFilled;
}
