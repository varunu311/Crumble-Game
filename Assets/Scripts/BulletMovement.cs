using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour
{
    public float bulletSpeed = 25.0f;
    private float originalSpeed;
    public float minSpeed = 0.0f; // Set this to your desired minimum speed
    public float speedReduction = 90;
    public float elevation = 0;
    public float bulletTimeDuration = 5.0f; // Duration of bullet time in seconds
    public static bool canShoot = true;
    public float bulletTimeCooldown = 5.0f; // Cooldown duration for bullet time
    private bool isInCooldown = false;
    void Start()
    {
        // Store the original speed
        originalSpeed = bulletSpeed;
    }

    void Update()
    {
        bulletSpeed = Mathf.Max(0, bulletSpeed);

        Vector3 forwardMovement = transform.forward * bulletSpeed * Time.deltaTime;
        forwardMovement.y = elevation; // Ensures elevation is not changed

        transform.position += forwardMovement;

        // Check if the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P) && !isInCooldown)
        {
            StartCoroutine(EnterBulletTime());
            Debug.Log("P Pressed, Speed: " + bulletSpeed);
        }
    }

    IEnumerator EnterBulletTime()
    {
        isInCooldown = true; // Start the cooldown

        canShoot = false;

        // Reduce speed for bullet time
        bulletSpeed = Mathf.Max(minSpeed, bulletSpeed - speedReduction);

        // Wait for the duration of bullet time
        yield return new WaitForSeconds(bulletTimeDuration);

        // Return to original speed and allow shooting again
        bulletSpeed = originalSpeed;
        canShoot = true;
    
        yield return new WaitForSeconds(bulletTimeCooldown);

        isInCooldown = false; // End the cooldown

    }
}
