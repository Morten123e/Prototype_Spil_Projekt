using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
