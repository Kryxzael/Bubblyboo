using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class ExplodingBubble : Bubble
{
    protected override IEnumerator OnPop(Turn turn)
    {
        yield return new PopChainDelay();

        foreach (Bubble i in Game.Instance.BubbleWrap.GetNeighbors(GridPosition.x, GridPosition.y))
        {
            i.Pop(turn);
        }
    }
}