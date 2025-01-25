using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Turn
{
    public float Multiplier = 1;
    public int Retriggers = 0;
    public bool AllowRetriggers = true;

    public HashSet<Bubble> Popped = new();

    public int ProcessingLevel;
}
