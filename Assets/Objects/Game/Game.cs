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

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        DebugScreenDrawer.Enable("points", "Points: " + Points);
        DebugScreenDrawer.Enable("turns", "Turns: " + Turns);
    }
}
