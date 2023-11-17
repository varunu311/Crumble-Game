using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 7.0f;
    public GameObject bulletPrefab; // Reference to your bullet prefab
    public float dodgeForce = 10.0f;
    public float dodgeCooldown = 0.5f;
    private bool isGrounded;
    private Vector3 initialPosition; // Store the initial position
    private float lastDodgeTime = -Mathf.Infinity; // Initialize to a very early time
    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position; // Store the initial position

        // Freeze rotation constraints for all axes
        playerRigidbody.freezeRotation = true;
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
            Debug.Log("Jumped");
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //dodge logic
        if (Input.GetKeyDown(KeyCode.O) && Time.time - lastDodgeTime > dodgeCooldown)
        {
            if (moveDirection != Vector3.zero)
            {
                Debug.Log("Dodge!");
                playerRigidbody.AddForce(moveDirection * dodgeForce, ForceMode.Impulse);
                lastDodgeTime = Time.time; // Update the last dodge time
            }
        }

        // Shooting logic
        if (Input.GetKeyDown(KeyCode.Return) && BulletMovement.canShoot) // You can customize the input button
        {
            Debug.Log("Shoot!");
            Shoot();
        }

        

        // Check if the player's Y position falls below -50
        if (transform.position.y < -20)
        {
            // If so, reset the player's position to the initial position
            transform.position = initialPosition;
        }
    }
    

void Shoot()
{
    if (bulletPrefab != null)
    {
        Vector3 bulletSpawnPosition = transform.position + transform.forward * 1.0f;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, transform.rotation);        
    }
}



}
