
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public int Points;
    public int Turns = 10;

    public Turn CurrentTurn;

    public BubbleWrap BubbleWrap { get; private set; }
    public Shop Shop { get; private set; }
    public SpriteRenderer GhostSprite { get; private set; }

    private void Awake()
    {
        Instance = this;
        BubbleWrap = FindObjectOfType<BubbleWrap>();
        Shop = FindObjectOfType<Shop>();
        GhostSprite = FindObjectOfType<FollowCursor>(includeInactive: true).GetComponent<SpriteRenderer>();
        FindObjectOfType<DebugMenu>().IsOpen = true;
    }

    public Turn BeginTurn()
    {
        Turns--;
        CurrentTurn = new Turn();
        return CurrentTurn;
    }

    public void EndTurn(Turn turn)
    {
        Points += (int)(turn.Points * turn.Multiplier);
    }
}
