using UnityEngine;

public class KrukkeSkaar : MonoBehaviour
{
    public float pressedScale = 1.15f;
    public float scaleSpeed = 15f;

    private SpriteRenderer spriteRenderer;
    private Vector3 normalScale;
    private Vector3 targetScale;

    // static = fælles for alle brikker. Tæller op hver gang en brik samles op.
    private static int topSortingOrder = 100;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalScale = transform.localScale;
        targetScale = normalScale;
    }

    void Update()
    {
        // Flytter størrelsen en del af vejen mod targetScale hvert frame = blød animation
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
    }

    public void PickUp()
    {
        targetScale = normalScale * pressedScale;

        topSortingOrder++;
        spriteRenderer.sortingOrder = topSortingOrder;
    }

    public void Release()
    {
        targetScale = normalScale;
    }

    public int GetSortingOrder()
    {
        return spriteRenderer.sortingOrder;
    }
}
