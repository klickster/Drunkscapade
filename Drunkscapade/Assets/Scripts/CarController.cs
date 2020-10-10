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

        if (transform.position.y < 0) Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();

            // TODO: add player force
            // if(player != null) 
        }
    }
}