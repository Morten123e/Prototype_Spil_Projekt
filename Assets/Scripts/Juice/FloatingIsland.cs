using UnityEngine;

public class FloatingIsland : MonoBehaviour
{
    [SerializeField] float amplitude = 0.1f;
    [SerializeField] float speed = 1f;
    private Vector3 startPosition;

    float offset;

    void Start()
    {
       startPosition = transform.position;
    }

    void Update()
    {
       offset = Mathf.Sin(Time.time * speed) * amplitude;
       transform.position = startPosition + new Vector3(0 ,offset, 0);
    }
}
