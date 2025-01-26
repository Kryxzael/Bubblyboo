using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class ExplodingBubble : Bubble
{
    public bool BigExplosion;

    protected override IEnumerator OnPop(Turn turn)
    {
        if (turn.InRetrigger)
            yield break;

        yield return new PopChainDelay(true);

        foreach (Bubble i in Game.Instance.BubbleWrap.GetNeighbors(GridPosition.x, GridPosition.y, BigExplosion))
        {
            i.Pop(turn);
        }
    }
}