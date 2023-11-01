using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingObject : MonoBehaviour
{
    public float disappearDelay = 2.0f; // Adjust this value to control the delay before the object disappears.
    public float reappearDelay = 3.0f; // Adjust this value to control the delay before the object reappears.

    private bool hasPlayerSteppedOn = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasPlayerSteppedOn)
        {
            hasPlayerSteppedOn = true;
            StartCoroutine(DisappearAfterDelay());
        }
    }

    IEnumerator DisappearAfterDelay()
    {
        // Wait for the disappear delay.
        yield return new WaitForSeconds(disappearDelay);

        // Deactivate the object's renderer and collider to make it "disappear".
        SetComponentsActive(false);

        // Wait for the reappear delay.
        yield return new WaitForSeconds(reappearDelay);

        // Reactivate the object's renderer and collider to make it "reappear".
        SetComponentsActive(true);

        // Reset the flag for the player's next interaction.
        hasPlayerSteppedOn = false;
    }

    private void SetComponentsActive(bool active)
    {
        // Assuming the object has a Renderer and Collider component you want to enable/disable.
        var renderer = GetComponent<Renderer>();
        var collider = GetComponent<Collider>();

        if (renderer != null)
            renderer.enabled = active;

        if (collider != null)
            collider.enabled = active;
    }
}
