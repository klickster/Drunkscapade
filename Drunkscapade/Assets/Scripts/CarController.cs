using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;

        //if (transform.position.y < 50) Destroy(gameObject);
    }
}