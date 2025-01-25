using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using UnityEngine;
using UnityEngine.UI;

public class BubbleWrap : MonoBehaviour
{
    public RandomValueFromSet<Bubble> BubblePrefabs;
    public Bubble[,] Grid { get; set; }


    public int SizeX = 10;
    public int SizeY = 10;

    public int Spacing = 1;

    public bool OffsetOdds;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoSpawnInitialGrid());
    }

    private IEnumerator CoSpawnInitialGrid()
    {
        Grid = new Bubble[SizeX, SizeY];
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                SpawnBubble(x, y, BubblePrefabs.Pick());
                yield return new WaitForSeconds(0.005f);
            }
        }
    }

    public void SpawnBubble(int x, int y, Bubble bubble)
    {
        if (Grid[x, y] != null)
            Destroy(Grid[x, y].gameObject);

        float offsetX = 0;
        if (OffsetOdds && y % 2 == 1)
            offsetX = Spacing / 2f;

        Grid[x, y] = Instantiate(
            original: bubble,
            position: new(x * Spacing + offsetX, y * Spacing),
            rotation: Quaternion.identity,
            transform
        );

        Grid[x, y].GridPosition = new Vector2Int(x, y);
    }

    public IEnumerable<Bubble> GetNeighbors(int x, int y)
    {
        foreach (Vector2Int i in GetNeighborPositions(x, y))
        {
            if (i.x >= 0 && i.x < SizeX && i.y >= 0 && i.y < SizeY)
            {
                yield return Grid[i.x, i.y];
            }
        }
    }

    public static IEnumerable<Vector2Int> GetNeighborPositions(int x, int y)
    {
        if (y % 2 == 0)
        {
            return new Vector2Int[]
            {
                new(x - 1, y - 1),
                new(x, y - 1),

                new(x - 1, y),
                new(x + 1, y),

                new(x - 1, y + 1),
                new(x, y + 1),
            };
        }
        else
        {
            return new Vector2Int[]
            {
                new(x, y - 1),
                new(x + 1, y - 1),

                new(x - 1, y),
                new(x + 1, y),

                new(x, y + 1),
                new(x + 1, y + 1),
            };
        }
    }
}
