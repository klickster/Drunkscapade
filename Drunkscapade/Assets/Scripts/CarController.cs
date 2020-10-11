using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int speed;
    [SerializeField] private float pushForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;

        //if (transform.position.y < 0) Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();

            if(player != null)
            {
                player.PushPlayer(transform.forward * pushForce, true);
            }
        }
    }
}