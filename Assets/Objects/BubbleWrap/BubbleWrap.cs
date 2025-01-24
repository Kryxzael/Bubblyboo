using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using UnityEngine;
using UnityEngine.UI;

public class BubbleWrap : MonoBehaviour
{
    public Bubble BubblePrefab;
    public Bubble[,] Grid { get; set; }


    public int SizeX = 10;
    public int SizeY = 10;

    public int Spacing = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnInitialGrid();
    }

    private void SpawnInitialGrid()
    {
        Grid = new Bubble[SizeX, SizeY];
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                Grid[x, y] = Instantiate(
                    original: BubblePrefab,
                    position: new(x * Spacing, y * Spacing),
                    rotation: Quaternion.identity,
                    transform
                );
            }
        }

        //Center grid to original position
        transform.position = new Vector2(transform.position.x - Spacing * SizeX / 2, transform.position.y - Spacing * SizeY / 2);
    }
}
