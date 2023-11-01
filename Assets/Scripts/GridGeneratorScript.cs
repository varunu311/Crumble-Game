using UnityEngine;

public class GridGeneratorScript : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab you want to generate.
    public int gridSizeX = 5;
    public int gridSizeY = 5;
    public float spacing = 1.0f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 spawnPosition = new Vector3(x * spacing, 0, y * spacing);
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}

