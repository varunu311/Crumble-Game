using UnityEngine;
using System.Collections;

public class GridGeneratorScript : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab you want to generate.
    public int gridSizeX = 5;
    public int gridSizeY = 5;
    public float spacing = 1.0f;

    public GameObject[,] gridTiles; // Array to store references to tiles

    void Start()
    {
        gridTiles = new GameObject[gridSizeX, gridSizeY];
        GenerateGrid();
        StartCoroutine(RemoveOuterTiles());
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 spawnPosition = new Vector3(x * spacing, 0, y * spacing);
                GameObject tile = Instantiate(prefab, spawnPosition, Quaternion.identity);
                gridTiles[x, y] = tile; // Store the reference to the tile
            }
        }
    }

    IEnumerator RemoveOuterTiles()
    {
        int minX = 0, minY = 0;
        int maxX = gridSizeX - 1, maxY = gridSizeY - 1;

        while (minX < maxX && minY < maxY)
        {
            yield return new WaitForSeconds(30); // Wait for 30 seconds

            // Remove top row and bottom row
            for (int x = minX; x <= maxX; x++)
            {
                DestroyTile(x, minY);
                DestroyTile(x, maxY);
            }

            // Remove left column and right column
            for (int y = minY + 1; y <= maxY - 1; y++)
            {
                DestroyTile(minX, y);
                DestroyTile(maxX, y);
            }

            // Update the grid boundaries
            minX++; minY++;
            maxX--; maxY--;
        }
    }

    void DestroyTile(int x, int y)
    {
        if (gridTiles[x, y] != null)
        {
            StartCoroutine(FadeOutAndDestroy(gridTiles[x, y]));
            gridTiles[x, y] = null; // Remove the reference to the tile
        }
    }

        IEnumerator FadeOutAndDestroy(GameObject tile)
    {
        MeshRenderer renderer = tile.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            float fadeDuration = 10.0f; // Duration in seconds for fade-out effect
            float elapsedTime = 0;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);

                Color newColor = mat.color;
                newColor.a = alpha;
                mat.color = newColor;

                yield return null;
            }
        }

        Destroy(tile);
    }



}
