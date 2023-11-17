using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Destroy the power-up
            Destroy(gameObject);
        }
    }
}
