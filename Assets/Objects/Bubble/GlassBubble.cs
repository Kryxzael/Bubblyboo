using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class GlassBubble : Bubble
{
    public int Points;
    public float Chance;

    protected override IEnumerator OnPop(Turn turn)
    {
        yield return new PopChainDelay(true);

        if (UnityEngine.Random.value < Chance)
        {
            int pnts = (int)(Points * turn.Multiplier);
            Game.Instance.CreateDamageNumber("+" + pnts, Color.red, 1f, this);

            Game.Instance.Points += pnts;
            Game.Instance.ExcitementLevel = Mathf.Min(1f, Game.Instance.ExcitementLevel + Game.Instance.ExcitementIncreasePerPoint * pnts);
        }
        else
        {
            Game.Instance.CreateDamageNumber("Dud", Color.gray, 1f, this);
        }
    }
}
