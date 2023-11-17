using UnityEngine;

public class PowerUpGenerationScript : MonoBehaviour
{
    public GameObject powerUpPrefab; // Reference to the power-up prefab
    public GridGeneratorScript gridGenerator; // Reference to the GridGeneratorScript
    public float powerUpSpawnInterval = 20.0f; // Time interval between power-up spawns
    public float disappearTime = 5.0f;
    public int spawnYCord = 1;
    private float timeSinceLastSpawn = 0.0f;

    void Update()
    {
        // Update the timer
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn a new power-up
        if (timeSinceLastSpawn >= powerUpSpawnInterval)
        {
            SpawnPowerUp();
            timeSinceLastSpawn = 0.0f;
        }
    }

    void SpawnPowerUp()
    {
        int x = Random.Range(0, gridGenerator.gridSizeX);
        int y = Random.Range(0, gridGenerator.gridSizeY);
        

        // Ensure the selected tile is not null and does not already have a power-up
        if (gridGenerator.gridTiles[x, y] != null && gridGenerator.gridTiles[x, y].transform.childCount == 0)
        {
            Vector3 spawnPosition = gridGenerator.gridTiles[x, y].transform.position;
            spawnPosition.y = spawnYCord; // Set the Y-coordinate to 3
            GameObject powerUpInstance = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity, gridGenerator.gridTiles[x, y].transform);
            Destroy(powerUpInstance, disappearTime);
        }
    }
}
