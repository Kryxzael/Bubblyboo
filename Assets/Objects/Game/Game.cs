
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

    private void Awake()
    {
        Instance = this;
        BubbleWrap = FindObjectOfType<BubbleWrap>();
    }

    private void Update()
    {
        DebugScreenDrawer.Enable("points", "Points: " + Points);
        DebugScreenDrawer.Enable("turns", "Turns: " + Turns);

        if (CurrentTurn != null)
        {
            DebugScreenDrawer.Enable("points-in-turn", "+" + CurrentTurn.Points);
            DebugScreenDrawer.Enable("mult-in-turn", "x" + CurrentTurn.Multiplier);
        }
        else
        {
            DebugScreenDrawer.Disable("points-in-turn");
            DebugScreenDrawer.Disable("mult-in-turn");
        }
        
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
