using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class BasicBubble : Bubble
{
    public int Points = 1;
    public int Retriggers = 0;

    protected override IEnumerator OnPop(Turn turn)
    {
        int pnts = (int)(Points * turn.Multiplier);

        if (Points != 0)
        {
            yield return new PopChainDelay(true);
            Game.Instance.CreateDamageNumber("+" + pnts, Color.gray, 1f, this);
        }

        if (Retriggers != 0 && !turn.InRetrigger)
        {
            yield return new PopChainDelay(true);
            Game.Instance.CreateDamageNumber("Re-Pop!", Color.green, 1.5f, this);
            turn.Retriggers += Retriggers;
        }

        Game.Instance.Points += pnts;
        Game.Instance.ExcitementLevel = Mathf.Min(1f, Game.Instance.ExcitementLevel + Game.Instance.ExcitementIncreasePerPoint * pnts);
    }
}