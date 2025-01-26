using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class ForwardDiagnoalSliceBubble : Bubble
{
    protected override IEnumerator OnPop(Turn turn)
    {
        if (turn.InRetrigger)
            yield break;

        Vector2Int up    = GridPosition;
        Vector2Int down  = GridPosition;
        spread();

        yield return new PopChainDelay(true);


        while ((up.y < Game.Instance.BubbleWrap.SizeY && up.x < Game.Instance.BubbleWrap.SizeX) || (down.y >= 0 && down.x >= 0))
        {
            if (up.y < Game.Instance.BubbleWrap.SizeY && up.x < Game.Instance.BubbleWrap.SizeX)
                Game.Instance.BubbleWrap.Grid[up.x, up.y].Pop(turn);

            if (down.y >= 0 && down.x >= 0)
                Game.Instance.BubbleWrap.Grid[down.x, down.y].Pop(turn);


            spread();
            yield return new PopChainDelay(false);
        }

        void spread()
        {
            up.y++;
            down.y--;

            if (up.y % 2 == 0)
                up.x++;

            if (down.y % 2 == 1)
                down.x--;
        }
    }
}