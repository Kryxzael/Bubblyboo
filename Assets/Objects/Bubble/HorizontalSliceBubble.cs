using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HorizontalSliceBubble : Bubble
{
    protected override IEnumerator OnPop(Turn turn)
    {
        if (turn.InRetrigger)
            yield break;

        int left  = GridPosition.x - 1;
        int right = GridPosition.x + 1;

        yield return new PopChainDelay(true);


        while (left >= 0 || right < Game.Instance.BubbleWrap.SizeX)
        {
            if (left >= 0)
                Game.Instance.BubbleWrap.Grid[left, GridPosition.y].Pop(turn);

            if (right < Game.Instance.BubbleWrap.SizeX)
                Game.Instance.BubbleWrap.Grid[right, GridPosition.y].Pop(turn);

            left--;
            right++;

            yield return new PopChainDelay(false);
        }
    }
}