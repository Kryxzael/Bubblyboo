using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BasicBubble : Bubble
{
    public int Points = 1;
    public int Multiplier = 0;

    protected override IEnumerator OnPop(Turn turn)
    {
        yield return new PopChainDelay();
        turn.Points += Points;
        turn.Multiplier += Multiplier;
    }
}