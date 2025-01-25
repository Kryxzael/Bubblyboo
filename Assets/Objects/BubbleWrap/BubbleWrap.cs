using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
        bool createMoney = UnityEngine.Random.value < Game.Instance.MoneySpawnRate;

        if (Grid[x, y] != null)
        {
            if (!Grid[x, y].IsPopped)
                createMoney = Grid[x, y].HasMoney;
            else
                createMoney = false;

            Destroy(Grid[x, y].gameObject);
        }

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

        if (createMoney)
        {
            Grid[x, y].HasMoney = true;
            Grid[x, y].MoneySprite.enabled = true;
        }
    }

    public IEnumerable<Bubble> GetNeighbors(int x, int y, bool extend)
    {
        foreach (Vector2Int i in extend ? GetExtendedNeighborPositions(x, y) : GetNeighborPositions(x, y))
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

    public static IEnumerable<Vector2Int> GetExtendedNeighborPositions(int x, int y)
    {
        IEnumerable<Vector2Int> nearestNeighbors = GetNeighborPositions(x, y);

        if (y % 2 == 0)
        {
            return nearestNeighbors.Concat(new Vector2Int[]
            {
                new(x - 1, y - 2),
                new(x,     y - 2),
                new(x + 1, y - 2),

                new(x - 2, y - 1),
                new(x + 1, y - 1),

                new(x - 2, y),
                new(x + 2, y),

                new(x - 2, y + 1),
                new(x + 1, y + 1),

                new(x - 1, y + 2),
                new(x,     y + 2),
                new(x + 1, y + 2),
            });
        }
        else
        {
            return nearestNeighbors.Concat(new Vector2Int[]
            {
                new(x - 1, y - 2),
                new(x,     y - 2),
                new(x + 1, y - 2),
                
                new(x - 1, y - 1),
                new(x + 2, y - 1),

                new(x - 2, y),
                new(x + 2, y),

                new(x - 1, y + 1),
                new(x + 2, y + 1),
                
                new(x - 1, y + 2),
                new(x,     y + 2),
                new(x + 1, y + 2),
            });
        }
    }
}
