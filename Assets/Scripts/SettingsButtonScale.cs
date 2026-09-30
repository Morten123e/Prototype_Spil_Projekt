using UnityEngine;

public class SettingsButtonScale : MonoBehaviour
{
    [SerializeField] float growSize = 0.1f;
    [SerializeField] float speed = 1f;
    private Vector3 startScale;

    float offset;

    float scale;

    void Start()
    {
       startScale = transform.localScale;
    }

    void Update()
    {
       offset = Mathf.Sin(Time.time * speed) * growSize;
       scale = 1 + offset;
       transform.localScale = startScale * scale;
    }
}
