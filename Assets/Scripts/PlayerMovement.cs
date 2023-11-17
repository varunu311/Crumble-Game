using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 7.0f;
    public GameObject bulletPrefab; // Reference to your bullet prefab
    public GameObject powerup; 
    
    public float bulletSpeed = 10.0f;
    
    private GameObject powerupI; 
    private float xRange = 20; 
    private float yRange = 30;
    private bool isGrounded;
    private Vector3 initialPosition; // Store the initial position

    private Rigidbody playerRigidbody;

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("powerup")) {
            UpdateRandomPosition();
            Debug.Log("collision triggered");
        }
    }

    void Start()
    {
        Vector3 powerupPos = new Vector3(10, 1.5f, 5);
        Quaternion powerupRot = Quaternion.identity; 
        powerupI = Instantiate(powerup, powerupPos, powerupRot);

        if (powerupI != null){
            powerupI.gameObject.tag = "powerup";
        }

        playerRigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position; // Store the initial position

        // Freeze rotation constraints for all axes
        playerRigidbody.freezeRotation = true;
    }

    void UpdateRandomPosition(){
        Vector3 randomPos = new Vector3(Random.Range(0, xRange), 1.5f, Random.Range(0, yRange) );

        if (powerupI != null){
            powerupI.transform.position = randomPos; 
        }
    }

    void Update()
    {
        // Check if the player is grounded.
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);
        // Get input for movement.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction.
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        moveDirection.Normalize();

        // Move the player.
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Jump when the player is grounded and the space bar is pressed.
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Shooting logic
        if (Input.GetKeyDown(KeyCode.Return)) // You can customize the input button
        {
            Debug.Log("Shoot!");
            Shoot();
        }

        // Check if the player's Y position falls below -50
        if (transform.position.y < -50)
        {
            // If so, reset the player's position to the initial position
            transform.position = initialPosition;
        }

        // check for collison with 
    }

    void Shoot()
    {
        // Instantiate and spawn the bullet, as you did before.
        // But remove the bullet movement logic from this script.
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}