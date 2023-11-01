using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float bulletSpeed = 10.0f;

    void Start()
    {
        // Set the initial velocity of the bullet
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    void Update()
    {
        // You can add additional logic here if needed
    }
}
